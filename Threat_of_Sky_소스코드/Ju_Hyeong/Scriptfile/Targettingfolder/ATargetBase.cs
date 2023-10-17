using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/**
 * 20181289 한주형
 * 드론과 타겟의 스크립트를 다르게 해여 포신이 돌아가지 않는 버그를 해결함.
 * 
 * 이 스크립트의 역할은 모든 페밀리어(드론, 치유드론, 터렛)의 타겟을 담당하는 스크립트 입니다.
 * TragetController.cs와 RifleDrone.cs등의 스크립트와 연결되어있습니다.
 **/


public struct TargetData
{
    public float droneHP;
    public float droneRange;
    public float spinSpeed;
    public float m_fireRate;
    public float m_currentFireRate;
    public float DeliveryValue;
}

public abstract class ATargetBase : MonoBehaviourPun, IDamageable
{
    public TargetData Tdata;
    Transform m_tfTarget = null;
    private RaycastHit hitInfo;
    private bool isDie = false;

    public virtual void InitSetting()
    {
        InvokeRepeating("TargetSearch", 0f, 0.5f);
    }

    // 가장 가까운 적을 찾는 단계
    public virtual void TargetSearch(LayerMask layermask)
    {
        Collider[] t_cols = Physics.OverlapSphere(transform.position, Tdata.droneRange, layermask);
        Transform t_shorttestTarget = null;

        if (t_cols.Length > 0)
        {
            float t_shortestDistance = Mathf.Infinity;
            foreach (Collider t_colTarget in t_cols)
            {
                float t_distance = Vector3.SqrMagnitude(transform.position - t_colTarget.transform.position);
                if (t_shortestDistance > t_distance)
                {
                    t_shortestDistance = t_distance;
                    t_shorttestTarget = t_colTarget.transform;
                }
            }
        }

        m_tfTarget = t_shorttestTarget;
    }


    public virtual void TargetUpdate(Transform m_tfbarrelBody, LayerMask layermask)
    {
        
        if (m_tfTarget == null)
        {
            m_tfbarrelBody.Rotate(new Vector3(0, 180, 0) * Time.deltaTime);
           // Debug.Log("changVec");
        }
        else
        {
            Vector3 direction = m_tfTarget.position - transform.position;
            bool hit = Physics.Raycast(transform.position, direction, out hitInfo, Tdata.droneRange);

            Quaternion lookrotation = Quaternion.LookRotation(direction); // 포신이 바라보는 방향
            transform.rotation = Quaternion.Lerp(transform.rotation, lookrotation, Tdata.spinSpeed * Time.deltaTime);

            if(Quaternion.Angle(m_tfbarrelBody.rotation, lookrotation) < 5f && hit) // 어드정도 각도 이하일때 발사 준비를 하는가
            {
                Tdata.m_currentFireRate -= Time.deltaTime;
                if(Tdata.m_currentFireRate <= 0)
                {
                    Tdata.m_currentFireRate = Tdata.m_fireRate; // 연사 속도.
                    // Debug.Log("발사");
                    if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    {
                        //Debug.Log(hitInfo.collider.name);
                        Debug.DrawRay(transform.position, m_tfbarrelBody.forward * Tdata.droneRange, Color.red, 1f);
                        //hitInfo.transform.GetComponent<EnemyCtrl>().TakeDamage(Tdata.DeliveryValue); // 적 데미지 함수. 이건 공격 드론에 빼자
                        DamageFramework.DamageHelper.ApplyDamage(hitInfo.collider.gameObject, Tdata.DeliveryValue);
                    }
                    if(hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                    {
                        //Debug.Log(hitInfo.collider.name);
                        Debug.DrawRay(transform.position, m_tfbarrelBody.forward * Tdata.droneRange, Color.green, 1f);
                        hitInfo.transform.GetComponent<ACharacterBase>().RestoreHealth(Tdata.DeliveryValue);
                    }
                }
            }
        }
    }

    void IDamageable.TakeDamage(float DamageAmount, Vector3 CauserPosition, DamageFramework.ADamageType DamageTypePrefab, float DamageTypeDamageMult)
    {
        //data.droneHP -= DamageAmount;

        // 사망하면 인식하지 않는다.
        if (isDie) return;

        if (DamageTypePrefab != null)
        {
            // 피해 유형을 인스턴싱합니다! 이 예제에서는 부모 객체가 자신이 되도록 인스턴싱하고 있습니다.
            DamageFramework.ADamageType AppliedDamageType = Instantiate(DamageTypePrefab, transform);

            // 함수 하나만 호출해주면 끝입니다!
            // StartDamage(대상 게임오브젝트(보통 자기 자신), 피해 유형 배율, 추가 피해량);
            AppliedDamageType.StartDamage(this.gameObject, DamageTypeDamageMult, 0.0f);
        }

        // 몬스터의 hp가 받은 데미지 만큼 감소
        Tdata.droneHP -= DamageAmount;
        photonView.RPC("SetDroneAbility", RpcTarget.All, Tdata.droneHP);
        //photonView.RPC("SetAnimation", RpcTarget.Others, Damage);

        if (Tdata.droneHP <= 0.0f)
        {
            isDie = true;
            //MonsterDie();
            Destroy(gameObject);
        }
    }


    protected void SetDroneAbility(float hp)
    {
        this.Tdata.droneHP = hp;
    }
}

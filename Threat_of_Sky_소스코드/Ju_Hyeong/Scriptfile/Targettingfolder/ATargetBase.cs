using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/**
 * 20181289 ������
 * ��а� Ÿ���� ��ũ��Ʈ�� �ٸ��� �ؿ� ������ ���ư��� �ʴ� ���׸� �ذ���.
 * 
 * �� ��ũ��Ʈ�� ������ ��� ��и���(���, ġ�����, �ͷ�)�� Ÿ���� ����ϴ� ��ũ��Ʈ �Դϴ�.
 * TragetController.cs�� RifleDrone.cs���� ��ũ��Ʈ�� ����Ǿ��ֽ��ϴ�.
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

    // ���� ����� ���� ã�� �ܰ�
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

            Quaternion lookrotation = Quaternion.LookRotation(direction); // ������ �ٶ󺸴� ����
            transform.rotation = Quaternion.Lerp(transform.rotation, lookrotation, Tdata.spinSpeed * Time.deltaTime);

            if(Quaternion.Angle(m_tfbarrelBody.rotation, lookrotation) < 5f && hit) // ������� ���� �����϶� �߻� �غ� �ϴ°�
            {
                Tdata.m_currentFireRate -= Time.deltaTime;
                if(Tdata.m_currentFireRate <= 0)
                {
                    Tdata.m_currentFireRate = Tdata.m_fireRate; // ���� �ӵ�.
                    // Debug.Log("�߻�");
                    if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    {
                        //Debug.Log(hitInfo.collider.name);
                        Debug.DrawRay(transform.position, m_tfbarrelBody.forward * Tdata.droneRange, Color.red, 1f);
                        //hitInfo.transform.GetComponent<EnemyCtrl>().TakeDamage(Tdata.DeliveryValue); // �� ������ �Լ�. �̰� ���� ��п� ����
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

        // ����ϸ� �ν����� �ʴ´�.
        if (isDie) return;

        if (DamageTypePrefab != null)
        {
            // ���� ������ �ν��Ͻ��մϴ�! �� ���������� �θ� ��ü�� �ڽ��� �ǵ��� �ν��Ͻ��ϰ� �ֽ��ϴ�.
            DamageFramework.ADamageType AppliedDamageType = Instantiate(DamageTypePrefab, transform);

            // �Լ� �ϳ��� ȣ�����ָ� ���Դϴ�!
            // StartDamage(��� ���ӿ�����Ʈ(���� �ڱ� �ڽ�), ���� ���� ����, �߰� ���ط�);
            AppliedDamageType.StartDamage(this.gameObject, DamageTypeDamageMult, 0.0f);
        }

        // ������ hp�� ���� ������ ��ŭ ����
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

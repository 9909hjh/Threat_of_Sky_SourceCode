using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
//using Photon.Realtime;
using System.Collections;


/**
 * 작성자: 20181289 한주형
 * 드론의 체력, 움직임, 사정거리을 담당하는 기본 베이스 클레스입니다.
 * 스트럭트로 선언하고 상속하여 사용하도록 할 것입니다.
 * 
 * 벽에 닿으면 미끄러지는 현상 마찰력을 0으로 바꿔 수정함
 * 
 * 고민 중인 것 : 빈 오브젝트에 자식으로 들어가 따라 가게 만들어야 하나? 공격시 적을 타겟하는 모습을 보이게 만들려면?
 */

public struct DroneData
{
    public float droneHP;
    public float droneSpeed;
    public float rotationModifier;
    public float SenseDistance;
    public float DistanceDiffSpeed;
}

public abstract class ADroneBase : MonoBehaviourPun
{
    public DroneData data;


    public virtual void InitSetting()
    {
        InvokeRepeating("AttackDrone", 0f, 0.5f);
    }

    public virtual void FlyingDrone(Transform targetGO, Rigidbody rb)
    {
        if (gameObject != null)
        {
            Vector3 vectorToTarget = targetGO.position + (Vector3.up * 5) - rb.position;
            vectorToTarget.Normalize();
            Vector3 rotationAmount = Vector3.Cross(transform.forward, vectorToTarget);

            rb.angularVelocity = rotationAmount * data.rotationModifier;
            rb.velocity = transform.forward * data.droneSpeed;

               
            if (Vector3.Distance(targetGO.position, rb.position) >= data.SenseDistance)
            {
                rb.velocity = transform.forward * data.droneSpeed * data.DistanceDiffSpeed;
            }
            //if (Vector3.Distance(targetGO.position, rb.position) <= data.SenseDistance)
            //{
            //    rb.angularVelocity = rotationAmount * data.rotationModifier * 2;
            //}

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            //changVec = -changVec;

            //check = true;
            //Debug.Log("changVec" + changVec);
        }
    }

}
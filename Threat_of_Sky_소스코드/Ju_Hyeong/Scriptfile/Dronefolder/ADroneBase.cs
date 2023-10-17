using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
//using Photon.Realtime;
using System.Collections;


/**
 * �ۼ���: 20181289 ������
 * ����� ü��, ������, �����Ÿ��� ����ϴ� �⺻ ���̽� Ŭ�����Դϴ�.
 * ��Ʈ��Ʈ�� �����ϰ� ����Ͽ� ����ϵ��� �� ���Դϴ�.
 * 
 * ���� ������ �̲������� ���� �������� 0���� �ٲ� ������
 * 
 * ��� ���� �� : �� ������Ʈ�� �ڽ����� �� ���� ���� ������ �ϳ�? ���ݽ� ���� Ÿ���ϴ� ����� ���̰� �������?
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
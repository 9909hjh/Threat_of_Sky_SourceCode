using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Missile : MonoBehaviour
{
    //[SerializeField] private Transform targetCube;
    [SerializeField] private float force;
    [SerializeField] private float rotationForce;
    [SerializeField] private float WaitSeconds;
    [SerializeField] private float launchForce;
    private Rigidbody rb;
    private bool shouldFollow;

    Transform m_tfTarget = null;

    public LayerMask layermask;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(WaitMissile());
    }

    // 가장 가까운 적을 찾는 단계
    public void MissileTargetSearch()
    {
        Collider[] t_cols = Physics.OverlapSphere(transform.position, 100.0f, layermask);
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
            //Debug.Log("ins??");
        }

        m_tfTarget = t_shorttestTarget;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MissileTargetSearch();

        if(shouldFollow)
        {
            if (m_tfTarget != null)
            {
                Vector3 direction = m_tfTarget.position - rb.position;
                direction.Normalize();
                Vector3 rotationAmount = Vector3.Cross(transform.forward, direction);
                rb.angularVelocity = rotationAmount * rotationForce;
                rb.velocity = transform.forward * force;
            }
            else
            {
                //미사일이 터지는 이팩트를 주자. 오브젝트를 삭제하고.
            }
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
    //    {
    //        //데미지를 전달?
    //        Destroy(gameObject);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //데미지를 전달?
            Destroy(gameObject);
        }
    }

    private IEnumerator WaitMissile()
    {
        rb.AddForce(Vector3.up * launchForce, ForceMode.Impulse);
        yield return new WaitForSeconds(WaitSeconds);
        shouldFollow = true;
    }
}

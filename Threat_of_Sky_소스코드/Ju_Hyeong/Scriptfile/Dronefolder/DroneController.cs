using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DroneController : MonoBehaviourPun //IInteractableObject
{
    public ADroneBase myDrone;
    private Rigidbody rb;

    PhotonView pv;

    [SerializeField]
    private Transform tr;


    public TargetController targetController;


    public void FollowTrans(Transform transform)
    {
        this.tr = transform;
    }

    void Start()
    {
        Invoke("testupdate", 2.0f);
        myDrone.InitSetting();
        rb = GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();

        targetController = GetComponent<TargetController>();

        GetComponent<Collider>().isTrigger = true;

        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Droneupdate();
    }
    

    void Droneupdate()
    {
        if (targetController.TryTakeMoeny)
        {
            if (pv.IsMine)
            {
                //rb.drag = 0;

                GetComponent<Collider>().isTrigger = false;
                myDrone.FlyingDrone(tr, rb);
                Debug.Log(tr);
            }
        }
    }

    

    
}

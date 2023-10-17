using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TestCh : MonoBehaviourPun
{
    public AActiveItemBase aActiveItemBase;

    private float h = 0f;
    private float v = 0f;
    private Transform tr;
    public float speed = 10f;
    //public AActiveItemBase activeItemBase;
    //TestActive testActive;
    PhotonView pv;

    [SerializeField] private float MoneyHas = 100;

    bool key;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PhotonNetwork.IsMasterClient);
        //activeItemBase = GetComponent<AActiveItemBase>();
        //testActive = GetComponent<TestActive>();
        pv = GetComponent<PhotonView>();

        

        //IInteractableObject Interactable = GetComponent<IInteractableObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                FindObjectOfType<TargetController>().GetComponent<IInteractableObject>().Interact(photonView.ViewID);
                //GetComponent<IInteractableObject>().Interact(photonView.ViewID);
            }
            //bool a;
            //if (Input.GetKeyDown(KeyCode.F7))
            //{
            //    a = aActiveItemBase.TryUseItem();

            //    if(a == true)
            //    {
            //        Debug.Log("들어왔나");
            //    }
            //}


            //Debug.Log(PhotonNetwork.IsMasterClient);
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");

            Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
            //tr.Translate(moveDir.normalized * Time.deltaTime * speed);

            key = Input.GetKeyDown(KeyCode.I);
            //Debug.Log("생성");
            if (key)
            {
                //Debug.Log("생성");
                aActiveItemBase.TryUseItem();

            }
        }
            
        //if(testActive.UseItem() == true)
        //{
        //    Debug.Log("아이템 사용.");
        //}
    }

    public void AddMoney(float Amount)
    {
        MoneyHas += Amount;
        Debug.Log(MoneyHas);
    }

    public bool TryUseMoney(float Amount)
    {
        if (MoneyHas < Amount) return false;

        MoneyHas -= Amount;

        //Hud_InGameHud?.UpdateMoneyText(MoneyHas);

        return true;
    }
}

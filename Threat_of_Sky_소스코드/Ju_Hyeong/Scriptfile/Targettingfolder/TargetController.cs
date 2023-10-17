using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class TargetController : MonoBehaviourPun, IInteractableObject
{
    public ATargetBase myTarget;
    public LayerMask LayerMask;
    public Transform m_tfbarrelBody;

    public bool TryTakeMoeny = false;
    [SerializeField]
    private float TakeMoney = 10.0f;

    [SerializeField]
    private TMP_Text PriceText;

    private bool bTryOpen = true;


    void IInteractableObject.Interact(int InteractingCharacterViewID)
    {
        ACharacterBase InteractingCharacter = PhotonView.Find(InteractingCharacterViewID).gameObject.GetComponent<ACharacterBase>();
        //TestCh InteractingCharacter = PhotonView.Find(InteractingCharacterViewID).gameObject.GetComponent<TestCh>(); // 사용할거면 이건 주석

        //Debug.Log(InteractingCharacterViewID);
        
        if (InteractingCharacter != null && bTryOpen)
        {
            TryTakeMoeny = InteractingCharacter.TryUseMoney(TakeMoney);

            DroneController droneController = GetComponent<DroneController>();
            if(droneController != null)
                droneController.FollowTrans(InteractingCharacter.transform);
        }
    }

    void Start()
    {
        myTarget.InitSetting();

        TakeMoney *= (0.3f * GameManager.Instance.GetCurrentLevel);
        PriceText.text = TakeMoney.ToString();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (TryTakeMoeny)
        {
            PriceText.gameObject.SetActive(false);
            bTryOpen = false;
            myTarget.TargetSearch(LayerMask);
            myTarget.TargetUpdate(m_tfbarrelBody, LayerMask);
        }

    }
}

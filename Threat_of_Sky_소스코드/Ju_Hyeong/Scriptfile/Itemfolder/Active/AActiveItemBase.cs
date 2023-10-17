using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public enum ActiveItemRating
{
    OMEGA
}

public abstract class AActiveItemBase : MonoBehaviourPun
{
    public string GetItemName { get => ItemName; }

    public Sprite GetItemSprite { get => ItemTexture; }

    [SerializeField, Tooltip("아이템 등급")]
    protected ItemRating itemRating;

    [SerializeField, Tooltip("아이템 이름")]
    protected string ItemName = "";

    [SerializeField, Tooltip("아이템 설명")]
    protected string ItemTooltip = "";

    public string GetItemTooltip {  get => ItemTooltip; }

    //protected string rating = "";
    [SerializeField, Tooltip("아이템 텍스쳐")]
    protected Sprite ItemTexture;

    [SerializeField, Tooltip("사용 캐릭터")]
    protected ACharacterBase ownerCharacter;

    //쿨타임 제작해야함.
    [SerializeField, Tooltip("아이템 쿨타임")]
    private float ActiveCoolDown;
    public float GetActiveCoolDown { get => ActiveCoolDown; }

    //스킬을 사용할 수 있는가?
    public bool bCansUseSkill = true;

    public void SetownerCharceter(ACharacterBase ownerCharacter) { this.ownerCharacter = ownerCharacter; }

    //[SerializeField, Tooltip("사용 캐릭터")]
    //protected TestCh ownerCharacter;

    public virtual void UseItem()
    {
        //return bIsUsableItem;
    }

    //아이템 사용.
    public bool TryUseItem()
    {
        if(bCansUseSkill)
        {
            bCansUseSkill = false;
            UseItem();
            // 쿨타임 돌리는 함수 호출
            StartCoroutine(StartCoolDown(ActiveCoolDown));
            Debug.Log("is in?");
            return true;
        }
        return false;
    }

    IEnumerator StartCoolDown(float activeCoolDown)
    {
        yield return new WaitForSeconds(activeCoolDown);
        bCansUseSkill = true;
        //Debug.Log(activeCoolDown);
    }
}

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

    [SerializeField, Tooltip("������ ���")]
    protected ItemRating itemRating;

    [SerializeField, Tooltip("������ �̸�")]
    protected string ItemName = "";

    [SerializeField, Tooltip("������ ����")]
    protected string ItemTooltip = "";

    public string GetItemTooltip {  get => ItemTooltip; }

    //protected string rating = "";
    [SerializeField, Tooltip("������ �ؽ���")]
    protected Sprite ItemTexture;

    [SerializeField, Tooltip("��� ĳ����")]
    protected ACharacterBase ownerCharacter;

    //��Ÿ�� �����ؾ���.
    [SerializeField, Tooltip("������ ��Ÿ��")]
    private float ActiveCoolDown;
    public float GetActiveCoolDown { get => ActiveCoolDown; }

    //��ų�� ����� �� �ִ°�?
    public bool bCansUseSkill = true;

    public void SetownerCharceter(ACharacterBase ownerCharacter) { this.ownerCharacter = ownerCharacter; }

    //[SerializeField, Tooltip("��� ĳ����")]
    //protected TestCh ownerCharacter;

    public virtual void UseItem()
    {
        //return bIsUsableItem;
    }

    //������ ���.
    public bool TryUseItem()
    {
        if(bCansUseSkill)
        {
            bCansUseSkill = false;
            UseItem();
            // ��Ÿ�� ������ �Լ� ȣ��
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public enum ItemRating
{
    COMMON, ADVANCED, SPECIAL, UNIQUE, UNREAL, ULTRON
}

[System.Serializable]
public struct CharacterStatusItemInfo
{
    public ECharacterStatus StatusToItem;
    public float Amount;
    public EApplyStatusType ApplyType;
}

public abstract class AStatusItemBase : MonoBehaviour
{
    [SerializeField, Tooltip("리스트")]
    protected List<CharacterStatusItemInfo> ItemInfoList = new List<CharacterStatusItemInfo>();

    public List<CharacterStatusItemInfo> GetItemInfoList { get => ItemInfoList; }

    public string GetItemName { get => ItemName; }

    public Sprite GetItemSprite { get => ItemTexture; }

    [SerializeField, Tooltip("아이템 등급")]
    protected ItemRating itemRating;

    public ItemRating GetItemRating { get => itemRating; }

    [SerializeField, Tooltip("아이템 이름")]
    protected string ItemName = "";

    [SerializeField, Tooltip("아이템 설명")]
    protected string ItemTooltip = "";

    public string GetItemTooltip { get => ItemTooltip; }

    //protected string rating = "";
    [SerializeField, Tooltip("아이템 텍스쳐")]
    protected Sprite ItemTexture;

    [SerializeField, Tooltip("아이템 수량")]
    public int ItemAmount;

    //[SerializeField, Tooltip("엑티브 아이템인지 채크")]
    //protected bool bIsUsableItem = false;

    [SerializeField, ReadOnlyProperty]
    protected ACharacterBase OwnerCharacter;


    /// <summary>
    /// 새 오너 캐릭터를 설정합니다.
    /// 설정 이후, OnItemAdded()를 호출합니다.
    /// </summary>
    /// <param name="NewOwner"> 새 오너 캐릭터</param>
    public void SetOwnerCharacter(ACharacterBase NewOwner)
	{
        OwnerCharacter = NewOwner;

        OnItemAdded();
	}

    /// <summary>
    /// 아이템이 최초로 추가되었을 때 호출
    /// 중복으로 추가되는 경우 (1개 -> 2개 등) 에는 호출되지 않음!
    /// </summary>
    protected virtual void OnItemAdded()
	{

	}
}

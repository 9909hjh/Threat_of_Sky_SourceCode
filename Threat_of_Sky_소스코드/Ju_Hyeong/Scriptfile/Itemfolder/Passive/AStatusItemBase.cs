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
    [SerializeField, Tooltip("����Ʈ")]
    protected List<CharacterStatusItemInfo> ItemInfoList = new List<CharacterStatusItemInfo>();

    public List<CharacterStatusItemInfo> GetItemInfoList { get => ItemInfoList; }

    public string GetItemName { get => ItemName; }

    public Sprite GetItemSprite { get => ItemTexture; }

    [SerializeField, Tooltip("������ ���")]
    protected ItemRating itemRating;

    public ItemRating GetItemRating { get => itemRating; }

    [SerializeField, Tooltip("������ �̸�")]
    protected string ItemName = "";

    [SerializeField, Tooltip("������ ����")]
    protected string ItemTooltip = "";

    public string GetItemTooltip { get => ItemTooltip; }

    //protected string rating = "";
    [SerializeField, Tooltip("������ �ؽ���")]
    protected Sprite ItemTexture;

    [SerializeField, Tooltip("������ ����")]
    public int ItemAmount;

    //[SerializeField, Tooltip("��Ƽ�� ���������� äũ")]
    //protected bool bIsUsableItem = false;

    [SerializeField, ReadOnlyProperty]
    protected ACharacterBase OwnerCharacter;


    /// <summary>
    /// �� ���� ĳ���͸� �����մϴ�.
    /// ���� ����, OnItemAdded()�� ȣ���մϴ�.
    /// </summary>
    /// <param name="NewOwner"> �� ���� ĳ����</param>
    public void SetOwnerCharacter(ACharacterBase NewOwner)
	{
        OwnerCharacter = NewOwner;

        OnItemAdded();
	}

    /// <summary>
    /// �������� ���ʷ� �߰��Ǿ��� �� ȣ��
    /// �ߺ����� �߰��Ǵ� ��� (1�� -> 2�� ��) ���� ȣ����� ����!
    /// </summary>
    protected virtual void OnItemAdded()
	{

	}
}

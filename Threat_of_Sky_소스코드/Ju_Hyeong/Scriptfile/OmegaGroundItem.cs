using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OmegaGroundItem : MonoBehaviour
{
    private float ElapsedTime;
    private float OriginPosY;

    [SerializeField, Tooltip("아이템 이름")]
    private string ItemName = "";

    [SerializeField, Tooltip("아이템 텍스쳐")]
    private Image ItemImage;
    public void SetItemName(string ItemName) { this.ItemName = ItemName; ItemImage.sprite = CharacterGameplay.CharacterGameplayManager.Instance.ActiveItemDictionary[ItemName].GetItemSprite; }
    //public void SetItemName(string ItemName) { this.ItemName = ItemName; ItemSprite = CharacterGameplay.CharacterGameplayManager.Instance.StatusItemDictionary[ItemName].GetItemSprite; }

    private void OnTriggerEnter(Collider other)
    {
        //other.gameObject.GetComponent<ACharacterBase>().AddStatusItem()
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ACharacterBase>().SetActiveItem(ItemName);

            Debug.Log("!!!!");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        OriginPosY = this.transform.position.y;
    }

    private void Update()
    {
        ElapsedTime += Time.deltaTime;
        var Temp = transform.position;
        Temp.y = OriginPosY + Mathf.Sin(ElapsedTime) * 0.3f;
        transform.position = Temp;
    }
}

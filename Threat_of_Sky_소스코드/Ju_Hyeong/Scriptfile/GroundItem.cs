using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class GroundItem : MonoBehaviourPun
{
    //�����̴� �����ۿ� �ʿ��� ����
    private float ElapsedTime;
    private float OriginPosY;

    [SerializeField, Tooltip("������ �̸�")]
    private string ItemName = "";

    [SerializeField, Tooltip("������ �ؽ���")]
    private Image ItemImage;
    public void SetItemName(string ItemName) { this.ItemName = ItemName; ItemImage.sprite = CharacterGameplay.CharacterGameplayManager.Instance.StatusItemDictionary[ItemName].GetItemSprite; }
    //public void SetItemName(string ItemName) { this.ItemName = ItemName; ItemSprite = CharacterGameplay.CharacterGameplayManager.Instance.StatusItemDictionary[ItemName].GetItemSprite; }

    private void OnTriggerEnter(Collider other)
    {
        //other.gameObject.GetComponent<ACharacterBase>().AddStatusItem()
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ACharacterBase>().AddStatusItem(CharacterGameplay.CharacterGameplayManager.Instance.StatusItemDictionary[ItemName]);

            Debug.Log("!!!!");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        OriginPosY = this.transform.position.y;

        Destroy(gameObject, 30.0f);
    }


    private void Update()
    {
        ElapsedTime += Time.deltaTime;
        var Temp = transform.position;
        Temp.y = OriginPosY + Mathf.Sin(ElapsedTime) * 0.3f;
        transform.position = Temp;
    }

}

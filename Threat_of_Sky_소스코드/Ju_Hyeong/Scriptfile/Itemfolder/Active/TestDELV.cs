using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDELV : MonoBehaviour
{
    
    // ���� ������Ʈ : �ɸ����� ���� ������ ������ �����Ѵ�.

    [SerializeField]
    private AActiveItemBase activeItemBase;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<ACharacterBase>(out ACharacterBase aCharacter))
        {
            //activeItemBase.GetItemName();
            //aCharacter.~~(activeItemBase);
            Destroy(gameObject);
        }
    }
}

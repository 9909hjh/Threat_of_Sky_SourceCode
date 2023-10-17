using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDELV : MonoBehaviour
{
    
    // 삭제 오브젝트 : 케릭터의 값을 전달한 다음에 삭제한다.

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

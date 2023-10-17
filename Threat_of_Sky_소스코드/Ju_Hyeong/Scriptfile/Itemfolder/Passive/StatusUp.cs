using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 작성자: 20181289 한주형
 * 아이템을 결정하여 사용하는 스크립트
 */

public class StatusUp : AStatusItemBase
{
    private void OnTriggerEnter(Collider other)
    {
        //아이템을 중복 될 때 처리 넣어야 하나.
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

    }
}

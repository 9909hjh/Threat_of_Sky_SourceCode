using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * �ۼ���: 20181289 ������
 * �������� �����Ͽ� ����ϴ� ��ũ��Ʈ
 */

public class StatusUp : AStatusItemBase
{
    private void OnTriggerEnter(Collider other)
    {
        //�������� �ߺ� �� �� ó�� �־�� �ϳ�.
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

    }
}

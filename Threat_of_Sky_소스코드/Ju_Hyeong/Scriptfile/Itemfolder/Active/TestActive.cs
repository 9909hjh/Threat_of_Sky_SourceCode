using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TestActive : AActiveItemBase
{
    public GameObject missile;

    //private void OnTriggerEnter(Collider other)
    //{
    //    //�������� �ߺ� �� �� ó�� �־�� �ϳ�.
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        //bIsUsableItem = true;
    //        //other.gameObject.GetComponent<>
    //        Destroy(gameObject);
    //    }
    //}

    // �̷��� ���� ����� �� ���ӿ�����Ʈ�� �־� "������"���� ����� �ȴ�. �׷��� ������� activeItemBase�� ������ �۵� ��ų �� �ִ�.
    // ���⼭�� ��ɸ� ����� ����.
    public override void UseItem()
    {
        //�̻����� ������� �� ���⿡ �̻����� �����ϵ��� ����.
        Instantiate(missile, ownerCharacter.transform.position + (Vector3.up * 5), ownerCharacter.transform.rotation);
        //���濡���� �����Ʈ��ũ.Instantiate�ϸ� �ȴ�.

        //ownerCharacter.RestoreHealth(~~~); �÷��̾� ä�� ��������
        //ownerCharacter.AddBuff(); <- �ڷ� �̷���

        // ���� ���� �̻��� �����

        //float a = ownerCharacter.GetStatusComponent().GetFinalDamage().Item1; // ĳ���� ����� ��������
        //�̻���(damage a * 2); <- �̻����� ������� �̻����� �������� ������ �������� �ȴ�.
        // ���

        //return base.UseItem();
        //return bIsUsableItem;
    }


}

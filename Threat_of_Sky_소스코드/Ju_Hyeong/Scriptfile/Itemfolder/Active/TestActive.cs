using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TestActive : AActiveItemBase
{
    public GameObject missile;

    //private void OnTriggerEnter(Collider other)
    //{
    //    //아이템을 중복 될 때 처리 넣어야 하나.
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        //bIsUsableItem = true;
    //        //other.gameObject.GetComponent<>
    //        Destroy(gameObject);
    //    }
    //}

    // 이렇게 만든 기능은 빈 게임오브젝트에 넣어 "프리팹"으로 만들면 된다. 그래서 만든것을 activeItemBase에 넣으면 작동 시킬 수 있다.
    // 여기서는 기능만 만들게 하자.
    public override void UseItem()
    {
        //미사일을 만들려고 함 여기에 미사일을 스폰하도록 하자.
        Instantiate(missile, ownerCharacter.transform.position + (Vector3.up * 5), ownerCharacter.transform.rotation);
        //포톤에서는 포톤네트워크.Instantiate하면 된다.

        //ownerCharacter.RestoreHealth(~~~); 플레이어 채력 가져오기
        //ownerCharacter.AddBuff(); <- 뒤로 미루자

        // 스폰 추적 미사일 만들기

        //float a = ownerCharacter.GetStatusComponent().GetFinalDamage().Item1; // 캐릭터 대미지 가져오기
        //미사일(damage a * 2); <- 미사일을 만들려면 미사일의 데미지를 넣으서 내보내면 된다.
        // 기능

        //return base.UseItem();
        //return bIsUsableItem;
    }


}

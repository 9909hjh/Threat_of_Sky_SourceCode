using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerGetMoney : MonoBehaviourPun
{
	//public ACharacterBase ownerCharacter;

	[SerializeField]
	private float getMoney;


	private void Start()
	{
		getMoney *= (0.85f * GameManager.Instance.GetCurrentLevel);
	}


	private void OnTriggerEnter(Collider other)
	{
		//아이템을 중복 될 때 처리 넣어야 하나.
		if (other.gameObject.CompareTag("Player"))
		{
			other.gameObject.GetComponent<ACharacterBase>().AddMoney(getMoney); // 사용하려면 이거를 쓰면 된다.

			//other.gameObject.GetComponent<TestCh>().AddMoney(getMoney);

			Destroy(gameObject);
		}
	}
}

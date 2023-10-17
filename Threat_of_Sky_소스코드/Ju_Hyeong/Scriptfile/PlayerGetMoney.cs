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
		//�������� �ߺ� �� �� ó�� �־�� �ϳ�.
		if (other.gameObject.CompareTag("Player"))
		{
			other.gameObject.GetComponent<ACharacterBase>().AddMoney(getMoney); // ����Ϸ��� �̰Ÿ� ���� �ȴ�.

			//other.gameObject.GetComponent<TestCh>().AddMoney(getMoney);

			Destroy(gameObject);
		}
	}
}

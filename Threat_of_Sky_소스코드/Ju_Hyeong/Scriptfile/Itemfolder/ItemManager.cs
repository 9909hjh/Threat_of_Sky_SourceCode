using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using CharacterGameplay;
using TMPro;

/**
 * 20181298 한주형 아이템 렌덤(가중치)뽑기 스크립트
 * 수정: 20181220 이성수
 */
public class ItemManager : MonoBehaviourPun, IInteractableObject
{
	private enum Card { COMMON, ADVANCED, SPECIAL, UNIQUE, UNREAL, ULTRON, OMEGA }
	private Dictionary<Card, int> m_Cards = new Dictionary<Card, int>();
	private Card m_Card = Card.COMMON;

	[SerializeField]
	private GroundItem groundItem;

	[SerializeField]
	private OmegaGroundItem OmegagroundItem;

	[SerializeField]
	private float TakeMoney;

	[SerializeField]
	private bool TryTakeMoeny = false;

	[SerializeField]
	private bool bTryOpen = true;

	[SerializeField]
	private Transform Openbox;

	private float X_up = 45.0f;

	//private float lerpTime = 45.0f;


	[SerializeField]
	private TMP_Text PriceText;



	void Start()
	{
		m_Cards.Add(Card.COMMON, 45);
		m_Cards.Add(Card.ADVANCED, 34);
		m_Cards.Add(Card.SPECIAL, 12);
		m_Cards.Add(Card.UNIQUE, 7);
		m_Cards.Add(Card.UNREAL, 5);
		m_Cards.Add(Card.ULTRON, 1);
		m_Cards.Add(Card.OMEGA, 10);
	}

	void Update()
	{
		//StartCoroutine(DelayOpenBox());
		//TestFuc();
		//if (Input.GetKeyDown(KeyCode.B))
		//    SpawnItem();
	}

	//[PunRPC]
	void TestFuc()
	{
		//if (bTryOpen /*&& TryTakeMoeny*/)
		//{
		//    X_up -= Time.deltaTime * 30.0f;
		//    Openbox.rotation = Quaternion.Euler(X_up, 0, 0);
		//    if(X_up <= -10.0f)
		//    {
		//        bTryOpen = false;
		//    }
		//}
	}

	void IInteractableObject.Interact(int InteractingCharacterViewID)
	{
		ACharacterBase InteractingCharacter = PhotonView.Find(InteractingCharacterViewID).gameObject.GetComponent<ACharacterBase>();

		if (InteractingCharacter != null && bTryOpen)
		{
			TryTakeMoeny = InteractingCharacter.TryUseMoney(TakeMoney);

			SpawnItem();

			if (TryTakeMoeny)
				PriceText.gameObject.SetActive(false);
		}
	}

	public void SetPositionToGround()
	{
		photonView.RPC("RpcSetPositionToGroundMaster", RpcTarget.All);

	}

	[PunRPC]
	protected void RpcSetPositionToGroundMaster()
	{
		Physics.Raycast(transform.position, Vector3.down, out RaycastHit HitInfo, 100.0f, 1 << LayerMask.NameToLayer("Ground"), QueryTriggerInteraction.Ignore);

		photonView.RPC("RpcSetPositionToGroundAll", RpcTarget.All, HitInfo.point != Vector3.zero ? HitInfo.point : transform.position);
	}

	[PunRPC]
	protected void RpcSetPositionToGroundAll(Vector3 NewPosition)
	{
		transform.position = NewPosition;
		transform.rotation = Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f);

		TakeMoney *= (0.45f * GameManager.Instance.GetCurrentLevel);
		PriceText.text = TakeMoney.ToString();
	}


	//     void Update()
	//     {
	//         SpawnItem();
	//     }

	void SpawnItem()
	{
		//Debug.Log("ghkrdls");
		if (TryTakeMoeny == true) // 여기에는 플레이어의 돈을 계산해서 크거나 같으면 계산된 돈을 빼기로 하자.
		{
			bTryOpen = false;
			//선택
			//Debug.Log("ghkrdls");
			m_Card = WeightedRandomizer.From(m_Cards).TakeOne();
			//여기에 

			string selecteditem = "";
			if (m_Card == Card.OMEGA)
			{
				selecteditem = CharacterGameplayHelper.GetRandomActiveItemName();
				OmegagroundItem.SetItemName(selecteditem);

				StartCoroutine(OmegaDelayOpenBox()); // 스폰에 딜레이 있도록


				CharacterGameplayHelper.PlayVfx(CharacterGameplayHelper.GetItemBoxItemSpawnVfx((int)m_Card), transform.position + Vector3.up, Quaternion.identity);

				SoundManager.Instance.SpawnSoundAtLocation("OpenItemBox", transform.position, ESoundGroup.SFX, 1.0f, 0.02f);

				return;
			}

			if (m_Card == Card.COMMON)
			{
				selecteditem = CharacterGameplayHelper.GetRandomItemNameByRating(ItemRating.COMMON);
			}
			else if (m_Card == Card.ADVANCED)
			{
				selecteditem = CharacterGameplayHelper.GetRandomItemNameByRating(ItemRating.ADVANCED);
			}
			else if (m_Card == Card.SPECIAL)
			{
				selecteditem = CharacterGameplayHelper.GetRandomItemNameByRating(ItemRating.SPECIAL);
			}
			else if (m_Card == Card.UNIQUE)
			{
				selecteditem = CharacterGameplayHelper.GetRandomItemNameByRating(ItemRating.UNIQUE);
			}
			else if (m_Card == Card.UNREAL)
			{
				selecteditem = CharacterGameplayHelper.GetRandomItemNameByRating(ItemRating.UNREAL);
			}
			else if (m_Card == Card.ULTRON)
			{
				selecteditem = CharacterGameplayHelper.GetRandomItemNameByRating(ItemRating.ULTRON);

				SoundManager.Instance.SpawnSoundAtLocation("ItemAppeared_ULTRON", transform.position, ESoundGroup.SFX, 1.0f, 0.0f);
			}

			//아이템을 이름을 전달해주는 코드
			groundItem.SetItemName(selecteditem);

			StartCoroutine(DelayOpenBox()); // 스폰에 딜레이 있도록


			// 파티클 스폰
			CharacterGameplayHelper.PlayVfx(CharacterGameplayHelper.GetItemBoxItemSpawnVfx((int)m_Card), transform.position + Vector3.up, Quaternion.identity);

			SoundManager.Instance.SpawnSoundAtLocation("OpenItemBox", transform.position, ESoundGroup.SFX, 1.0f, 0.02f);

		}
	}

	//패시브 아이템 스폰
	IEnumerator DelayOpenBox()
	{
		while (X_up >= -20.0f)
		{
			yield return null;
			X_up -= Time.deltaTime * 55.0f;
			Openbox.localRotation = Quaternion.Euler(X_up, 0, 0);
		}

		PhotonNetwork.Instantiate("GroundItem", transform.position + (Vector3.up * 1.5f), transform.rotation);
		//bTryOpen = false;
		// yield return new WaitForSeconds(2f);
	}

	//액티브 아이템 스폰
	IEnumerator OmegaDelayOpenBox()
	{
		while (X_up >= -20.0f)
		{
			yield return null;
			X_up -= Time.deltaTime * 40.0f;
			Openbox.localRotation = Quaternion.Euler(X_up, 0, 0);
		}
		PhotonNetwork.Instantiate("OmegaGroundItem", transform.position + (Vector3.up * 1.5f), transform.rotation);
		//bTryOpen = false;
		// yield return new WaitForSeconds(2f);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			if (Input.GetKeyDown(KeyCode.U))
			{
				//TryTakeMoeny = collision.gameObject.GetComponent<ACharacterBase>().TryUseMoney(TakeMoney); // 사용하려면 이거를 쓰면 된다.
				TryTakeMoeny = other.gameObject.GetComponent<TestCh>().TryUseMoney(TakeMoney);
				Debug.Log(TryTakeMoeny);
			}
			//collision.gameObject.GetComponent<TestCh>().AddMoney(getMoney);
			//Destroy(gameObject);
		}
	}

	public static class WeightedRandomizer
	{
		public static WeightedRandomizer<R> From<R>(Dictionary<R, int> spawnRate)
		{
			return new WeightedRandomizer<R>(spawnRate);
		}
	}

	public class WeightedRandomizer<T>
	{
		private static System.Random _random = new System.Random();
		private Dictionary<T, int> _weights;

		public WeightedRandomizer(Dictionary<T, int> weights)
		{
			_weights = weights;
		}

		public T TakeOne()
		{
			var sortedSpawnRate = Sort(_weights);
			int sum = 0;
			foreach (var spawn in _weights)
			{
				sum += spawn.Value;
			}

			int roll = _random.Next(0, sum);

			T selected = sortedSpawnRate[sortedSpawnRate.Count - 1].Key;
			foreach (var spawn in sortedSpawnRate)
			{
				if (roll < spawn.Value)
				{
					selected = spawn.Key;
					break;
				}
				roll -= spawn.Value;
			}

			return selected;
		}

		private List<KeyValuePair<T, int>> Sort(Dictionary<T, int> weights)
		{
			var list = new List<KeyValuePair<T, int>>(weights);

			list.Sort(
				delegate (KeyValuePair<T, int> firstPair,
							KeyValuePair<T, int> nextPair)
				{
					return firstPair.Value.CompareTo(nextPair.Value);
				}
				);

			return list;
		}
	}
}

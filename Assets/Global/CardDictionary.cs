using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardDictionary : MonoBehaviour {

	const int DRAFT_CARD_NUMBER = 3;

	static CardDictionary instance;

	void Awake() {
		instance = this;
	}

	public static CardDictionary Instance {
		get { return instance; }
	}

	public static Card[] GetRandomCards() {

		List<Card> result = new List<Card> ();

		Card[] cards = Instance.transform.GetComponentsInChildren<Card>();

		for (int i = 0; i < DRAFT_CARD_NUMBER; i++) {
			int randomIndex = Random.Range (0, cards.Length);

			result.Add (cards[randomIndex]);
		}

		return result.ToArray();
	}
}

using UnityEngine;
using System.Collections;

public class CardDraftPool : MonoBehaviour {

	static CardDraftPool instance;

	public int pickingPlayer = 0;

	void Awake() {
		instance = this;
	}

	public static CardDraftPool Instance {
		get { return instance; }
	}

	public void ClearCards() {
		var cards = GetComponentsInChildren<Card> (true);

		foreach (Card c in cards) {
			GameObject.Destroy (c.gameObject);
		}
	}

	public void PutCard(Card card) {
		card.transform.SetParent(transform);
	}
}

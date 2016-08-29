using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CardContainer : MonoBehaviour {

	public int SelectedCardsCount {
		get { return GetComponentsInChildren<Card> ().Count ((x) => {
				return x.Selected == true;
			}); }
	}

	public Card[] SelectedCards {
		get { return GetComponentsInChildren<Card> ().Where ((x) => {
			return x.Selected == true;
		}).ToArray(); }
	}

	public Card[] GetDefaultCards ()
	{
		return GetComponentsInChildren<Card> ();
	}

	public int CardsCount {
		get { return GetComponentsInChildren<Card> ().Length; }
	}

	public void PutCard(Card card) {
		card.transform.SetParent(transform);
	}

	public void ClearCards() {
		var cards = GetComponentsInChildren<Card> (true);

		foreach (Card c in cards) {
			GameObject.Destroy (c.gameObject);
		}
	}

	public Card[] GetRandomCards(int count) {

		List<Card> result = new List<Card> ();

		Card[] cards = transform.GetComponentsInChildren<Card>();

		for (int i = 0; i < count; i++) {
			int randomIndex = Random.Range (0, cards.Length);

			result.Add (cards[randomIndex]);
		}

		return result.ToArray();
	}
}

using UnityEngine;
using System.Collections;

public class CardDraftHandler : MonoBehaviour {

	public CardDeck ownedDeck;
	public CardDeck enemyDeck;

	public int draftingPlayer = 0;

	public CardDeck[] playerDecks; 

	void OnEnable() {
		EventBus.OnCardClickedEvent += EventBus_OnCardClickedEvent;
	}

	void OnDisable() {
		EventBus.OnCardClickedEvent -= EventBus_OnCardClickedEvent;
	}

	void EventBus_OnCardClickedEvent (Card card)
	{
		if (ownedDeck.CardsCount < GameConfig.Instance.deckHalfSize) {
			ownedDeck.PutCard (card);
		} else if (enemyDeck.CardsCount < GameConfig.Instance.deckHalfSize) {
			enemyDeck.PutCard (card);
		}

		if (ownedDeck.CardsCount >= GameConfig.Instance.deckHalfSize
		    && enemyDeck.CardsCount >= GameConfig.Instance.deckHalfSize) {

			PutSelectedCardsToDecks ();

			if (draftingPlayer == 0) {
				draftingPlayer = 1;
				GameController.Instance.ShowNextCards ();
			} else if (draftingPlayer == 1) {
				GameController.Instance.FinishDraft ();
			}
		} else {
			GameController.Instance.ShowNextCards ();
		}

	}

	void PutSelectedCardsToDecks() {
		int ownerIndex = 0;
		int enemyIndex = 1;

		if (draftingPlayer == 1) {
			ownerIndex = 1;
			enemyIndex = 0;
		}

		Card[] ownedCards = ownedDeck.GetComponentsInChildren<Card> ();
		Card[] enemyCards = enemyDeck.GetComponentsInChildren<Card> ();

		foreach (Card c in ownedCards) {
			playerDecks [ownerIndex].PutCard (c);
		}

		foreach (Card c in enemyCards) {
			playerDecks [enemyIndex].PutCard (c);
		}
	}
}

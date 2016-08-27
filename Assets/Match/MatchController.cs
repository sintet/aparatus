using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class MatchController : MonoBehaviour {

	public CardDeck[] playerDecks;
	public CardHand[] playerHands;
	public PlayerActor[] playerActors;

	Queue<Card> cardsQueue = new Queue<Card> ();

	enum MatchPhase {
		Planning,
		Resolving
	}
		
	MatchPhase matchPhase;

	void OnEnable() {
		EventBus.OnCardClickedEvent += OnCardClickedEvent;
	}

	void OnDisable() {
		EventBus.OnCardClickedEvent -= OnCardClickedEvent;
	}

	public void StartMatch() {
		DealCards ();
	}

	public void DealCards() {
		for (int i = 0; i < 2; i++) {
			playerHands[i].ClearCards ();

			var cards = playerDecks [i].GetRandomCards (5);

			foreach (Card c in cards) {
				Card createdCard = GameObject.Instantiate<Card> (c);
				playerHands [i].PutCard (createdCard);
			}
		}
	}

	void OnCardClickedEvent (Card card)
	{
		if (card.GetComponentInParent<CardContainer> ().SelectedCardsCount < GameConfig.Instance.actionsLimit) {
			card.Selected = true;
		}

		TryFinishPlanning ();
	}

	void TryFinishPlanning() {
		if (playerHands [0].SelectedCardsCount >= GameConfig.Instance.actionsLimit
		    && playerHands [1].SelectedCardsCount >= GameConfig.Instance.actionsLimit) {
			EnqueueActions ();
		}
	}

	void EnqueueActions() {
		Card[] selectedCards0 = playerHands [0].SelectedCards;
		Card[] selectedCards1 = playerHands [1].SelectedCards;

		cardsQueue.Clear ();

		for (int i = 0; i < GameConfig.Instance.actionsLimit; i++) {
			// Attack/Defence
			cardsQueue.Enqueue(selectedCards0[i]);
			cardsQueue.Enqueue(selectedCards1[i]);
		}

		StartCoroutine (ApplyActionsQueued());
	}

	IEnumerator ApplyActionsQueued() {
		while (cardsQueue.Count > 0) {
			Card c = cardsQueue.Dequeue ();
			CardHand hand = c.GetComponentInParent<CardHand> ();
			yield return c.ApplyActionsRoutine (playerActors [hand.playerIndex], playerActors [1 - hand.playerIndex]);
		}

		DealCards ();
	}
}

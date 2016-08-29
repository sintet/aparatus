using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class MatchController : MonoBehaviour {

	public CardDeck[] playerDecks;
	public CardHand[] playerHands;
	public PlayerActor[] playerActors;

	public int attackingPlayer = 1; 

	Queue<Card> cardsQueue = new Queue<Card> ();

	enum MatchPhase {
		Planning,
		Resolving
	}
		
	MatchPhase matchPhase;

	static MatchController instance;

	void Awake() {
		instance = this;
	}

	public static MatchController Instance {
		get { return instance; }
	}

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

			List<Card> cards = new List<Card>(playerDecks [i].GetDefaultCards());

			var specialCards = playerDecks [i].GetRandomCards (2);

			cards.AddRange (specialCards);

			Shuffle (cards);

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
			if (attackingPlayer == 0) {
				cardsQueue.Enqueue (selectedCards0 [i]);
				cardsQueue.Enqueue (selectedCards1 [i]);
			} else if (attackingPlayer == 1) {
				cardsQueue.Enqueue (selectedCards1 [i]);
				cardsQueue.Enqueue (selectedCards0 [i]);
			}
		}

		StartCoroutine (ApplyActionsQueued());
	}

	IEnumerator ApplyActionsQueued() {
		while (cardsQueue.Count > 0) {
			Card c = cardsQueue.Dequeue ();
			CardHand hand = c.GetComponentInParent<CardHand> ();
			yield return c.ApplyActionsRoutine (playerActors [hand.playerIndex], playerActors [1 - hand.playerIndex]);
		}

		CheckWinCondition ();

		DealCards ();

		attackingPlayer = 1 - attackingPlayer;
	}

	void CheckWinCondition ()
	{
		int c = 0;
		int winner = -1;

		foreach (var p in playerActors) {
			if (p.health <= 0) {
				winner = p.playerIndex;
				c++;
			}
		}

		if (c >= 2) {
			FinishGameWithDraw ();
		} else {
			FinishGameWithWinner (winner);
		}
	}

	[ContextMenu("Draw!")]
	void FinishGameWithDraw ()
	{
		for (int i = 0; i < 2; i++) {
			playerHands [i].ClearCards ();
		}
		FindObjectOfType<MatchControllerUI> ().ShowEndScreen (-1);
	}

	[ContextMenu("Winner0")]
	void TestWinner1() {
		FinishGameWithWinner (0);
	}

	void FinishGameWithWinner (int winner)
	{
		for (int i = 0; i < 2; i++) {
			playerHands [i].ClearCards ();
		}
		FindObjectOfType<MatchControllerUI> ().ShowEndScreen (winner);
	}

	public void Shuffle(List<Card> list)  
	{  
		System.Random rng = new System.Random ();

		int n = list.Count;
		while (n > 1) {  
			n--;  
			int k = rng.Next(n + 1);
			Card value = list[k];  
			list[k] = list[n];  
			list[n] = value;  
		}
	}
}

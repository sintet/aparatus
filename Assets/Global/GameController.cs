using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	//public int draftPlayer;
	//public int pickedCardsCount;

	public CardDraftPool cardDraftPool;
	public GameObject draftScreen;
	public GameObject matchScreen;

	static GameController instance;

	void Awake() {
		instance = this;
	}

	public static GameController Instance {
		get { return instance; }
	}

	void Start() {
		StartDraft ();
	}

	[ContextMenu("ShowNextCards")]
	public void ShowNextCards() {
		cardDraftPool.ClearCards ();

		Card[] cards = CardDictionary.GetRandomCards ();

		foreach (var card in cards) {
			Card createdCard = GameObject.Instantiate<Card> (card);
			cardDraftPool.PutCard (createdCard);
		}
	}

	public void StartDraft() {
		draftScreen.SetActive(true);
		matchScreen.SetActive(false);

		ShowNextCards ();
	}

	public void FinishDraft() {

		draftScreen.SetActive(false);
		matchScreen.SetActive(true);

		FindObjectOfType<MatchController> ().StartMatch ();
	}
}

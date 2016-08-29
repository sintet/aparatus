using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Card : MonoBehaviour {
	[SerializeField]
	bool selected = false;
	public string title = "Untitled Card"; 

	public CardAction[] actions;

	public void OnCardClicked() {
		EventBus.OnCardClicked (this);
	}

	public bool Selected {
		get { return this.selected; }
		set { this.selected = value; }
	}

	void FixedUpdate() {
		if (selected) {
			transform.Find ("Image").GetComponent<Image> ().color = GameConfig.Instance.selectedCardColor;
		} else {
			transform.Find ("Image").GetComponent<Image> ().color = Color.white;
		}
	}

	public void ApplyActions (PlayerActor owner, PlayerActor target)
	{
		StartCoroutine (ApplyActionsRoutine (owner, target));
	}

	public IEnumerator ApplyActionsRoutine(PlayerActor owner, PlayerActor target) {

		//Debug.Log ("Apply");

		int ownerIndex = GetComponentInParent<CardHand> ().playerIndex;

		foreach (var a in actions) {
			var createdAction = GameObject.Instantiate<CardAction> (a);
			createdAction.Apply (owner, target);

			while (createdAction.done == false) {
				yield return new WaitForFixedUpdate();
			}

			GameObject.Destroy (createdAction.gameObject);
		}
	}

	void Update() {
		if (Application.isEditor && Application.isPlaying == false) {
			transform.Find ("Header/Text").GetComponent<Text> ().text = title;
			gameObject.name = title;
		}
	}
}

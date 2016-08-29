using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerActorUI : MonoBehaviour {

	[SerializeField]
	PlayerActor playerActor;

	Slider slider; 
	GameObject attackIndicator;

	void Awake() {
		slider = transform.Find("Slider").GetComponent<Slider> ();
		attackIndicator = transform.Find ("AttackIndicator").gameObject;
	}

	void FixedUpdate () {
		slider.value = playerActor.health;
		attackIndicator.SetActive(MatchController.Instance.attackingPlayer == playerActor.playerIndex);
	}
}

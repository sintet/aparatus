using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class EventBus : MonoBehaviour {

	public static event Action<Card> OnCardClickedEvent;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void OnCardClicked (Card card) {
		if (OnCardClickedEvent != null) {
			OnCardClickedEvent.Invoke (card);
		}
	}


}

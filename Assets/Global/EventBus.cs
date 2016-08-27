using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class EventBus : MonoBehaviour {

	static event Action<Card> OnCardClickedEvent;

	// Use this for initialization
	void Start () {
		OnCardClickedEvent += OnCardClicked;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void OnCardClicked (Card card) {
		Debug.Log ("HELLO");
	}


}

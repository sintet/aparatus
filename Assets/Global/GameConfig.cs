﻿using UnityEngine;
using System.Collections;

public class GameConfig : MonoBehaviour {

	static GameConfig instance;

	void Awake() {
		instance = this;
	}

	public static GameConfig Instance {
		get { return instance; }
	}

	public int deckHalfSize = 12;
	public int actionsLimit = 3;
	public int upperlane = 2;
	public Color selectedCardColor;
	public GameObject canonball;
	public int canonDamage = 2;
}

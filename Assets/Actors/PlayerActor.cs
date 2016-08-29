using UnityEngine;
using System.Collections;

public class PlayerActor : MonoBehaviour {

	public int playerIndex;
	public int currentLane = 0;
	public int health = 16;

	public Transform Weapon {
		get { return transform.Find ("Weapon"); }
	}

	public void DealDamage(int amount) {
		health -= amount;
	}
}
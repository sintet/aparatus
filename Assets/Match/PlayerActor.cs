using UnityEngine;
using System.Collections;

public class PlayerActor : MonoBehaviour {

	public int playerIndex;

	public Transform Weapon {
		get { return transform.Find ("Weapon"); }
	}
}
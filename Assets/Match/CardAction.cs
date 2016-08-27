using UnityEngine;
using System.Collections;

public abstract class CardAction : MonoBehaviour {
	
	public bool done = false;

	abstract public void Apply (PlayerActor owner, PlayerActor target);
}

using UnityEngine;
using System.Collections;
using DG.Tweening;


public class MoveAction : CardAction {

	public int amount;

	public override void Apply (PlayerActor owner, PlayerActor target)
	{
		done = false;
		DOTweenAnimation anim = GetComponent<DOTweenAnimation> ();

		int newLane = Mathf.Clamp (owner.currentLane + amount, 0, GameConfig.Instance.upperlane);
			
		if (owner.currentLane == newLane) {
			owner.transform.DOJump (owner.transform.position, 1f, 1, anim.duration / 3f).OnComplete(() => {
				done = true; 
			});
		} else {
			Vector3 location = FindObjectOfType<Battleground> ().GetLanePosition (owner.playerIndex, newLane);
			owner.transform.DOMove (location, anim.duration).SetEase (anim.easeType, 0.1f).OnComplete (() => {
				owner.currentLane = newLane;
				done = true;
			});
	
		}
	}


}

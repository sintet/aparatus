using UnityEngine;
using System.Collections;
using DG.Tweening;


public class MoveAction : CardAction {

	public Lane lane;

	public override void Apply (PlayerActor owner, PlayerActor target)
	{
		DOTweenAnimation anim = GetComponent<DOTweenAnimation> ();
		Debug.Log ("Apply" + this.gameObject.name);
		done = false;
		Vector3 location = FindObjectOfType<Battleground> ().GetLanePosition (owner.playerIndex, lane);
		owner.transform.DOMove(location, anim.duration).SetEase(anim.easeType, 0.1f).OnComplete(() => { Debug.Log("DONE"); done = true; });
	}
		


}

using UnityEngine;
using System.Collections;
using DG.Tweening;


public class ShootAction : CardAction {

	public Lane lane;

	public override void Apply (PlayerActor owner, PlayerActor target)
	{
		done = false;
		DOTweenAnimation anim = GetComponent<DOTweenAnimation> ();
		Vector3 location = FindObjectOfType<Battleground> ().GetLanePosition (target.playerIndex, lane);

		Quaternion rotation = Quaternion.LookRotation (location - owner.transform.position);
		Debug.Log (rotation);
		owner.Weapon.DORotate(rotation.eulerAngles, anim.duration).SetEase(anim.easeType, 0.1f).OnComplete(() => { Debug.Log("DONE"); done = true; });
	
		GameObject cannonball = GameObject.Instantiate (GameConfig.Instance.canonball);
	}
		


}

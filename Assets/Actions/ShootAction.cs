using UnityEngine;
using System.Collections;
using DG.Tweening;


public class ShootAction : CardAction {

	public int offset;
	public float projectileFlyDuration = 0.2f;
	public Ease projectileFlyEase;

	public override void Apply (PlayerActor owner, PlayerActor target)
	{
		done = false;
		DOTweenAnimation anim = GetComponent<DOTweenAnimation> ();

		int targetLane = Mathf.Clamp (owner.currentLane + offset, -1, GameConfig.Instance.upperlane + 1);

		Vector3 location = FindObjectOfType<Battleground> ().GetLanePosition (target.playerIndex, targetLane);

		Quaternion rotation = Quaternion.LookRotation (location - owner.transform.position);
		Debug.Log (rotation);

		owner.Weapon.DORotate(rotation.eulerAngles, anim.duration).SetEase(anim.easeType, 0.1f).OnComplete(() => {
			Canonball canonball = GameObject.Instantiate (GameConfig.Instance.canonball).GetComponent<Canonball>();
			canonball.owner = owner;
			canonball.parentAction = this;
			canonball.transform.position = owner.GetFirePoint();
			canonball.transform.DOMove (location, projectileFlyDuration).SetEase(projectileFlyEase).OnComplete(() => {
				Destroy(canonball.gameObject);
				done = true; 
			});

		});


	}
		


}

using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Canonball : MonoBehaviour {

	public PlayerActor owner;
	public CardAction parentAction;

	void OnTriggerEnter2D(Collider2D collider) {
		PlayerActor playerActor = collider.transform.parent.GetComponent<PlayerActor>();
		if (playerActor != null && playerActor != owner) {
			playerActor.DealDamage (GameConfig.Instance.canonDamage);
			Destroy (gameObject);
			playerActor.transform.DOShakePosition (1f, 0.2f).OnComplete(() => {
				parentAction.done = true;
			});
		}
	}
}

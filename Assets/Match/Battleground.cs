using UnityEngine;
using System.Collections;

public class Battleground : MonoBehaviour {

	public Vector3 GetLanePosition(int playerIndex, Lane lane) {
		return transform.Find(string.Format("Player{0}Lanes/{1}", playerIndex, lane.ToString())).position;
	}
}
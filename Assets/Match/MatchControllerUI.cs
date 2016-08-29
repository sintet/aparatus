using UnityEngine;
using System.Collections;

public class MatchControllerUI : MonoBehaviour {

	public void ShowEndScreen(int winner) {
		transform.Find ("EndScreen").gameObject.SetActive (true);
		transform.Find ("EndScreen/Player0WinText").gameObject.SetActive (winner == 0);
		transform.Find ("EndScreen/Player1WinText").gameObject.SetActive (winner == 1);
		transform.Find ("EndScreen/DrawText").gameObject.SetActive (winner == -1);
	}

	public void GoToMainMenu() {
		Application.LoadLevel ("Menu");
	}
}

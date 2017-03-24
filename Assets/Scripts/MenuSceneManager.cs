using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuSceneManager : MonoBehaviour {

	private bool onStart;
	private Vector3 iconMoveDistance;

	public GameObject menuIcon;

	// Use this for initialization
	void Start() {
		onStart = true;
		iconMoveDistance = new Vector3(0f, 56f, 0f);
	}

	// Update is called once per frame
	void Update() {
		MoveMenuIcon();
		QuitOrStartGame(onStart);
	}

	void MoveMenuIcon() {
		if (onStart && Input.GetKeyDown("down")) {
			onStart = false;
			menuIcon.transform.position -= iconMoveDistance;
		}
		if (!onStart && Input.GetKeyDown("up")) {
			onStart = true;
			menuIcon.transform.position += iconMoveDistance;
		}
	}

	void QuitOrStartGame(bool onStart) {
		if (Input.GetKeyDown("return") && SceneManager.GetActiveScene().buildIndex == 0) {
			if (onStart) {
				SceneManager.LoadScene("GameLevel");
				ScoreKeeper.score = 0; 
			} else {
				SceneManager.LoadScene("ExitGame");
			}
		}
	}

	public void LoadStartMenu() {
		SceneManager.LoadScene("Menu");
	}

	public void LoadExitScene() {
		SceneManager.LoadScene("ExitGame");
	}

	public void LoadEndScene() {
		SceneManager.LoadScene("EndGame");
	}

	public void ApplicationQuit() {
		Application.Quit();
	}
}

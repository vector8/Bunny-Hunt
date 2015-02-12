using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {
	public GameObject TutorialPanel;

	public void GameStart(){
		Application.LoadLevel("Main");
	}

	public void Tutorial(){
		TutorialPanel.SetActive(true);
	}

	public void Back2Title(){
		TutorialPanel.SetActive(false);
	}

	public void ExitGame(){
		Application.Quit();
	}
}

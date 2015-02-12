using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pause : MonoBehaviour {
	private bool pauseGame;
	private bool showGUI;
	public GameObject pause;
	public GameObject pauseGUI;

	public void ResumeGame(){
		pauseGUI.SetActive(false);
		pause.SetActive(true);
		Time.timeScale=1;
		pauseGame = false;

	}

	public void PauseGame()
	{
		pauseGame = true;
		Time.timeScale=0;
		pause.SetActive(false);
		pauseGUI.SetActive(true);
	}

	public bool isGamePasued(){
		return pauseGame;
	}

	public void ExitGame(){
		Application.LoadLevel("TitleScreen");
	}
}

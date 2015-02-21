using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pause : MonoBehaviour {
	private bool pauseGame;
	private bool showGUI;
	
	public PlayerController player;
	public GameObject pause;
	public GameObject pauseGUI;

	public void ResumeGame()
	{
		pauseGUI.SetActive(false);
		pause.SetActive(true);
		player.freeze();
		Time.timeScale=1;
		pauseGame = false;
	}

	public void PauseGame()
	{
		pauseGame = true;
		player.freeze();
		Time.timeScale=0;
		pause.SetActive(false);
		pauseGUI.SetActive(true);
	}

	public bool isGamePaused(){
		return pauseGame;
	}

	public void ExitGame(){
		Time.timeScale=1;
		Application.LoadLevel("TitleScreen");
	}
}

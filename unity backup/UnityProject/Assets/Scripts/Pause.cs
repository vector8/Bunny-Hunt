using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pause : MonoBehaviour {
	private bool pauseGame;
	private bool showGUI;
	public GameObject pause;
	public GameObject pauseGUI;

	// Update is called once per frame
	void Update () {

		if(pauseGame == true)
		{
			Time.timeScale=0;
			pause.SetActive(false);
			//pauseGUI.SetActive(true);
			showGUI = true;
		}
		if(showGUI == true)
		{
			if(Input.GetButtonDown("Fire1")){
				Time.timeScale=1;
				pause.SetActive(true);
				pauseGUI.SetActive(false);
				showGUI = false;
			}
		}

	
	}
	public void PauseGame()
	{
		pauseGame = true;
		pauseGUI.SetActive(true);
	}

	public bool isGamePasued(){
		return pauseGame;
	}
}

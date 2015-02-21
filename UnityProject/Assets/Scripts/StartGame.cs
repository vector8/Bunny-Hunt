using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {
	public GameObject TutorialPanel;
	public Fading fading;

	public void GameStart(){
		//Application.LoadLevel("Main");
		StartCoroutine(RunGame());
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
	IEnumerator RunGame(){
		//print("waiting");
		fading.gameObject.SetActive(true);
		fading.FadeOut();
		yield return new WaitForSeconds(fading.fadeSpeed);	
		//print("load main");
		Application.LoadLevel("Main");
	}
}

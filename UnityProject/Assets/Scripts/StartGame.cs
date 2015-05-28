using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using UnityEditor;
using System.IO;


public class StartGame : MonoBehaviour 
{
	public Tutorial tutorial;
	public Fading fading;
	public Text builtVersion;
	
	private bool hasSeenTutorial;

	void Start() 
	{
		//PlayerPrefs.DeleteAll(); // use for testing
		//#if UNITY_EDITOR
		//	builtVersion.text = "Built Version: " + PlayerSettings.bundleVersion + "  Code: " + PlayerSettings.Android.bundleVersionCode;
		//#endif
		hasSeenTutorial = PlayerPrefs.GetInt("HasSeenTutorial") == 1;
	}
	public void GameStart()
	{
		//Application.LoadLevel("Main");
		StartCoroutine(RunGame());
	}

	public void tutorialFromMenu()
	{
		tutorial.gameObject.SetActive(true);
		tutorial.willReturnToMenu(true);
		hasSeenTutorial = true;
		PlayerPrefs.SetInt("HasSeenTutorial", 1);
	}

	public void ExitGame()
	{
		Application.Quit();
	}
	
	public IEnumerator RunGame()
	{
		//print("waiting");
		if(hasSeenTutorial)
		{
			tutorial.gameObject.SetActive(false);
			fading.gameObject.SetActive(true);
			fading.FadeOut();
			yield return new WaitForSeconds(fading.fadeSpeed);	
			//print("load main");
			Application.LoadLevel("Main");
		}
		else
		{
			tutorial.gameObject.SetActive(true);
			tutorial.willReturnToMenu(false);
			hasSeenTutorial = true;
			PlayerPrefs.SetInt("HasSeenTutorial", 1);
		}
		
	}
}

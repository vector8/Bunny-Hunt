using UnityEngine;
using System.Collections;

public class TitleFade : MonoBehaviour {
	public Fading fading;
	public GameObject fadeGUI;

	void OnLevelWasLoaded(){
		
		fadeGUI.SetActive(true);
		//print("loading fade");
		fading.FadeIn();
		StartCoroutine(deactiveFade());
	}
	IEnumerator deactiveFade(){
		//print("waiting");
		yield return new WaitForSeconds(fading.fadeSpeed+0.5f);	
		fadeGUI.SetActive(false);
	}
}

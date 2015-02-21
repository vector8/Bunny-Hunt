using UnityEngine;
using System.Collections;

public class SplashFade : MonoBehaviour {
	
	public Fading fading; 
	public float displayTime;
	
	// Use this for initialization
	void Start () {
		fading.FadeIn();
		StartCoroutine(delayWait());
	}
	IEnumerator delayWait(){
		//print("waiting");
		yield return new WaitForSeconds(displayTime);		
		fading.FadeOut();
		yield return new WaitForSeconds(fading.fadeSpeed+0.5f);	
		Application.LoadLevel("TitleScreen");
	}
}

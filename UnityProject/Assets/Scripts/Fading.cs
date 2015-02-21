using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fading : MonoBehaviour {


	public float fadeSpeed = 0.8f;

	public void FadeIn(){
		StartCoroutine(DoFadeIn());
	}

	public void delayFade(float waitTime){
		StartCoroutine(delayWait(waitTime));
	}

	public void FadeOut(){
		StartCoroutine(DoFadeOut());
	}
	IEnumerator delayWait(float waitTime){
		//print("waiting");
		yield return new WaitForSeconds(waitTime);
	}

	IEnumerator DoFadeIn()
	{
		//print("fading in");
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 1;
		while (canvasGroup.alpha > 0)
		{
			canvasGroup.alpha -= fadeSpeed * Time.deltaTime;
			yield return null;
		}
		//canvasGroup.interactable = false;
		yield return null;
	}
	IEnumerator DoFadeOut()
	{
		//yield return new WaitForSeconds(3);
		//print("fading out");
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0;
		while (canvasGroup.alpha < 1)
		{
			canvasGroup.alpha += fadeSpeed * Time.deltaTime;
			yield return null;
		}
		//canvasGroup.interactable = false;
		yield return null;
	}

}
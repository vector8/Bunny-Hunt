using UnityEngine;
using System.Collections;

public class ConfirmSignIn : MonoBehaviour {
	public GameObject confirmSignIn;
	public GameController controller;

	private GameObject googlePlayObj;
	private Googleplay googlePlayScript;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void CancelSignIn(){
		confirmSignIn.SetActive(false);
	}
	
	public void ConfirmToSignIn(){
		googlePlayObj = GameObject.Find("GooglePlay");
		googlePlayScript = googlePlayObj.GetComponent<Googleplay>();
		googlePlayScript.SignIn();
		confirmSignIn.SetActive(false);
		new WaitForSeconds(2.0f);
		controller.PostToLeaderboard();
	}
	public IEnumerator Wait(){
		yield return new WaitForSeconds(5.0f);
	}

}

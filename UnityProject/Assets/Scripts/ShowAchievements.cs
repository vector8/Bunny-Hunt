using UnityEngine;
using System.Collections;

public class ShowAchievements : MonoBehaviour {

	private GameObject gp;
	private Googleplay gpScript;
	// Use this for initialization
	void Start () {
		gp = GameObject.Find("GooglePlay");
		gpScript = gp.GetComponent<Googleplay>();
		bool status = gpScript.ReturnSignInStatus();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void showAchievements(){
		gp = GameObject.Find("GooglePlay");
		gpScript = gp.GetComponent<Googleplay>();
		gpScript.ShowAchievements();
		
	}
}

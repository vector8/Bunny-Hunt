using UnityEngine;
using System.Collections;

public class ShowLeaderboard : MonoBehaviour {

	public GameObject gp;
	public Googleplay gpScript;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void showLeaderboard(){
		gp = GameObject.Find("GooglePlay");
		gpScript = gp.GetComponent<Googleplay>();
		gpScript.ShowLeaderboard();

	}

}

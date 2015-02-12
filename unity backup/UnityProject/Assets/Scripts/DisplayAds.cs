using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class DisplayAds : MonoBehaviour {

	public GameObject player;
	public GameController gameController;
	public PlayerController playerController;

	// Use this for initialization
	void Start () {

	}
	
	public void DisplayAd(){
		if(Advertisement.isReady())
		{
			ShowOptions options = new ShowOptions();
			options.pause = true;
			options.resultCallback = HandleShowResult;
			
			Advertisement.Show(null,options);
		}
	}

	public void HandleShowResult (ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log("The ad was skipped before reaching the end.");
			Revive();
			break;
		case ShowResult.Skipped:
			if(Debug.isDebugBuild){
				Revive();
			}
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}

	#region helper functions
	public void Revive(){
		gameController.Reviving(true);
		gameController.lifeAvailable = false;
		playerController.hunger = 100;
		player.SetActive(true);

		// Hide UI and start invincible counter (5s)
		//StartCoroutine(gameController.HideGameOverDisplay());
		gameController.HideGameOverDisplay();


	}
	#endregion
}

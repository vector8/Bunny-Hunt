using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class Googleplay : MonoBehaviour {
	//DontDestroyOnLoad gameobject GooglePlay
	public string daySurvived ="CgkI9raG460ZEAIQAQ";
	public string carrotEaten = "CgkI9raG460ZEAIQDA";
	public string hunterEaten = "CgkI9raG460ZEAIQDQ";
	public static Googleplay googlePlayObject;

	private bool signInStatus = false;
	// Use this for initialization
	void Awake() {
		if(googlePlayObject == null)
		{
			googlePlayObject = this;
			DontDestroyOnLoad(gameObject);
		} else
			Destroy(gameObject);

	}

	void Start () {
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();
		Social.localUser.Authenticate((bool success) => {
			if (success){
				Debug.Log("Login successful");
				signInStatus = true;
			}else{
				Debug.Log("Login failed");
				signInStatus = false;
			}
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SignIn(){
		Social.localUser.Authenticate((bool success) => {
			if (success){
				Debug.Log("Login successful");
				signInStatus = true;
			}else{
				Debug.Log("Login failed");
				signInStatus = false;
			}
		});

	}
	public void SignOut(){
		// sign out
		PlayGamesPlatform.Instance.SignOut();
		signInStatus = false;
	}

	//return player sign in status
	public bool ReturnSignInStatus(){
		return signInStatus;
	}

	// post day survived to leaderboard
	public void PostDaySurvived(int day){
		Social.ReportScore(day, daySurvived, (bool success) => {
			if (success){
				Debug.Log("Day survived posted");
			}else{
				Debug.Log("posting Day survived failed");
			}
		});
	}

	//post carots eaten to leaderboard
	public void PostCarrotEaten(int carrot){
		Social.ReportScore(carrot, carrotEaten, (bool success) => {
			if (success){
				Debug.Log("Carrot eaten posted");
			}else{
				Debug.Log("posting Carrot eaten failed");
			}
		});
	}

	//post hunters eaten to leaderboard
	public void PostHunterEaten(int hunter){
		Social.ReportScore(hunter, hunterEaten, (bool success) => {
			if (success){
				Debug.Log("Hunter eaten posted");
			}else{
				Debug.Log("posting Hunter eaten failed");
			}
		});
	}

	//show leaderboard main menu 
	public void ShowLeaderboard(){
		//PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI9raG460ZEAIQAQ");
		if(signInStatus == true)
		{
			Social.ShowLeaderboardUI();
		}else {
			SignIn();
		}

	}


}

using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class Googleplay : MonoBehaviour {
	//DontDestroyOnLoad gameobject GooglePlay
	private string leaderboardIDDay ="CgkI9raG460ZEAIQAQ";
	private string leaderboardIDCarrot ="CgkI9raG460ZEAIQDA";
	private string leaderboardIDHunter ="CgkI9raG460ZEAIQDQ";
	private string achieveIDDay1 ="CgkI9raG460ZEAIQBw";
	private string achieveIDDay2 ="CgkI9raG460ZEAIQCA";
	private string achieveIDDay3 ="CgkI9raG460ZEAIQCQ";
	private string achieveIDCarrot1 ="CgkI9raG460ZEAIQDg";
	private string achieveIDCarrot2 ="CgkI9raG460ZEAIQDw";
	private string achieveIDHunter1 ="CgkI9raG460ZEAIQCg";
	private string achieveIDHunter2 ="CgkI9raG460ZEAIQCw";

	public static Googleplay googlePlayObject;
	public int achieveDay1Req = 5;
	public int achieveDay2Req = 10;
	public int achieveDay3Req = 15;
	public int achieveCarrot1Req = 10;
	public int achieveCarrot2Req = 20;
	public int achieveHunter1Req = 50;
	public int achieveHunter2Req = 100;


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
		//Disable for PC
		/*
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
		*/
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
		Social.ReportScore(day, leaderboardIDDay, (bool success) => {
			if (success){
				Debug.Log("Day survived posted");
			}else{
				Debug.Log("posting Day survived failed");
			}
		});
	}

	//post carots eaten to leaderboard
	public void PostCarrotEaten(int carrot){
		Social.ReportScore(carrot, leaderboardIDCarrot, (bool success) => {
			if (success){
				Debug.Log("Carrot eaten posted");
			}else{
				Debug.Log("posting Carrot eaten failed");
			}
		});
	}

	//post hunters eaten to leaderboard
	public void PostHunterEaten(int hunter){
		Social.ReportScore(hunter, leaderboardIDHunter, (bool success) => {
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
	public void ShowAchievements(){
		//PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI9raG460ZEAIQAQ");
		if(signInStatus == true)
		{
			Social.ShowAchievementsUI();
		}else {
			SignIn();
		}
		
	}
	public void DaysAchievement(int days){
		if (signInStatus == true) {
			if (days >= achieveDay1Req){
				Social.ReportProgress(achieveIDDay1, 100.0f, (bool success) => {
					if (success){
						Debug.Log("posted day1 achievement");
					}else{
						Debug.Log("posted day1 achievement failed");
					}
				});
				Social.ReportProgress(achieveIDDay2, 0.0f, (bool success) => {
					if (success){
						Debug.Log("reveal day2 achievement");
					}else{
						Debug.Log("reveal day2 achievement failed");
					}
				});
			}
			if (days >= achieveDay2Req){
				Social.ReportProgress(achieveIDDay2, 100.0f, (bool success) => {
					if (success){
						Debug.Log("posted day2 achievement");
					}else{
						Debug.Log("posted day2 achievement failed");
					}
				});
				Social.ReportProgress(achieveIDDay2, 0.0f, (bool success) => {
					if (success){
						Debug.Log("reveal day3 achievement");
					}else{
						Debug.Log("reveal day3 achievement failed");
					}
				});
			}
			if (days >= achieveDay3Req){
				Social.ReportProgress(achieveIDDay3, 100.0f, (bool success) => {
					if (success){
						Debug.Log("posted day3 achievement");
					}else{
						Debug.Log("posted day3 achievement failed");
					}
				});
			}
		}
	}
	public void CarrotsAchievement (int carrots){
		if (signInStatus == true) {
			if (carrots >= achieveCarrot1Req){
				Social.ReportProgress(achieveIDCarrot1, 100.0f, (bool success) => {
					if (success){
						Debug.Log("posted carrot1 achievement");
					}else{
						Debug.Log("posted carrot1 achievement failed");
					}
				});
				Social.ReportProgress(achieveIDCarrot2, 0.0f, (bool success) => {
					if (success){
						Debug.Log("reveal carrot2 achievement");
					}else{
						Debug.Log("reveal carrot2 achievement failed");
					}
				});
			}
			if (carrots >= achieveCarrot2Req){
				Social.ReportProgress(achieveIDCarrot2, 100.0f, (bool success) => {
					if (success){
						Debug.Log("posted carrot2 achievement");
					}else{
						Debug.Log("posted carrot2 achievement failed");
					}
				});

			}
		}
	}
	public void HunterAchievement (int hunters){
		if (signInStatus == true) {
			if (hunters >= achieveHunter1Req){
				Social.ReportProgress(achieveIDHunter1, 100.0f, (bool success) => {
					if (success){
						Debug.Log("posted hunter1 achievement");
					}else{
						Debug.Log("posted hunter1 achievement failed");
					}
				});
				Social.ReportProgress(achieveIDHunter2, 0.0f, (bool success) => {
					if (success){
						Debug.Log("reveal hunter2 achievement");
					}else{
						Debug.Log("reveal hunter2 achievement failed");
					}
				});
			}
			if (hunters >= achieveHunter2Req){
				Social.ReportProgress(achieveIDHunter2, 100.0f, (bool success) => {
					if (success){
						Debug.Log("posted hunter2 achievement");
					}else{
						Debug.Log("posted hunter2 achievement failed");
					}
				});
				
			}
		}
	}
}

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
			}else{
				Debug.Log("Login failed");
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
			}else{
				Debug.Log("Login failed");
			}
		});

	}
	public void PostDaySurvived(int day){
		Social.ReportScore(day, daySurvived, (bool success) => {
			if (success){
				Debug.Log("Day survived posted");
			}else{
				Debug.Log("posting Day survived failed");
			}
		});
	}
	public void PostCarrotEaten(int carrot){
		Social.ReportScore(carrot, carrotEaten, (bool success) => {
			if (success){
				Debug.Log("Carrot eaten posted");
			}else{
				Debug.Log("posting Carrot eaten failed");
			}
		});
	}
	public void PostHunterEaten(int hunter){
		Social.ReportScore(hunter, hunterEaten, (bool success) => {
			if (success){
				Debug.Log("Hunter eaten posted");
			}else{
				Debug.Log("posting Hunter eaten failed");
			}
		});
	}

	public void ShowLeaderboard(){
		//PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI9raG460ZEAIQAQ");
		Social.ShowLeaderboardUI();
	}


}

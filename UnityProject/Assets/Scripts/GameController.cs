using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;

public class GameController : MonoBehaviour
{

    public float timeOfDay;
    public GameObject player;
    public string appID;
    public int carrots = 0;
    public int hunters = 0;
    public bool lifeAvailable = true;
	public int levelScaleToDays = 10;
	public float levelStartRatio = 0.6f;

    public Text txtDayCount;
    public Text txtGameOver;
    public Text txtCarrots;
    public Text txtDays;
    public Text txtHunters;
    public Text txtDayMsg;
    public Text carrotsCount;
    public Text huntersCount;
    public Button btnShowAd;
    public Sundial sundial;
    public Camera camera;
    public GameObject sundialUI;
    public GameObject confirmLogin;
    public GameObject gameOverDisplay;
    public AudioSource gameMusic;
    public Button btnPause;

    private GameObject googlePlayObj;
    private Googleplay googlePlayScript;
    private bool dayTime = true;
    private bool fade = true;
    private bool displayMsg = false;
    private bool reviving = false;
    private int preCarrots = 0;
    private int preHunters = 0;
    private int preDay = 0;

    // Use this for initialization
    void Start()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.allowPrecache = true;
            Advertisement.Initialize(appID, false);
        }
        googlePlayObj = GameObject.Find("GooglePlay");
		if (googlePlayObj != null) {
			googlePlayScript = googlePlayObj.GetComponent<Googleplay> ();
		}
    }

    // Update is called once per physics time unit
    void FixedUpdate()
    {
        //update day count
        txtDayCount.text = "Day " + sundial.day.ToString();

        if (player.activeSelf)
        {
            if (dayTime != sundial.isDayTime()) // if daytime has changed, update display msg
            {
                dayTime = sundial.isDayTime();
                fade = true;
                displayMsg = true;

                if (dayTime)
                {
                    txtDayMsg.text = "Day " + sundial.day + "\n"
                                    + "Run!";
                }
                else
                {
                    txtDayMsg.text = "Night " + sundial.day + "\n"
                                    + "Hunt!";
                }
            }

            if (fade)
            {
                Fade();
            }
        }
        if (preCarrots != carrots)
        {
            preCarrots = carrots;
            carrotsCount.text = carrots.ToString();
			if (googlePlayScript != null)
			{
	            if (googlePlayScript.ReturnSignInStatus() == true)
	            {
	                googlePlayScript.CarrotsAchievement(carrots);
	            }
			}
        }
        if (preHunters != hunters)
        {
            preHunters = hunters;
            huntersCount.text = hunters.ToString();
			if (googlePlayScript != null)
			{
	            if (googlePlayScript.ReturnSignInStatus() == true)
	            {
	                googlePlayScript.HunterAchievement(hunters);
	            }
			}
        }
        if (preDay != sundial.day)
        {
            preDay = sundial.day;
			if (googlePlayScript != null)
			{
	            if (googlePlayScript.ReturnSignInStatus() == true)
	            {
	                googlePlayScript.DaysAchievement(sundial.day);
	            }
			}
        }

    }

    public void GameOverDisplay()
    {
		txtCarrots.text = "Carrots: " + carrots;
		txtDays.text = "Days: " + sundial.day;
		txtHunters.text = "Hunters: " + hunters;
		new WaitForSeconds (1.0f);

		gameOverDisplay.SetActive (true);
		btnPause.gameObject.SetActive (false);
		if (lifeAvailable) {
			gameMusic.Pause ();
			btnShowAd.gameObject.SetActive (true);
		}
		//googlePlayObj = GameObject.Find("GooglePlay");
		//if (googlePlayObj != null) {
		//googlePlayScript = googlePlayObj.GetComponent<Googleplay> ();
		//}
		if (googlePlayScript != null)
		{
			if (googlePlayScript.ReturnSignInStatus () == false) {
				confirmLogin.SetActive (true);
			} else {
				googlePlayScript.PostDaySurvived (sundial.day);
				googlePlayScript.PostCarrotEaten (carrots);
				googlePlayScript.PostHunterEaten (hunters);
			}
		}
    }

    public void HideGameOverDisplay()
    {
        gameOverDisplay.SetActive(false);
        btnShowAd.enabled = false;
        btnShowAd.gameObject.SetActive(false);
        btnPause.gameObject.SetActive(true);
    }

    public void Fade()
    {
        // if we try to display the msg, 
        if (displayMsg)
        {
            txtDayMsg.CrossFadeAlpha(255, 2, false);
            displayMsg = false;
        }
        else
        {
            txtDayMsg.CrossFadeAlpha(0, 2, false);
            fade = false;
        }
    }

    public void Reviving(bool isReviving)
    {
        reviving = isReviving;
    }

    public bool IsReviving()
    {
        return reviving;
    }

    public void ShowRank()
    {
        //googlePlayObj = GameObject.Find("GooglePlay");
		//if (googlePlayObj != null) {
		//	googlePlayScript = googlePlayObj.GetComponent<Googleplay> ();
		//}
		if (googlePlayScript != null) {
			googlePlayScript.ShowLeaderboard ();
		}
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(5.0f);
    }

    public void PostToLeaderboard()
    {
        //googlePlayObj = GameObject.Find("GooglePlay");
       // googlePlayScript = googlePlayObj.GetComponent<Googleplay>();
		if (googlePlayScript != null) {
			googlePlayScript.PostDaySurvived (sundial.day);
			googlePlayScript.PostCarrotEaten (carrots);
			googlePlayScript.PostHunterEaten (hunters);
		}

    }
	public int getLevelScale()
	{
		return levelScaleToDays;
	}
	public float getLevelStartRatio()
	{
		return levelStartRatio;
	}
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public float timeOfDay;
	public GameObject player;
	public string appID;
	public int carrots = 0;
	public int hunters = 0;
	public bool lifeAvailable = true;

	public Text txtGameOver; 
	public Text txtCarrots;
	public Text txtDays;
	public Text txtHunters;
	public Text txtDayMsg;
	public Button btnRestart;
	public Button btnShowAd;
	public Sundial sundial;

	private bool bDayTime = true;
	private bool bFade = true;
	private bool bDisplayMsg = false;
	private bool bReviving = false;
	
	// Use this for initialization
	void Start()
	{
		if (Advertisement.isSupported) {
			Advertisement.allowPrecache = true;
			Advertisement.Initialize (appID, false);
		}
	}
	
	// Update is called once per physics time unit
	void FixedUpdate()
	{
		if(player.activeSelf)
		{
			if (bDayTime != sundial.isDayTime())	// if daytime has changed, update display msg
			{
				bDayTime = sundial.isDayTime();
				bFade = true;
				bDisplayMsg = true;

				if (bDayTime)
				{
					txtDayMsg.text = "Day " + sundial.day + "\n"
									+ "Run!";
				} else
				{
					txtDayMsg.text = "Night " + sundial.day + "\n"
									+ "Hunt!";
				}
			}

			if (bFade)
			{
				Fade();
			}
		}
	}

	public void GameOverDisplay(){
		txtCarrots.text = "Carrots: " + carrots;
		txtDays.text 	= "Days: " + sundial.day;
		txtHunters.text = "Hunters: " + hunters;

		new WaitForSeconds(1.0f);
		txtGameOver.gameObject.SetActive(true);
		txtCarrots.gameObject.SetActive(true);
		txtDays.gameObject.SetActive(true);
		txtHunters.gameObject.SetActive(true);
		btnRestart.gameObject.SetActive(true);
		if(lifeAvailable)
			btnShowAd.gameObject.SetActive(true);
	}

	public void HideGameOverDisplay(){
		txtGameOver.gameObject.SetActive(false);
		txtCarrots.gameObject.SetActive(false);
		txtDays.gameObject.SetActive(false);
		txtHunters.gameObject.SetActive(false);
		btnRestart.gameObject.SetActive(false);
		btnShowAd.enabled = false;
		btnShowAd.gameObject.SetActive(false);
	}
	
	public void Fade()
	{
		// if we try to display the msg, 
		if(bDisplayMsg)
		{
			txtDayMsg.CrossFadeAlpha(255, 2, false);
			bDisplayMsg = false;
		} else
		{
			txtDayMsg.CrossFadeAlpha(0, 2, false);
			bFade = false;
		}
	}

	public void Reviving(bool isReviving){
		Debug.Log("revival = " + isReviving);
		bReviving = isReviving;
	}

	public bool IsReviving(){
		return bReviving;
	}

	public IEnumerator Wait(){
		yield return new WaitForSeconds(5.0f);
	}
}

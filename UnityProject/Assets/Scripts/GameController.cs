using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public float timeOfDay;
	public GameObject player;
	public int carrots = 0;
	public int hunters = 0;

	public Text txtGameOver; 
	public Text txtCarrots;
	public Text txtDays;
	public Text txtHunters;
	public Text txtDayMsg;
	public Button btnRestart;
	public Sundial sundial;

	private bool bDayTime = true;
	private bool bFade = true;
	private bool bDisplayMsg = false;
	
	// Use this for initialization
	void Start()
	{

	}
	
	// Update is called once per physics time unit
	void FixedUpdate()
	{
		if(!player.activeSelf)
		{
			StartCoroutine(GameOverDisplay());
		} else
		{
			if (bDayTime != sundial.isDayTime())	// if daytime has changed, update display msg
			{
				bDayTime = sundial.isDayTime();
				bFade = true;
				bDisplayMsg = true;

				if (bDayTime)
				{
					txtDayMsg.text = "Day " + sundial.day;
				} else
				{
					txtDayMsg.text = "Night " + sundial.day;
				}
			}

			if (bFade)
			{
				Fade();
			}

		}
	}

	IEnumerator GameOverDisplay(){
		txtCarrots.text = "Carrots: " + carrots;
		txtDays.text 	= "Days: " + sundial.day;
		txtHunters.text = "Hunters: " + hunters;

		yield return new WaitForSeconds(1.0f);
		txtGameOver.gameObject.SetActive(true);
		txtCarrots.gameObject.SetActive(true);
		txtDays.gameObject.SetActive(true);
		txtHunters.gameObject.SetActive(true);
		btnRestart.gameObject.SetActive(true);
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
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public float timeOfDay;
	public GameObject player;
	public int hunger = 0;
	public int carrots = 0;
	public int hunters = 0;

	public Text txtGameOver; 
	public Text txtCarrots;
	public Text txtDays;
	public Text txtHunters;
	public GameObject btnRestart;

	private int day = 1;
	
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
		}
	}

	IEnumerator GameOverDisplay(){
		txtCarrots.text = "Carrots: " + carrots;
		txtDays.text 	= "Days: " + day;
		txtHunters.text = "Hunters: " + hunters;

		yield return new WaitForSeconds(2.0f);
		txtGameOver.gameObject.SetActive(true);
		txtCarrots.gameObject.SetActive(true);
		txtDays.gameObject.SetActive(true);
		txtHunters.gameObject.SetActive(true);
		btnRestart.SetActive(true);
	}
}

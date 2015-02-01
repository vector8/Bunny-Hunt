using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour
{
	public Sundial sundial;
	public GameController gameController;

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log(other.tag);
		if(other.tag == "Enemy")
		{
			if(sundial.isDayTime())
			{
				// kill the player
				gameObject.SetActive(false);
			}
			else
			{
				// eat the enemy
				Destroy(other.gameObject);
				// gain point
				gameController.hunters++;
			}
		} else
		{
			// eat the carrot
			Destroy(other.gameObject);
			gameController.carrots++;
		}
	}
	
	void OnTriggerStay2D(Collider2D other)
	{
		
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		
	}
}

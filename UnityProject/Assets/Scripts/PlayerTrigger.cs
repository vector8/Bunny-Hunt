using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour
{
	public Sundial sundial;
	public GameController gameController;
	public Hunger hungerController;

	void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log(other.tag);
		if(other.tag == "Enemy")
		{
			if(sundial.isDayTime())
			{
				// kill the player
				gameObject.SetActive(false);
			} else
			{
				// eat the enemy
				Destroy(other.gameObject);
				// gain point and update hunger
				gameController.hunters++;
				if(hungerController.hunger < 70)
				{
					hungerController.hunger += 30;
				} else
				{
					hungerController.hunger = 100;
				}
			}
		} else if(other.tag == "Carrot")
		{
			// eat the carrot
			Destroy(other.gameObject);
			// gain point and update hunger
			gameController.carrots++;
			if(hungerController.hunger < 70)
			{
				hungerController.hunger += 30;
			} else
			{
				hungerController.hunger = 100;
			}

		}
	}
	
	void OnTriggerStay2D(Collider2D other)
	{
		
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		
	}
}

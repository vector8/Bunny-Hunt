using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour
{
	public Sundial sundial;
	public GameController gameController;
	public PlayerController playerController;

	void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log(other.tag);
		if(other.tag == "Enemy")
		{
			if(!gameController.IsReviving()){
				if(sundial.isDayTime())
				{
					// kill the player
					gameObject.SetActive(false);
					gameController.GameOverDisplay();
				} else
				{
					// eat the enemy
					Destroy(other.gameObject);
					// gain point and update hunger
					gameController.hunters++;

					if(playerController.hunger < 70)
					{
						playerController.hunger += 30;
					} else
					{
						playerController.hunger = 100;
					}
				}
			}else{
				StartCoroutine(gameController.Wait());
				gameController.Reviving(false);
			}
		} else if(other.tag == "Carrot")
		{
			// eat the carrot
			Destroy(other.gameObject);
			// gain point and update hunger
			gameController.carrots++;
			if(playerController.hunger < 70)
			{
				playerController.hunger += 30;
			} else
			{
				playerController.hunger = 100;
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

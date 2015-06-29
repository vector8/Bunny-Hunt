using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour
{
	public Sundial sundial;
	public GameController gameController;
	public PlayerController playerController;
	public AudioSource SF_eatCarrot;
	public AudioSource SF_eatHunter;
	public AudioSource SF_bunnyDie;
	public AudioSource SF_bunnyHop;
	public AudioSource SF_mutatedHop;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Enemy")
		{
			SF_bunnyDie.Play ();
			if (SF_bunnyHop.isPlaying)
			{
				SF_bunnyHop.Stop ();
			}
			if (SF_mutatedHop.isPlaying)
			{
				SF_mutatedHop.Stop ();
			}
			SF_eatHunter.Play ();
			if(!gameController.IsReviving())
			{
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
			} else
			{
				StartCoroutine(gameController.Wait());
				gameController.Reviving(false);
			}
		} else if(other.tag == "Carrot")
		{
			SF_eatCarrot.Play ();
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

		} else if(other.tag == "Spear")
		{
			SF_bunnyDie.Play ();
			if (SF_bunnyHop.isPlaying)
			{
				SF_bunnyHop.Stop ();
			}
			if (SF_mutatedHop.isPlaying)
			{
				SF_mutatedHop.Stop ();
			}
			if(!gameController.IsReviving())
			{
				if(sundial.isDayTime())
				{
					// kill the player
					gameObject.SetActive(false);
					gameController.GameOverDisplay();
				} else
				{
					// Destory spear
					Destroy(other.gameObject);
				}
			} else
			{
				StartCoroutine(gameController.Wait());
				gameController.Reviving(false);
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

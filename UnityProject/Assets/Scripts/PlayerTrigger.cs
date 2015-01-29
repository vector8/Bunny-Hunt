using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour
{
	public Sundial sundial;

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

using UnityEngine;
using System.Collections;

public class DestroyCarrot : MonoBehaviour {
	void OnTriggerEnter2D (Collider2D food){
		if(food.tag == "Carrot")
		{
			Destroy(food.gameObject);
		}
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hunger : MonoBehaviour {

	public float hunger = 100;
	public float hungerFactor;

	public Image hungerBar;
	public GameObject player;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per physics time unit
	void FixedUpdate () {
		if(player.activeSelf)
		{
			if(hunger > 0)
			{
				hunger -= Time.deltaTime * hungerFactor;
				if(hunger < 0)
				{
					hunger = 0;
				}	
				hungerBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hunger * 3);
			} else
			{
				player.SetActive(false);
			}
		}
	}
}

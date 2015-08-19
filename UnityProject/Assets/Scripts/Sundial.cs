using UnityEngine;
using System.Collections;

public class Sundial : MonoBehaviour
{
	public float currentTime;
	public int day = 1;
	public GameObject sundialUI;
	public GameObject player;
	
	// Use this for initialization
	void Start()
	{
	}
	
	// Update is called once per frame
	void Update()
	{
		if (player.activeSelf)
		{
			currentTime += Time.deltaTime;
			
			while(currentTime > 30)
			{
				currentTime -= 30;
				day++;
			}
			
			Vector3 rotation = new Vector3();
			rotation.z = (currentTime / 30.0f) * 360.0f;
			
			sundialUI.transform.localEulerAngles = rotation;
		}
	}
	
	public bool isDayTime()
	{
		return currentTime < 15;
	}
}

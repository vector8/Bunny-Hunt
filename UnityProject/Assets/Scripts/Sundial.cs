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
		float screenWidth, screenHeight, dialWidth, dialHeight;
		screenHeight = Camera.main.orthographicSize;
		screenWidth = Camera.main.aspect * screenHeight;
		dialWidth = sundialUI.renderer.bounds.extents.x; 
		dialHeight = sundialUI.renderer.bounds.extents.y;

		this.transform.position = new Vector3(screenWidth - dialWidth - 0.5f, screenHeight - dialHeight - 0.2f, 0);
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

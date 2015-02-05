﻿using UnityEngine;
using System.Collections;

public class Sundial : MonoBehaviour
{
	private float currentTime;
	public int day = 1;
	public GameObject sundialUI;
	public GameObject dial;
	
	// Use this for initialization
	void Start()
	{
		float screenWidth, screenHeight, dialWidth, dialHeight;
		screenHeight = Camera.main.orthographicSize;
		screenWidth = Camera.main.aspect * screenHeight;
		dialWidth = dial.renderer.bounds.extents.x; 
		dialHeight = dial.renderer.bounds.extents.y;

		this.transform.position = new Vector3(screenWidth - dialWidth, screenHeight - dialHeight, 0);
	}
	
	// Update is called once per frame
	void Update()
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
	
	public bool isDayTime()
	{
		return currentTime < 15;
	}
}

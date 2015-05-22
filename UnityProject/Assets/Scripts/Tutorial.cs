using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour 
{
	public GameObject nextFrameButton;
	public GameObject prevFrameButton;
	public GameObject backButton;
	public GameObject skipButton;
	public StartGame startGame;
	public GameObject frame1;
	public GameObject frame2;
	public GameObject frame3;
	
	public GameObject player1;
	public GameObject player2;
	
	private int frameNumber = 1;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	}
	
	public void willReturnToMenu(bool returnToMenu)
	{
		if(returnToMenu)
		{
			backButton.SetActive(true);
			skipButton.SetActive(false);
		}
		else
		{
			backButton.SetActive(false);
			skipButton.SetActive(true);
		}
	}
	
	public void nextFrame()
	{
		frameNumber++;
		
		prevFrameButton.SetActive(true);
		
		if(frameNumber == 2)
		{
			frame1.SetActive(false);
			frame2.SetActive(true);
			player1.transform.position = new Vector2(-3.2f, 0f);
		}
		else if(frameNumber == 3)
		{
			frame2.SetActive(false);
			frame3.SetActive(true);
			nextFrameButton.SetActive(false);
			player2.transform.position = new Vector2(-3.2f, 0f);
		}
	}
	
	public void prevFrame()
	{
		frameNumber--;
		
		nextFrameButton.SetActive(true);
		
		if(frameNumber == 2)
		{
			frame3.SetActive(false);
			frame2.SetActive(true);
		}
		else if(frameNumber == 1)
		{
			frame2.SetActive(false);
			frame1.SetActive(true);
			prevFrameButton.SetActive(false);
			player2.transform.position = new Vector2(-3.2f, 0f);
		}
	}
	
	public void back()
	{
		// return to main menu
		gameObject.SetActive(false);
	}
	
	public void skip()
	{
		// start game
		StartCoroutine(startGame.RunGame());
	}
}

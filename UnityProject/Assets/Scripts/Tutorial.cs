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
	public GameObject frame1Text;
	public GameObject frame2Text;
	public GameObject frame3Text;
	
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
			frame1Text.SetActive(false);
			frame2Text.SetActive(true);
		}
		else if(frameNumber == 3)
		{
			frame2Text.SetActive(false);
			frame3Text.SetActive(true);
			nextFrameButton.SetActive(false);
		}
	}
	
	public void prevFrame()
	{
		frameNumber--;
		
		nextFrameButton.SetActive(true);
		
		if(frameNumber == 2)
		{
			frame3Text.SetActive(false);
			frame2Text.SetActive(true);
		}
		else if(frameNumber == 1)
		{
			frame2Text.SetActive(false);
			frame1Text.SetActive(true);
			prevFrameButton.SetActive(false);
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

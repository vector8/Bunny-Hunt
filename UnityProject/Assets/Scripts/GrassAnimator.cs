using UnityEngine;
using System.Collections;

public class GrassAnimator : MonoBehaviour 
{
	private SpriteRenderer renderer;
	
	public Sundial sundial;
	public Color dayColor;
	public Color nightColor;
	
	// Use this for initialization
	void Start () 
	{
		renderer = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float interpValue = 0.0f;
		float currentTime = sundial.currentTime;
		
		// When currentTime is 7.5, interpValue should be 0.0
		// When currentTime is 22.5, interpValue should be 1.0
		
		if(currentTime > 22.5f)
		{
			interpValue = (37.5f - currentTime) / 15.0f;
		}
		else if(currentTime < 7.5f)
		{
			interpValue = (7.5f - currentTime) / 15.0f;
		}
		else
		{
			interpValue = (currentTime - 7.5f) / 15.0f;
		}
		
		renderer.color = Color.Lerp(dayColor, nightColor, interpValue);
	}
}

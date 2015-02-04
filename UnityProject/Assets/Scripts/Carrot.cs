using UnityEngine;
using System.Collections;

public class Carrot : MonoBehaviour {
	private float destructTimer;
	private float newLevelTimer;
	public float timeLowerBound = 0.5f;
	public float initialTimer = 4f;
	public float dayTimeModifier = 0.15f;
	public Sundial sundial;
	// Use this for initialization
	void Start () {
		destructTimer = Random.Range(initialTimer * 0.8f, initialTimer * 1.2f);
	}
	
	// Update is called once per frame
	void Update () {
		destructTimer -= Time.deltaTime;
		if(destructTimer < 0.0f)
		{
			Destroy(this.gameObject);
			newLevelTimer = initialTimer * (1f - (dayTimeModifier*(sundial.day - 1)));
			if (newLevelTimer < timeLowerBound)
			{
				newLevelTimer = timeLowerBound;
			}
			destructTimer = Random.Range(newLevelTimer * 0.8f, newLevelTimer * 1.2f);
			
		}
		
	}
}

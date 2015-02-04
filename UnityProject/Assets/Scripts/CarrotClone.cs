using UnityEngine;
using System.Collections;

public class CarrotClone : MonoBehaviour {
	private float spawnTimer;
	public float avgSpawnTime=4;
	public Sundial sundial;
	public Carrot carrot;
	private float spawnScreenWidth, spawnScreenHeight, carrotWidth, carrotHeight;
	// Use this for initialization
	void Start () {
		spawnTimer = Random.Range(avgSpawnTime * 0.5f, avgSpawnTime * 1.5f);
		carrotWidth = carrot.renderer.bounds.extents.x; 
		carrotHeight = carrot.renderer.bounds.extents.y;		
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		spawnScreenHeight = Camera.main.orthographicSize - carrotHeight;
		spawnScreenWidth = (Camera.main.aspect * spawnScreenHeight) - carrotWidth;
	}
	
	// Update is called once per frame
	void Update () {
		spawnTimer -= Time.deltaTime;
		if (spawnTimer < 0)
		{
			if (sundial.isDayTime())
			{
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnScreenWidth, spawnScreenWidth), Random.Range(-spawnScreenHeight, spawnScreenHeight), 0);
				
				Carrot clone;
				clone = Instantiate(carrot, spawnPosition, Quaternion.identity) as Carrot;
				clone.sundial=sundial;
			}
			spawnTimer = Random.Range(avgSpawnTime * 0.5f, avgSpawnTime * 1.5f);
		}
	}
}
using UnityEngine;
using System.Collections;

public class CarrotClone : MonoBehaviour
{
	private float spawnTimer;
	private float avgSpawnTime;
	public float startingAverageSpawnTime;
	public float spawnDivisor;
	public GameObject carrotPrefab;
	public GameObject player;
	
	// Use this for initialization
	void Start()
	{
		avgSpawnTime = startingAverageSpawnTime;
		spawnTimer = Random.Range(avgSpawnTime * 0.5f, avgSpawnTime * 1.5f);
	}
	
	// Update is called once per frame
	void Update()
	{
		if(player.activeSelf)
		{
			spawnTimer -= Time.deltaTime;
			
			if(spawnTimer <= 0)
			{
				float screenWidth, screenHeight;
				screenHeight = Camera.main.orthographicSize;
				screenWidth = Camera.main.aspect * screenHeight;
				
				Vector3 spawnPosition = new Vector3(Random.Range(-screenWidth, screenWidth), Random.Range(-screenHeight, screenHeight), 0);
				
				Instantiate(carrotPrefab, spawnPosition, Quaternion.identity);
				
				spawnTimer = Random.Range(avgSpawnTime * 0.5f, avgSpawnTime * 1.5f);
			}
		}
	}
}

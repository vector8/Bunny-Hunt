﻿using UnityEngine;
using System.Collections;

public class EnemyClone : MonoBehaviour
{
	private float avgSpawnTime;
	private int day = 1;
	private float spawnTimer;

	public float startingAverageSpawnTime;
	public float spawnDivisor;
	public Enemy enemyPrefab;
	public GameController gameController;
	public GameObject player;
	public Sundial sundial;


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
		
			if(sundial.day > day)
			{
				day = sundial.day;
				avgSpawnTime = Mathf.Max(startingAverageSpawnTime * Mathf.Pow((1.0f / spawnDivisor), sundial.day - 1), 0.5f);
			}
		
			if(spawnTimer <= 0)
			{
				float screenWidth, screenHeight;
				screenHeight = Camera.main.orthographicSize;
				screenWidth = Camera.main.aspect * screenHeight;
			
				Vector3 spawnPosition = new Vector3(Random.Range(-screenWidth, screenWidth), Random.Range(-screenHeight, screenHeight), 0);
			
				Enemy instance;
				instance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity) as Enemy;
				instance.player = player;
				instance.sundial = sundial;
				instance.gameController = gameController;
			
				spawnTimer = Random.Range(avgSpawnTime * 0.5f, avgSpawnTime * 1.5f);
			}
		}
	}
}
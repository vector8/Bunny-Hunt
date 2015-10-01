using UnityEngine;
using System.Collections;

public class EnemyClone : MonoBehaviour
{
	private float avgSpawnTime;
	private int day = 1;
	private float spawnTimer;
	private int maxEnemyCount;

	public float startingAverageSpawnTime;
	public float spawnDivisor;
	public Enemy enemyPrefab;
	public GameController gameController;
	public GameObject player;
	public Sundial sundial;
	public int startingMaxEnemy;
	public int maxEnemyByDayRatio;


	// Use this for initialization
	void Start()
	{
		avgSpawnTime = startingAverageSpawnTime;
		spawnTimer = Random.Range(avgSpawnTime * 0.5f, avgSpawnTime * 1.5f);
		maxEnemyCount = startingMaxEnemy;
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
				avgSpawnTime = Mathf.Max(startingAverageSpawnTime * Mathf.Pow((1.0f / spawnDivisor), (sundial.day + 1)/2), 0.5f);
				maxEnemyCount = maxEnemyCount + sundial.getDayCount() * maxEnemyByDayRatio;
			}
		
			if(spawnTimer <= 0)
			{
				GameObject[] getEnemyCount;
				int currentEnemyCount;
				try {
					getEnemyCount = GameObject.FindGameObjectsWithTag ("Enemy");
					currentEnemyCount = getEnemyCount.Length;
				} catch (UnityException ex) {
					currentEnemyCount = 0;
				}
				Debug.Log(currentEnemyCount+"/"+maxEnemyCount);


				if (currentEnemyCount < maxEnemyCount){

					float screenWidth, screenHeight;
					float enemyWidth, enemyHeight;
					float spawnWidth, spawnHeight;
					screenHeight = Camera.main.orthographicSize;
					screenWidth = Camera.main.aspect * screenHeight;				
					enemyHeight = enemyPrefab.renderer.bounds.extents.y;
					enemyWidth = enemyPrefab.renderer.bounds.extents.x;

					spawnWidth = Random.Range(screenWidth, screenWidth + enemyWidth);
					//returns 0 or 1, 0 = left side of screen, 1 = right side of screen
					if (Random.Range(0,2) < 1){
						spawnWidth = 0-spawnWidth;
					}
					spawnHeight = Random.Range(screenHeight, screenHeight + enemyHeight);
					//returns 0 or 1, 0 = above screen, 1 = below screen
					if (Random.Range(0,2) < 1){
						spawnHeight = 0-spawnHeight;
					}

					Vector3 spawnPosition = new Vector3(spawnWidth, spawnHeight, 0);
				
					Enemy instance;
					instance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity) as Enemy;
					instance.player = player;
					instance.sundial = sundial;
					instance.gameController = gameController;
					instance.goal = spawnPosition;
				
					spawnTimer = Random.Range(avgSpawnTime * 0.5f, avgSpawnTime * 1.5f);
				}
			}
		}
	}
}

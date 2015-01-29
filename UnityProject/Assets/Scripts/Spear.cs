using UnityEngine;
using System.Collections;

public class Spear : MonoBehaviour
{
	private float u = 0.0f;
	private Vector3 originalPosition;
	private bool finished = false;
	private float interpSpeed;
	private float lifeTime;
	private Vector3 target;
	public float maxLifeTime;
	public float moveSpeed;
	public GameObject player;
	
	// Use this for initialization
	void Start()
	{
		originalPosition = transform.position;
		target = player.transform.position;
		
		interpSpeed = moveSpeed / Vector3.Distance(originalPosition, target);
	}
	
	// Update is called once per frame
	void Update()
	{
		lifeTime += Time.deltaTime;
		u += interpSpeed * Time.deltaTime;
		
		if(u >= 1.0f)
		{
			u = 1.0f;
			finished = true;
		} else if(lifeTime >= maxLifeTime)
		{
			finished = true;
		}
		
		transform.position = Vector3.Lerp(originalPosition, target, u);
		
		if(Vector3.Distance(player.transform.position, transform.position) < 0.1)
		{
			// TODO: kill player here
			player.SetActive(false);
			
			// remove this spear
			Destroy(gameObject);
		} else if(finished)
		{
			Destroy(gameObject);
		}
		
	}
}

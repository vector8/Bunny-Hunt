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
	private int dayCount;
	private float levelStartRatio;
	private float levelScaleToDays;
	public float maxLifeTime;
	public float maxMoveSpeed;
	public GameObject player;
	public GameController gameController;
	public GameObject sundialObj;
	public Sundial sundial;
	
	// Use this for initialization
	void Start()
	{
		originalPosition = transform.position;
		target = player.transform.position;
		levelScaleToDays = gameController.getLevelScale ();
		levelStartRatio = gameController.getLevelStartRatio ();
		sundialObj = GameObject.Find ("Sundial");
		sundial = sundialObj.GetComponent<Sundial> ();

	}
	
	// Update is called once per frame
	void Update()
	{
		dayCount = sundial.getDayCount ();
		float moveSpeed = 0;
		moveSpeed = (maxMoveSpeed * levelStartRatio) + ((maxMoveSpeed * (1 - levelStartRatio)) / levelScaleToDays)*sundial.getDayCount();
		if (moveSpeed > maxMoveSpeed)
		{
			moveSpeed = maxMoveSpeed;
		}
		interpSpeed = moveSpeed / Vector3.Distance(originalPosition, target);
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

		/*
		 * Original spear kill rabbit code, moving it to PlayerTrigger.cs
		if(Vector3.Distance(player.transform.position, transform.position) < 0.1)
		{
			if(!gameController.IsReviving()){
				player.SetActive(false);
				gameController.GameOverDisplay();
			}else{
				Debug.Log("resetting revival");
				StartCoroutine(gameController.Wait());
				gameController.Reviving(false);
			}
			// remove this spear
			Destroy(gameObject);
		} else if(finished)
		{
			Destroy(gameObject);
		}
		*/
		if(finished)
		{
			Destroy(gameObject);
		}
		
	}
}

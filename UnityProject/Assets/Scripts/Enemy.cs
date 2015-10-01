using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	private bool attacking = false;
	private float attackAnimationLength = 0.5f;
	private float attackTimer = 0;
	private int speedHash = Animator.StringToHash("Speed");
	private int attackingHash = Animator.StringToHash("Attacking");
	private Animator anim;
	private float chaseTimer;
	private float idleTimer;
	private float currentSpeed;
	private GameObject alertBox;
	private GameObject ahhhBox;
	private GameObject SF_throwSpearObj;
	private GameObject SF_hunterRunawayObj;
	private GameObject SF_hunterChaseObj;
	private float screamTimer = 0;
	private int levelScaleToDays;
	private float levelStartRatio;
	private int dayCount;
	private float walkSpeed = 0;
	private float runSpeed =0;
	private float screenWidth;
	private float screenHeight;

	public GameController gameController;
	public GameObject player;
	public Sundial sundial;
	public float detectionRadius;
	public float attackDelay;
	public float maxChaseTime;
	public float maxRunSpeed;
	public float maxWalkSpeed;
	public float minWanderRange;
	public float maxWanderRange;
	public float minIdleTime;
	public float maxIdleTime;
	public float screamDelay;
	public Spear spearPrefab;
	public Vector3 goal;
	public AudioSource SF_throwSpear;
	public AudioSource SF_hunterRunaway;
	public AudioSource SF_hunterChase;
	
	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator>();
		SF_throwSpearObj = GameObject.Find("SF_ThrowSpear");
		SF_throwSpear = SF_throwSpearObj.GetComponent<AudioSource>();
		SF_hunterRunawayObj = GameObject.Find("SF_HunterRunaway");
		SF_hunterRunaway = SF_hunterRunawayObj.GetComponent<AudioSource>();
		SF_hunterChaseObj = GameObject.Find("SF_HunterChase");
		SF_hunterChase = SF_hunterChaseObj.GetComponent<AudioSource>();		
		alertBox = this.transform.FindChild("Alert").gameObject;
		ahhhBox = this.transform.FindChild("Ahhh").gameObject;
		levelScaleToDays = gameController.getLevelScale ();
		levelStartRatio = gameController.getLevelStartRatio ();
		dayCount = sundial.getDayCount ();
		walkSpeed = (maxWalkSpeed * levelStartRatio) + ((maxWalkSpeed * (1 - levelStartRatio)) / levelScaleToDays) * sundial.getDayCount ();
		runSpeed = (maxRunSpeed * levelStartRatio) + ((maxRunSpeed * (1 - levelStartRatio)) / levelScaleToDays) * sundial.getDayCount ();

		screenHeight = Camera.main.orthographicSize;
		screenWidth = Camera.main.aspect * screenHeight;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (sundial.getDayCount () > dayCount) {
			dayCount = sundial.getDayCount ();

			runSpeed = (maxRunSpeed * levelStartRatio) + ((maxRunSpeed * (1 - levelStartRatio)) / levelScaleToDays) * sundial.getDayCount ();
			if (runSpeed > maxRunSpeed) {
				runSpeed = maxRunSpeed;
			}

			walkSpeed = (maxWalkSpeed * levelStartRatio) + ((maxWalkSpeed * (1 - levelStartRatio)) / levelScaleToDays) * sundial.getDayCount ();
			if (walkSpeed > maxWalkSpeed) {
				walkSpeed = maxWalkSpeed;
			}
		}

		attackTimer += Time.deltaTime;
		screamTimer += Time.deltaTime;
		
		if(!attacking)
		{
			alertBox.SetActive(false);
			ahhhBox.SetActive(false);
			if(player.activeSelf && Vector3.Distance(player.transform.position, transform.position) < detectionRadius)
			{

				alertBox.SetActive(true);
				ahhhBox.SetActive(false);
				if(attackTimer >= attackDelay && sundial.isDayTime())
				{
					if ((screamTimer >= screamDelay) && (!SF_hunterChase.isPlaying)){
						SF_hunterChase.Play();
						screamTimer = 0;
					}
					attackTimer = 0.0f;
					attacking = true;
					anim.SetBool(attackingHash, attacking);
					return;
				}
				else
				{
					if ((screamTimer >= screamDelay) && (!sundial.isDayTime())){
						SF_hunterRunaway.Play ();
						screamTimer=0;
					}
					chaseTimer = maxChaseTime;
				}
			}
			
			if(player.activeSelf && chaseTimer > 0.0f)
			{
				chaseTimer -= Time.deltaTime;
				
				if(sundial.isDayTime())
				{

					alertBox.SetActive(true);
					ahhhBox.SetActive(false);
					goal = player.transform.position;
				}
				else
				{
					// get the point on the opposite side of this enemy from the player
					// TODO: Limit running to within the screen boundary?
					alertBox.SetActive(false);
					ahhhBox.SetActive(true);
					goal = 0.75f*(transform.position - player.transform.position) + transform.position;
				}
				
				currentSpeed = runSpeed;
			}
			else if(idleTimer > 0.0f)
			{
				alertBox.SetActive(false);
				ahhhBox.SetActive(false);
				idleTimer -= Time.deltaTime;
				
				if(idleTimer <= 0.0f)
				{
					// find a new goal to wander to
					// find a random point within wander range (higher chance to move toward center of map the further out this is)
					//float wanderDistance = Random.Range(minWanderRange, maxWanderRange);
					


					goal.x = Random.Range(-screenWidth,screenWidth);
					goal.y = Random.Range(-screenHeight, screenHeight);
					goal.z = 0;
					/*
					 * float chanceWest = 0.5f;
					float chanceSouth = 0.5f;
					
					Vector3 temp = transform.position;
					if(temp.x > screenWidth * 0.5f)
					{
						chanceWest = temp.x / screenWidth;
					}
					else if(temp.x < -screenWidth * 0.5f)
					{
						chanceWest = 1.0f - (-temp.x / screenWidth);
					}
					
					if(temp.y > screenHeight * 0.5f)
					{
						chanceSouth = temp.x / screenHeight;
					}
					else if(temp.x < -screenHeight * 0.5f)
					{
						chanceSouth = 1.0f - (-temp.x / screenHeight);
					}
					
					int rollX = Random.Range(0, 100);
					int rollY = Random.Range(0, 100);
					
					Vector3 dir = new Vector3();
					
					if(rollX < chanceWest * 100.0f)
					{
						dir.x = -1.0f;
					}
					else
					{
						dir.x = 1.0f;
					}
					if(rollY < chanceSouth * 100.0f)
					{
						dir.y = -1.0f;
					}
					else
					{
						dir.y = 1.0f;
					}
					
					dir.x *= Random.value;
					dir.y *= Random.value;
					
					dir.Normalize();
					
					goal = transform.position + dir * wanderDistance;
					
					if(goal.x > screenWidth)
					{
						goal.x = screenWidth;	
					}
					else if(goal.x < -screenWidth)
					{
						goal.x = -screenWidth;
					}
					
					if(goal.y > screenHeight)
					{
						goal.y = screenHeight;
					}
					else if(goal.y < -screenHeight)
					{
						goal.y = -screenHeight;
					}
					*/
				}
				
				currentSpeed = 0.0f;
			}
			else // must be walking
			{

				currentSpeed = walkSpeed;
			}
			
			if(currentSpeed > 0)
			{
				// move the unit according to the goal and speed
				Vector3 moveDirection = goal - transform.position;
				
				if(moveDirection.magnitude < 0.1)
				{
					// we've reached our goal
					idleTimer = Random.Range(minIdleTime, maxIdleTime);
				}
				else
				{
					moveDirection.Normalize();
					transform.position += moveDirection * currentSpeed * Time.deltaTime;
				}
				
				Vector3 scale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
				if(moveDirection.x < 0)
				{
					scale.x *= -1;
				}
				transform.localScale = scale;
			}
				
			anim.SetFloat(speedHash, currentSpeed);
		}
		else
		{
			if(attackTimer >= attackAnimationLength)
			{
				SF_throwSpear.Play ();
				Quaternion rotation = Quaternion.FromToRotation(new Vector3(1, 0, 0), (player.transform.position - transform.position));
				Spear instance;
				Vector3 startPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
				instance = Instantiate(spearPrefab, startPos, rotation) as Spear;
				instance.player = player;
				instance.gameController = gameController;
				
				attacking = false;
				anim.SetBool(attackingHash, attacking);
			}
		}
		alertBox.transform.rotation = Quaternion.Euler(alertBox.transform.rotation.eulerAngles.x, 0, 0); 
		ahhhBox.transform.rotation = Quaternion.Euler(ahhhBox.transform.rotation.eulerAngles.x, 0, 0); 
	}
}

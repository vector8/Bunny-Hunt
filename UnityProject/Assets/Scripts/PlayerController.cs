using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	private bool jumping = false;
	private float clickDelay;
	private float jumpTimer;
	private int speedHash = Animator.StringToHash("Speed");
	private int mutatedHash = Animator.StringToHash("Mutated");
	private Animator anim;
	private Vector3 moveDirection;
	private Vector3 prevMoveDirection;
	private Vector3 targetPosition;
	private bool dayTimeNow = true;
	private int levelScaleToDays;
	private float levelStartRatio;
	private int day;
	private float hungerFactor =0;

	public float hunger = 100;
	public float maxHungerFactor = 5;
	public float jumpSpeed;
	public float jumpDuration;
	public float moveSpeed;
	public GameController gameController;
	public Image hungerBar;
	public Sundial sundial;
	public AudioSource SF_hop;
	public AudioSource SF_mutatedHop;
	public AudioSource SF_mutateBig;
	public AudioSource SF_mutateSmall;
	public AudioSource SF_bunnyDie;
	
	// Use this for initialization
	void Start()
	{
		anim = GetComponent<Animator>();
		levelScaleToDays = gameController.getLevelScale ();
		levelStartRatio = gameController.getLevelStartRatio ();
		day = sundial.getDayCount ();
		hungerFactor = (maxHungerFactor * levelStartRatio) + ((maxHungerFactor * (1 - levelStartRatio)) / levelScaleToDays)*sundial.getDayCount();
	}
	
	// Update is called once per frame
	void Update()
	{
		if(Time.timeScale > 0)
		{
			Vector3 currentPosition = transform.position;
		
			if(Input.GetButtonDown("Fire1"))
			{
				clickDelay = 0;

			} else if(Input.GetButtonUp("Fire1"))
			{
				jumping = (clickDelay < 0.5f);

			}
	
			if(jumping)
			{
				if (sundial.isDayTime())
				{
					if (SF_mutatedHop.isPlaying)
					{
						SF_mutatedHop.Stop ();
					}
					if(!SF_hop.isPlaying)
					{
						SF_hop.Play ();
					}
				}else {
					if(SF_hop.isPlaying)
					{
						SF_hop.Stop ();
					}
					if (!SF_mutatedHop.isPlaying)
					{
						SF_mutatedHop.Play ();
					}
				}
				jumpTimer += Time.deltaTime;
			

				if ((moveDirection.x ==0)&&(moveDirection.y == 0)){
					transform.position += prevMoveDirection * jumpSpeed * Time.deltaTime;
				}else {
					anim.SetFloat(speedHash, jumpSpeed);
					prevMoveDirection = moveDirection;
					transform.position += moveDirection * jumpSpeed * Time.deltaTime;
				}
			
				float screenWidth, screenHeight;
				screenHeight = Camera.main.orthographicSize;
				screenWidth = Camera.main.aspect * screenHeight;
			
				Vector3 pos = transform.position;
			
				if(pos.x > screenWidth)
				{
					pos.x = screenWidth;
				} else if(pos.x < -screenWidth)
				{
					pos.x = -screenWidth;
				}
				if(pos.y > screenHeight)
				{
					pos.y = screenHeight;
				} else if(pos.y < -screenHeight)
				{
					pos.y = -screenHeight;
				}
			
				transform.position = pos;
			
				if(jumpTimer >= jumpDuration)
				{
					moveDirection.x = 0;
					moveDirection.y = 0;
					moveDirection.z = 0;
				
					jumping = false;
					jumpTimer = 0;
				}
			} else
			{
				if(Input.GetButton("Fire1"))
				{				
					clickDelay += Time.deltaTime;
				
					targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				
					float screenWidth, screenHeight;
					screenHeight = Camera.main.orthographicSize;
					screenWidth = Camera.main.aspect * screenHeight;
				
					if(targetPosition.x > screenWidth)
					{
						targetPosition.x = screenWidth;
					} else if(targetPosition.x < -screenWidth)
					{
						targetPosition.x = -screenWidth;
					}
				
					if(targetPosition.y > screenHeight)
					{
						targetPosition.y = screenHeight;
					} else if(targetPosition.y < -screenHeight)
					{
						targetPosition.y = -screenHeight;
					}
				
					targetPosition.z = 0;
				
					if(Vector3.Distance(targetPosition, transform.position) >= 0.1)
					{
						moveDirection = targetPosition - currentPosition;
						moveDirection.z = 0;
						moveDirection.Normalize();
					}
				} else
				{
					moveDirection.x = 0;
					moveDirection.y = 0;
					moveDirection.z = 0;
					jumping = false;
				}
		
				if(moveDirection.magnitude != 0)
				{
				
					Vector3 scale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
					if(moveDirection.x < 0)
					{
						scale.x *= -1;
					}
					transform.localScale = scale;
				
					anim.SetFloat(speedHash, moveSpeed);
					transform.position += moveDirection * moveSpeed * Time.deltaTime;
				
					if(Vector3.Distance(targetPosition, transform.position) < 0.1)
					{
						moveDirection.x = 0;
						moveDirection.y = 0;
						moveDirection.z = 0;
						jumping = false;
					}
				} else
				{
					anim.SetFloat(speedHash, 0);

				}
			}
		
			if(sundial.isDayTime())
			{				
				transform.localScale = new Vector3(Mathf.Sign(transform.localScale.x) * 1.5f, 1.5f, 1.0f);
				if (dayTimeNow != sundial.isDayTime())
				{
					dayTimeNow=sundial.isDayTime();
					SF_mutateSmall.Play ();
				}
			} 
			else
			{
				transform.localScale = new Vector3(Mathf.Sign(transform.localScale.x) * 3.0f, 3.0f, 1.0f);
				if (dayTimeNow != sundial.isDayTime())
				{
					dayTimeNow=sundial.isDayTime();
					SF_mutateBig.Play ();
				}
			}
		
			anim.SetBool(mutatedHash, !sundial.isDayTime());
	
			if(this.gameObject.activeSelf)
			{
				UpdateHunger();
			}
		}
	}

	public void freeze()
	{
		targetPosition = transform.position;
		moveDirection.x = 0;
		moveDirection.y = 0;
		moveDirection.z = 0;
		jumping = false;
		jumpTimer = 0;
		clickDelay = 0.5f;
	}

	void UpdateHunger()
	{
		if(hunger > 0)
		{
			if (sundial.getDayCount() > day )
			{
				day = sundial.getDayCount();
				hungerFactor = (maxHungerFactor * levelStartRatio) + ((maxHungerFactor * (1 - levelStartRatio)) / levelScaleToDays)*sundial.getDayCount();
				if (hungerFactor > maxHungerFactor)
				{
					hungerFactor = maxHungerFactor;
				}
			}
			hunger -= Time.deltaTime * hungerFactor;
			if(hunger < 0)
			{
				if(SF_hop.isPlaying)
				{
					SF_hop.Stop();
				}
				if (!SF_mutatedHop.isPlaying)
				{
					SF_mutatedHop.Stop ();
				}
				SF_bunnyDie.Play ();
				hunger = 0;
			}	
			hungerBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hunger * 3);
		} else
		{
			this.gameObject.SetActive(false);
			gameController.GameOverDisplay();
		}
	}
}

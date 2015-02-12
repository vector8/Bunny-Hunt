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
	private Vector3 targetPosition;
	private bool canMove = true;

	public float hunger = 100;
	public float hungerFactor;
	public float jumpSpeed;
	public float jumpDuration;
	public float moveSpeed;
	public GameController gameController;
	public Image hungerBar;
	public Sundial sundial;
	
	// Use this for initialization
	void Start()
	{
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if(canMove == true)
		{
			Vector3 currentPosition = transform.position;
		
			if(Input.GetButtonDown("Fire1"))
			{
				clickDelay = 0;
			} else if(Input.GetButtonUp("Fire1"))
			{
				jumping = (clickDelay < 0.5f);
			}

			if(jumping && canMove)
			{
				jumpTimer += Time.deltaTime;
			
				anim.SetFloat(speedHash, jumpSpeed);
				transform.position += moveDirection * jumpSpeed * Time.deltaTime;
			
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
				}
		
				if((moveDirection.magnitude != 0)&& canMove)
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
					}
				} else
				{
					anim.SetFloat(speedHash, 0);
				}
			}
		
			if(!sundial.isDayTime())
			{
				transform.localScale = new Vector3(Mathf.Sign(transform.localScale.x) * 3.0f, 3.0f, 1.0f);
			} else
			{
				transform.localScale = new Vector3(Mathf.Sign(transform.localScale.x) * 1.5f, 1.5f, 1.0f);
			}
		
			anim.SetBool(mutatedHash, !sundial.isDayTime());

			if(this.gameObject.activeSelf)
			{
				UpdateHunger();
			}
		}
	}
	void Move(){

	}

	public void stopMove(){
		print("stop moving");
		canMove = false;
	}
	public void resumeMove(){
		print("resume moving");
		canMove = true;
	}

	void UpdateHunger()
	{
		if(hunger > 0)
		{
			hunger -= Time.deltaTime * hungerFactor;
			if(hunger < 0)
			{
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

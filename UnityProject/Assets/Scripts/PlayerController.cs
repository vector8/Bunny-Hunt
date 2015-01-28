using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public Vector3 moveDirection;
    public Vector3 targetPosition;

	public float moveSpeed;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 currentPosition = transform.position;

		if (Input.GetButton("Fire1"))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0;

            moveDirection = targetPosition - currentPosition;
            moveDirection.z = 0;
            moveDirection.Normalize();
        } 

        if (moveDirection.magnitude != 0)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            if(Mathf.Abs((targetPosition - transform.position).magnitude) < 0.1)
            {
                moveDirection.x = 0;
                moveDirection.y = 0;
                moveDirection.z = 0;
            }
        }
	}
}

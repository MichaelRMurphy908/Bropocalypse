using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	
	public Transform target;
	public int speed;
	public int rotationSpeed;
	public float attackedTimer = 0.1f;
	float timer = 0;
	public float gravity = 15.0f;
	public int maxDistance;
	public Vector3 curMoveSpeed;
	Vector3 attackedPosition;
	//private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;
	void Awake()
	{	
	}
	
	// Use this for initialization
	void Start () 
	{
		animation.CrossFade("walk");
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		controller = GetComponent<CharacterController>();
		target = go.transform;
		maxDistance = 2;
		curMoveSpeed = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () 
	{
		ActiveRagdoll ar = gameObject.GetComponent<ActiveRagdoll>();
		//Debug.Log(ar.freezeForce);
		Debug.DrawLine(target.position, transform.position, Color.green);
		
		
		if (!ar.IsRagdoll())
		{
			if(timer > 0)
			{
				timer -= Time.deltaTime;

			}
				MoveTowards(target.position);
		}
		curMoveSpeed = controller.velocity;
	}
	public void Attacked()
	{
		timer = attackedTimer;

	}
	void MoveTowards(Vector3 newPos)
	{
	
		Vector3 moveVector = newPos - transform.position;
		moveVector.Normalize();
		moveVector.x *= speed;
		moveVector.z *= speed;
		if (timer > 0)
		{
			moveVector.x *= -2;
			moveVector.z *= -2;
			moveVector.y *= (speed);
		}
		else
		{
			moveVector.y -= gravity;
		}
		controller.Move(moveVector * Time.deltaTime);
		
		if(!(timer > 0))
		{
			moveVector.x *= -1;
			moveVector.z *= -1;
		}
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(moveVector), rotationSpeed * Time.deltaTime);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
	}
}


 
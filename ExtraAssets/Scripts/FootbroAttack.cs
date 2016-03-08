using UnityEngine;
using System.Collections;

public class FootbroAttack : MonoBehaviour {
	
	public GameObject target;
	public float attackTimer;
	public float coolDown;
	public int chargeSpeed;
	public float chargeTimer;
	public float chargeLength;
	
	// Use this for initialization
	void Start () 
	{
		attackTimer = 0;
		coolDown = 0.5f;
		chargeTimer = 0;
		chargeLength = 1.0f;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if(chargeTimer != 0)
		{
			if (chargeTimer < 0)
			{
				chargeTimer = 0;
			}
			if(attackTimer > 0)
					attackTimer -= Time.deltaTime;
			if (attackTimer	< 0)
					attackTimer = 0;
			if (Input.GetMouseButtonDown(0))	
			{
				if (attackTimer == 0)
				{
					Attack();
					attackTimer = coolDown;
				}
			}
			if (Input.GetKeyUp(KeyCode.R))
			{
				charging = true;
			}
		}
		else
		{
			
		}
	}
	
	private void Attack()
	{
		float distance = Vector3.Distance(target.transform.position, transform.position);
		
		Vector3 dir = (target.transform.position - transform.position).normalized;
		float direction = Vector3.Dot(dir, transform.forward);
		
		Debug.Log(distance);
		
		if (distance < 2)
			if (direction > 0)
			{
				EnemyHealth eh = (EnemyHealth)target.GetComponent("EnemyHealth");
				eh.AdjustCurrentHealth(-10);
			}
	}
	
	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		chargeSpeed = 50;
		if (hit.gameObject.tag == "Ball")
		{
			Debug.Log("Hit a ball");
			hit.rigidbody.AddForce(transform.forward * chargeSpeed);
			hit.rigidbody.AddForce(transform.up * chargeSpeed * 2);
		}
	}
}














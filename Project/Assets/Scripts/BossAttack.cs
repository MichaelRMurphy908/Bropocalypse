using UnityEngine;
using System.Collections;

public class BossAttack: MonoBehaviour {
	
	public GameObject target;
	public float attackTimer;
	public float coolDown;
	public bool attacking;
	public bool bumping;
	
	// Use this for initialization
	void Start () 
	{
		attackTimer = 0;
		coolDown = 2.5f;
		attacking = false;
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(attackTimer > 0)
				attackTimer -= Time.deltaTime;
		if (attackTimer	< 0)
				attackTimer = 0;
		if (attackTimer == 0)
		{
			Attack();
			attackTimer = coolDown;
		}
	}
	
	private void Attack()
	{
		float distance = Vector3.Distance(target.transform.position, transform.position);
		
		Vector3 dir = (target.transform.position - transform.position).normalized;
		float direction = Vector3.Dot(dir, transform.forward);
		
		Debug.Log(distance);
		
		if (distance < 7.5f)
		{
			//Debug.Log (direction);
			if (direction < 0)
			{
				attacking = true;
				target.SendMessage("AdjustCurrentHealth",-10);			
			}
		}
		else if (distance > 0) attacking = false;
	}
	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		Debug.Log(hit.gameObject.name);
	}
	private void BumpAttack()
	{
		
	}
}

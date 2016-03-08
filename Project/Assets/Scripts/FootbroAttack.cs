using UnityEngine;
using System.Collections;

public class FootbroAttack : MonoBehaviour {
	
	public GameObject target;
	public float attackTimer;
	public float coolDown;
	public int chargeSpeed;
	public int attackSpeed;
	public float chargeTimer;
	public float chargeLength;
	public bool attacking;
	//public AudioSource gulp;
	public AudioClip charge;
	public AudioClip attack;
	public AudioClip gulp;
	public AudioClip eat;
	public AudioSource sound;
	PlayerHealth ph;
	
	
	// Use this for initialization
	void Start () 
	{
		attackTimer = 0;
		coolDown = 0.3f;
		chargeTimer = 0;
		chargeLength = 1.0f;
		animation["attack"].speed = 2.0f;
		animation["attack"].layer = 2;
		ph = gameObject.GetComponent<PlayerHealth>();
		sound = gameObject.GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if(chargeTimer <= 0)
		{
			gameObject.SendMessage("endCharge");
			if (chargeTimer < 0)
			{
				chargeTimer = 0;
			}
			if(attackTimer > 0)
				attackTimer -= Time.deltaTime;
			if (attackTimer	< 0)
				attackTimer = 0;
			if (attackTimer == 0)
			{
				attacking = false;
				if (Input.GetKeyDown(KeyCode.RightShift))
				{	
					Attack();
					attackTimer = coolDown;
				}
			}
			if (Input.GetKeyDown(KeyCode.LeftControl))
			{
				if (ph.curStam >= 50)
				{
					gameObject.SendMessage("beginCharge");
					ph.AdjustCurrentStam(-50);
					sound.clip = charge;
					sound.Play();
					chargeTimer = chargeLength;
				}
			}
		}
		else
		{
			chargeTimer -= Time.deltaTime;
		}
		
	}
	
	private void Attack()
	{
		/*float distance = Vector3.Distance(target.transform.position, transform.position);
		
		Vector3 dir = (target.transform.position - transform.position).normalized;
		float direction = Vector3.Dot(dir, transform.forward);
		
		//Debug.Log(distance);
		
		if (distance < 2)
			if (direction > 0)
			{
				EnemyHealth eh = (EnemyHealth)target.GetComponent("EnemyHealth");
				eh.AdjustCurrentHealth(-10);
			}
			*/
		if (ph.curStam >= 20)
		{
			ph.AdjustCurrentStam(-20);
			attacking = true;
			animation.CrossFade ("attack");
			sound.clip = attack;
			sound.Play();
		}
		
	}
	
	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		//Debug.Log("HIT");
		chargeSpeed = 1000;
		//attackSpeed = 100;
		if ((hit.gameObject.tag == "Enemy") || (hit.gameObject.tag =="EnemyBit"))
		{
			//EnemyHealth eh = hit.gameObject.GetComponent<EnemyHealth>();
			//Debug.Log(eh.curHealth);
			if (chargeTimer > 0)
			{
				//Debug.Log("Hit a ball");
				/*ActiveRagdoll ar = hit.gameObject.GetComponent<ActiveRagdoll>();
				if(ar != null)
				{
					if(!ar.IsRagdoll())
					{
						ar.Ragdoll();
					}
				}
				*/
				hit.rigidbody.AddForce(transform.forward * chargeSpeed);
				hit.rigidbody.AddForce(transform.up * (chargeSpeed / 1.5f));
				if (hit.gameObject.tag == "Enemy")
				{
					hit.gameObject.SendMessage("AdjustCurrentHealth",-20);
				}
				else if (hit.gameObject.tag =="EnemyBit")
				{
					hit.gameObject.transform.root.SendMessage("AdjustCurrentHealth", -20);
				}
			}    
			else if (attacking)
			{
				//eh.AdjustCurrentHealth(-5);
				if (hit.gameObject.tag == "Enemy")
				{
					hit.gameObject.SendMessage("Attacked");
					hit.gameObject.SendMessage("AdjustCurrentHealth",-5);
					hit.gameObject.SendMessage("Attacked1");
				}
				else if (hit.gameObject.tag =="EnemyBit")
				{
					hit.gameObject.transform.root.SendMessage("Attacked");
					hit.gameObject.transform.root.SendMessage("AdjustCurrentHealth", -5);
					hit.gameObject.transform.root.SendMessage("Attacked1");
				}
			}
			else hit.rigidbody.AddForce(transform.forward * 100);
		}
		else if(hit.gameObject.tag == "PickupC")
		{
			gameObject.SendMessage("AdjustCurrentStam",+40);
			//AudioSource gulp = (AudioSource)hit.gameObject.GetComponent("AudioSource");
			//gulp.Play();
			sound.clip = gulp;
			sound.Play();
			Destroy(hit.gameObject);
		}
		else if(hit.gameObject.tag == "PickupS")
		{
			gameObject.SendMessage("AdjustCurrentHealth",+30);
			//AudioSource gulp = (AudioSource)hit.gameObject.GetComponent("AudioSource");
			//gulp.Play();
			sound.clip = eat;
			sound.Play();
			Destroy(hit.gameObject);
		}
		if ((hit.gameObject.tag == "Boss") || (hit.gameObject.tag =="BossBits"))
		{
			//EnemyHealth eh = hit.gameObject.GetComponent<EnemyHealth>();
			//Debug.Log(eh.curHealth);
			if (chargeTimer > 0)
			{
				//Debug.Log("Hit a ball");
				/*ActiveRagdoll ar = hit.gameObject.GetComponent<ActiveRagdoll>();
				if(ar != null)
				{
					if(!ar.IsRagdoll())
					{
						ar.Ragdoll();
					}
				}
				*/
				hit.rigidbody.AddForce(transform.forward * chargeSpeed);
				hit.rigidbody.AddForce(transform.up * (chargeSpeed / 1.5f));
				if (hit.gameObject.tag == "Boss")
				{
					hit.gameObject.SendMessage("Attacked");
					hit.gameObject.SendMessage("AdjustCurrentHealth",-20);
					hit.gameObject.SendMessage("Attacked1");
				}
				else if (hit.gameObject.tag =="BossBits")
				{
					hit.gameObject.transform.root.SendMessage("Attacked");
					hit.gameObject.transform.root.SendMessage("AdjustCurrentHealth", -20);
					hit.gameObject.transform.root.SendMessage("Attacked1");
				}
			}    
			else if (attacking)
			{
				//eh.AdjustCurrentHealth(-5);
				if (hit.gameObject.tag == "Boss")
				{
					hit.gameObject.SendMessage("Attacked");
					hit.gameObject.SendMessage("AdjustCurrentHealth",-5);
					hit.gameObject.SendMessage("Attacked1");
				}
				else if (hit.gameObject.tag =="BossBits")
				{
					hit.gameObject.transform.root.SendMessage("Attacked");
					hit.gameObject.transform.root.SendMessage("AdjustCurrentHealth", -5);
					hit.gameObject.transform.root.SendMessage("Attacked1");
				}
			}
		}
	}
}














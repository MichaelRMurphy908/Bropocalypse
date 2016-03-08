using UnityEngine;
using System.Collections;

public class ZombieAnim : MonoBehaviour {
 
	public AudioClip walk;
	public AudioSource sound;	
	
	// Use this for initialization
	void Start () 
	{
				animation["attack1"].layer = 0;
				animation["attack2"].layer = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		GameObject zomb = this.gameObject;
		EnemyAI derp = (EnemyAI)zomb.GetComponent("EnemyAI");
		EnemyAttack herp = (EnemyAttack)zomb.GetComponent("EnemyAttack");
		ActiveRagdoll ar = (ActiveRagdoll)zomb.GetComponent ("ActiveRagdoll");
		Vector3 speed = derp.curMoveSpeed;
		bool attacking = herp.attacking;
		bool hurr = ar.IsRagdoll ();
		
		if (!hurr)
			if (!attacking)
			{
   				if (speed != Vector3.zero)
   			    	animation.CrossFade ("walk");
   				else
   		  			animation.CrossFade ("idle");
					
			}
			else
			{
				
				float comp = Random.Range (1, 100);
				if (comp > 50)
					animation.CrossFade("attack1");
				else animation.CrossFade("attack2");
			}
		
	}
}

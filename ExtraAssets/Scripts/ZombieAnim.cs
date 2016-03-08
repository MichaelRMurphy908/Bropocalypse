using UnityEngine;
using System.Collections;

public class ZombieAnim : MonoBehaviour {
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		GameObject zomb = GameObject.Find("Evil Cube 1");
		EnemyAI derp = (EnemyAI)zomb.GetComponent("EnemyAI");
		EnemyAttack herp = (EnemyAttack)zomb.GetComponent("EnemyAttack");
		Vector3 speed = derp.curMoveSpeed;
		bool attacking = herp.attacking;
		
		if (!attacking)
		{
   			if (speed != Vector3.zero)
   		    	animation.CrossFade ("walk");
   			else
   		  		animation.CrossFade ("idle");
		}
		else animation.CrossFade("attack");
	
	}
}

using UnityEngine;
using System.Collections;

public class BossAnim : MonoBehaviour {
	// Use this for initialization
	void Start () 
	{
				animation["attack1"].layer = 1;
				animation["attack2"].layer = 1;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		GameObject zomb = this.gameObject;
		BossAI derp = (BossAI)zomb.GetComponent("BossAI");
		BossAttack herp = (BossAttack)zomb.GetComponent("BossAttack");
		//ActiveRagdoll ar = (ActiveRagdoll)zomb.GetComponent ("ActiveRagdoll");
		Vector3 speed = derp.curMoveSpeed;
		bool attacking = herp.attacking;
			if (!attacking)
			{
   			  animation.CrossFade ("walk");
					
			}
			else
			{
				animation.CrossFade("attack2");
			}
		
	}
}

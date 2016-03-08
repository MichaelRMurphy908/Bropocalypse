using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour {
	
	public float maxHealth = 60f;
	public float curHealth = 60f;
	public float healthBarLength;
	public Transform bar;
	public float planeScaleX;
	public float timer = 0;
	public float attackedTimer = 0.5f;
	
	// Use this for initialization
	void Start () 
	{
		//healthBarLength = Screen.width / 2;
		//bar = GameObject.Find("/Ragdolled Zombie/HealthBar");
		bar = transform.Find("HealthBar").transform;
		planeScaleX = bar.localScale.x;
		//Debug.Log(planeScaleX);
		healthBarLength = planeScaleX;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer -= Time.deltaTime;
		if (timer < 0)
			timer = 0;
		AdjustCurrentHealth(0);
	}
	
	
	
	public void AdjustCurrentHealth(int adj)
	{
	  if (timer == 0)
	  {
		curHealth += adj;
		
		if(curHealth < 0)
			curHealth =0;
		if(curHealth > maxHealth)
			curHealth = maxHealth;
		if (maxHealth < 1)
				maxHealth = 1;
		healthBarLength = (curHealth / maxHealth) * planeScaleX;
		bar.localScale = new Vector3(healthBarLength,0f,0.05f);
		if (curHealth <= 0)
		{
			Application.LoadLevel(4);
		}
	  }
			
	}
	
	public void Attacked1()
	{
		//Debug.Log("ATTACKED");
		if (timer == 0)
			timer = attackedTimer;
	}
}

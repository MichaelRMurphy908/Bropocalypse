using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	
	public int maxHealth = 100;
	public int curHealth = 100;
	public int maxStam;
	public int curStam;
	public float stamRegenTime;
	public float timer;
	
	public float healthBarLength;
	public float stamBarLength;
	
	// Use this for initialization
	void Start () 
	{
	healthBarLength = Screen.width / 2;
	stamBarLength = Screen.width / 2;
	timer = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		AdjustCurrentHealth(0);
		
		timer += Time.deltaTime;
		if (timer >= stamRegenTime) //stam regen
		{
			AdjustCurrentStam(5);
			timer = 0f;
		}
		else AdjustCurrentStam(0);
		
	}
	
	void OnGUI() 
	{
		GUI.Box(new Rect(10, 10, healthBarLength, 20),"Health: " + curHealth + "/" + maxHealth);
		GUI.Box(new Rect(10, 30, stamBarLength, 20), "Stamina: " + curStam + "/" + maxStam);
		
	}
	
	public void AdjustCurrentHealth(int adj)
	{
		curHealth += adj;
		
		if(curHealth < 0)
			curHealth =0;
		if(curHealth > maxHealth)
			curHealth = maxHealth;
		if (maxHealth < 1)
				maxHealth = 1;
		healthBarLength = (Screen.width /2) * (curHealth / (float)maxHealth);
		if(curHealth<=0)
			Application.LoadLevel(5);
	}
	public void AdjustCurrentStam(int adj)
	{
		curStam += adj;
		
		if(curStam < 0)
			curStam =0;
		if(curStam > maxStam)
			curStam = maxStam;
		if (maxStam < 1)
				maxStam = 1;
		stamBarLength = (Screen.width /2) * (curStam / (float)maxStam);
	}
}

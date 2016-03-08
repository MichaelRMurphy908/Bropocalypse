using UnityEngine;
using System.Collections;

public class NarrativeCamera : MonoBehaviour {
	
	public Vector3[] positions = new Vector3[7];
	public int curFrame = 0;
	public float[] frameReadTime = new float[7];
	public float timer;
	bool pan = false;
	public float speed;
	public float derp;   //tracking time
	//float alphaFadeValue = 0;
	//Vector3 newPos;

	// Use this for initialization
	void Start () {
	
		positions[0] = new Vector3(-2.790625f,5.712813f,2.527897f);
		positions[1] = new Vector3(-2.790625f,2.669456f,0.002584859f);
		positions[2] = new Vector3(-2.790625f,5.740544f,-2.061592f);
		positions[3] = new Vector3(-2.790625f,-0.4255323f,2.464893f);
		positions[4] = new Vector3(-2.790625f,-0.6645269f,-2.481257f);
		positions[5] = new Vector3(-2.790625f,-3.902105f,2.640705f);
		positions[6] = new Vector3(-3.402766f,-5.032688f,-2.104192f);
		
		frameReadTime[0] = 12f;
		frameReadTime[1] = 12f;
		frameReadTime[2] = 2f;
		frameReadTime[3] = 12f;
		frameReadTime[4] = 12f;
		frameReadTime[5] = 5f;
		frameReadTime[6] = 16f;
		
		transform.position = positions[0];
		timer = frameReadTime[0];
	}
	
	// Update is called once per frame
	void Update () {
	
		if(curFrame < 7)
		{
			if(!(timer <= 0))
			{
				if(pan)
				{	
					float herp = Time.deltaTime;
					transform.position = Vector3.Lerp(transform.position, positions[curFrame], herp);
					derp += herp / 3.3f;
					//timer = Mathf.Abs(derp);
					timer = derp;
					if (timer >= 1f)
					{
						timer = 0;
					}
					
				}
				else
				{
					timer -= Time.deltaTime;
					derp = 0;
				}
					
			}
			else
			{
				if(pan)
				{
					pan = false;
					timer = frameReadTime[curFrame];
				}
				else
				{
					timer = 1;
					pan = true;
					curFrame++;
				}
			}
		}
		else
		{
			Application.LoadLevel(2);
		}
	
	}
}

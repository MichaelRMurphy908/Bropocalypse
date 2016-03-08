using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	
	public Transform target;
	public int moveSpeed;
	public int rotationSpeed;
	public int maxDistance;
	public Vector3 curMoveSpeed;
	
	private Transform myTransform;
	
	void Awake()
	{
		myTransform = transform;		
	}
	
	// Use this for initialization
	void Start () 
	{
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		
		target = go.transform;
		maxDistance = 2;
		curMoveSpeed = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.DrawLine(target.position, myTransform.position, Color.green);
		
		//Look at target
		
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
		
		if (Vector3.Distance(target.position, myTransform.position) > maxDistance)
		{
			//Move Towards Target
			curMoveSpeed = myTransform.forward * moveSpeed * Time.deltaTime;
			myTransform.position += curMoveSpeed;
		}
		
	}
	
}


 
using UnityEngine;
using System.Collections;

public class TwoDCameraFollow : MonoBehaviour {
public Transform target;
public float height = 15.0f;
public float distance = 30.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void LateUpdate()
	{
	Vector3 temp = new Vector3(0, height, distance);
    transform.position = target.position + temp;
    transform.LookAt (target); 
	}
}
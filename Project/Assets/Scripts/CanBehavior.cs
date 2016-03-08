using UnityEngine;
using System.Collections;

public class CanBehavior : MonoBehaviour {
	public AudioSource gulp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnDestroy()
	{
		gulp.Play();
	}
}

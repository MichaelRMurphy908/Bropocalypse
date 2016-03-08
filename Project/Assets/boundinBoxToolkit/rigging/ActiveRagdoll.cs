using UnityEngine;
using System.Collections;


public class ActiveRagdoll : MonoBehaviour
{
	bool debug_ragdoll = true;
	
	public GameObject animationObject;
	
	// transform the ragdoll root is relative to, used for recovery
	public Transform ragdollLocator = null;
	
	// animation and time to recover from ragdoll
	public string recoverAnimation = "walk";
	public float recoverTime = 1.0f;
	
	// character controller
	public CharacterController character = null;
	
	// ragdoll initialization
	public bool ragdoll = false;
	
	public float freezeForce = 0.0f;
	
	// ragdoll current state
	private bool _ragdoll = false;
	
	// ragdoll locator initial offset, used for recovery
	private Vector3 _ragdollOffset;
	
	public RagdollJoint[] _ragdollJoints = null;
	
	private float _freezeForce = -10.0f;
		
	void Reset()
	{
		Animation anim = GetComponent<Animation>();

		if (anim != null)
		{
			animationObject = anim.gameObject;
			anim.animatePhysics = true;
		}
	}
	
	void Start()
	{
		Initialize();
		
		Ragdoll(transform, ragdoll);
	}
	
	void Update()
	{
		// DEBUG: Toggle ragdoll with R key
		if (debug_ragdoll)
		{
			//if (Input.GetKeyDown(KeyCode.R)) Ragdoll(!_ragdoll);
			
			// debug freeze force
			if (Input.GetKeyDown(KeyCode.Alpha0)) SetFreezeForce(0.0f);
			if (Input.GetKeyDown(KeyCode.Alpha1)) SetFreezeForce(0.1f);
			if (Input.GetKeyDown(KeyCode.Alpha2)) SetFreezeForce(0.5f);
			if (Input.GetKeyDown(KeyCode.Alpha3)) SetFreezeForce(1.0f);
			if (Input.GetKeyDown(KeyCode.Alpha4)) SetFreezeForce(2.0f);
			if (Input.GetKeyDown(KeyCode.Alpha5)) SetFreezeForce(4.0f);
			if (Input.GetKeyDown(KeyCode.Alpha6)) SetFreezeForce(8.0f);
			if (Input.GetKeyDown(KeyCode.Alpha7)) SetFreezeForce(10.0f);
			if (Input.GetKeyDown(KeyCode.Alpha8)) SetFreezeForce(20.0f);
			if (Input.GetKeyDown(KeyCode.Alpha9)) SetFreezeForce(100.0f);
		}
		//ragdollLocator = animationObject.transform;

		if (_ragdoll)
		{
			RecenterRagdoll();
			SetFreezeForce(freezeForce);
		}
	}
	
	void SetFreezeForce(float argFreezeForce)
	{
		if (argFreezeForce == _freezeForce)
		{
			return;
		}
			
		freezeForce = Mathf.Max(0.0f, argFreezeForce);
		
		foreach (RagdollJoint rj in _ragdollJoints)
			rj.SetFreezeForce(freezeForce);
		
		_freezeForce = freezeForce;
	}
	
	// Recenter character controller to ragdollLocator position
	void RecenterRagdoll()
	{
		Vector3 pos = ragdollLocator.position;
		Vector3 diff = (ragdollLocator.position - _ragdollOffset) - transform.position;
		
		if (character != null)
			character.Move(diff);
		else
			transform.position = ragdollLocator.position - _ragdollOffset;
		
		ragdollLocator.position = pos;
	}
	
	// Add Velocity to all ragdollJoints
	public void AddVelocity(Vector3 argVelocity)
	{
		foreach(RagdollJoint j in _ragdollJoints)
		{
			j.rigidbody.velocity = j.rigidbody.velocity + argVelocity;
		}
	}
	
	// Apply Velocity to all ragdollJoints
	public void ApplyVelocity(Vector3 argVelocity)
	{
		foreach(RagdollJoint j in _ragdollJoints)
		{
			j.rigidbody.velocity = argVelocity;
		}
	}
	
	// Determine pathname of transform
	string GetPathName(Transform argTransform)
	{
		Transform temp = argTransform.parent;
		string str = argTransform.name;
		
		while ((temp != animationObject.transform) && (temp != null))
		{
			str = temp.name + "/" + str;
			
			temp = temp.parent;
		}
		
		if (temp == null)
			return str;
		
		return str;
	}
	
	// Initialize Ragdoll
	void Initialize()
	{
		_ragdollOffset = ragdollLocator.position - transform.position;
		
		Collider[] colliders = GetComponentsInChildren<Collider>();
		Collider mainCollider = (character != null)?character:collider;
		
		foreach (Collider col in colliders)
		{
			if (col != mainCollider)
				Physics.IgnoreCollision(mainCollider, col);
		}
		
		Joint[] joints = GetComponentsInChildren<Joint>();
		
		foreach (Joint joint in joints)
		{
			joint.gameObject.AddComponent<RagdollJoint>();
		}
		
		_ragdollJoints = GetComponentsInChildren<RagdollJoint>();
	}
	
	// Create animation out of current ragdoll pose
	void MakeAnimationClip()
	{
		Animation anim = animationObject.animation;
		
		AnimationClip clip = new AnimationClip();
		
		Transform[] tms = GetComponentsInChildren<Transform>();
		
		for (int i=0; i<tms.Length; ++i)
		{
			if ((tms[i] == animationObject.transform) || (tms[i] == transform))
				continue;
			
			clip.SetCurve(GetPathName(tms[i]), typeof(Transform), "localPosition.x", AnimationCurve.Linear(0.0f, tms[i].localPosition.x, 1.0f, tms[i].localPosition.x));
			clip.SetCurve(GetPathName(tms[i]), typeof(Transform), "localPosition.y", AnimationCurve.Linear(0.0f, tms[i].localPosition.y, 1.0f, tms[i].localPosition.y));
			clip.SetCurve(GetPathName(tms[i]), typeof(Transform), "localPosition.z", AnimationCurve.Linear(0.0f, tms[i].localPosition.z, 1.0f, tms[i].localPosition.z));
			
			clip.SetCurve(GetPathName(tms[i]), typeof(Transform), "localRotation.w", AnimationCurve.Linear(0.0f, tms[i].localRotation.w, 1.0f, tms[i].localRotation.w));
			clip.SetCurve(GetPathName(tms[i]), typeof(Transform), "localRotation.x", AnimationCurve.Linear(0.0f, tms[i].localRotation.x, 1.0f, tms[i].localRotation.x));
			clip.SetCurve(GetPathName(tms[i]), typeof(Transform), "localRotation.y", AnimationCurve.Linear(0.0f, tms[i].localRotation.y, 1.0f, tms[i].localRotation.y));
			clip.SetCurve(GetPathName(tms[i]), typeof(Transform), "localRotation.z", AnimationCurve.Linear(0.0f, tms[i].localRotation.z, 1.0f, tms[i].localRotation.z));
		}
			
		anim.AddClip(clip, "active_ragdoll_clip");
	}	
	
	// Full ragdoll (de)activation
	public void Ragdoll(bool argEnable)
	{
		if (_ragdoll == argEnable)
			return;
		
		Ragdoll(transform, argEnable);
		
		if (argEnable)
		{
			animationObject.animation.Stop();
			
			if (character != null)
				ApplyVelocity(character.velocity);
		}
		else
		{
			RecenterRagdoll();
			
			MakeAnimationClip();
			animationObject.animation.CrossFade("active_ragdoll_clip", 0.0f);
			animationObject.animation.CrossFade(recoverAnimation, recoverTime);
		}		
		
		foreach (RagdollJoint rj in _ragdollJoints)
		{
			if (argEnable && (freezeForce > 0.0f))
				rj.Freeze(freezeForce);
			else
				rj.Unfreeze();
		}
		
		/*if (GetComponent<ThirdPersonController>())
			GetComponent<ThirdPersonController>().enabled = !argEnable;*/
		
		_ragdoll = argEnable;
	}

	public void Ragdoll()
	{
		Ragdoll(true);
	}

	public void Ragdoll(Vector3 argPoint, float argForce, float argRadius)
	{
		float dist;
		Vector3 vec;

		Ragdoll(true);

		foreach(RagdollJoint j in _ragdollJoints)
		{
			vec = j.transform.position - argPoint;
			dist = vec.magnitude;

			if (dist >= argRadius)
				continue;

			vec = vec / dist;

			dist = 1.0f - dist / argRadius;

			j.rigidbody.AddForce(vec * argForce * dist, ForceMode.Impulse);
		}
	}

	// Propagate ragdoll (de)activation to children
	void Ragdoll(Transform argTransform, bool argEnable)
	{
		if (argTransform.rigidbody)
		{
			argTransform.rigidbody.isKinematic = !argEnable;
			
			/* DEBUG: Null out rigidbody values
			
			argTransform.rigidbody.angularVelocity = Vector3.zero;
			argTransform.rigidbody.velocity = Vector3.zero;
			argTransform.rigidbody.useGravity = false;*/
		}
		
		foreach (Transform t in argTransform)
			Ragdoll(t, argEnable);
	}
	public bool IsRagdoll ()
	{
		bool derp = _ragdoll;
		return derp;
	}
}

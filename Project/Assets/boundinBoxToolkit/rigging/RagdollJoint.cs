using UnityEngine;
using System.Collections;

public class RagdollJoint : MonoBehaviour
{
	ConfigurableJoint _joint = null;
	
	public void Freeze(float argForce)
	{
		if (argForce <= 0.0f)
		{
			Unfreeze();
			return;
		}

		if (_joint != null)
			return;

		Joint joint = GetComponent<Joint>();
		
		if (_joint == null)
			_joint = gameObject.AddComponent<ConfigurableJoint>();

		//_joint.targetRotation = l_rot;
		_joint.rotationDriveMode = RotationDriveMode.Slerp;
		
		_joint.xMotion = _joint.yMotion = _joint.zMotion = ConfigurableJointMotion.Locked;
		
		SetFreezeForce(argForce);
		
		_joint.configuredInWorldSpace = false;
		
		_joint.connectedBody = joint.connectedBody;
		
		/*rigidbody.rotation = rot;
		transform.localRotation = l_rot;*/
	}
	
	public void Unfreeze()
	{
		if (_joint != null)
			Destroy(_joint);
	}
	
	public void SetFreezeForce(float argForce)
	{
		Freeze(argForce);
		
		if (_joint == null)
			return;
		
		JointDrive jd = _joint.slerpDrive;
		jd.mode = JointDriveMode.Position;
		jd.maximumForce = argForce;
		jd.positionSpring = argForce;
		jd.positionDamper = 1.0f;
		
		_joint.slerpDrive = jd;
	}
	
	/*void OnDrawGizmos()
	{
		Gizmos.DrawRay(transform.position, rigidbody.velocity);
	}*/
}

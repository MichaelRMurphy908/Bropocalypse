using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(ActiveRagdoll))]

public class ActiveRagdollEditor : Editor
{
	private bool _showTools = false;

	public float force = -1.0f;
	public float forceVariance = 0.0f;

	public float torque = -1.0f;
	public float torqueVariance = 0.0f;

	override public void OnInspectorGUI()
	{
		ActiveRagdoll ar = (ActiveRagdoll)target;
		
		DrawDefaultInspector();
		
		_showTools = EditorGUILayout.Foldout(_showTools, "Ragdoll Tools");
		
		
		if (_showTools)
		{
			float mass = GetMass(ar);
			float mass2 = EditorGUILayout.FloatField("Mass", mass);

			if (mass != mass2)
				SetMass(ar, mass2);

			if (GUILayout.Button("Normalize Density"))
				NormalizeDensity(ar);

			if (GUILayout.Button("Unify Density"))
				UnifyDensity(ar);

			EditorGUILayout.Separator();
			force = EditorGUILayout.FloatField("Break Force (<0 = inf)", force);
			forceVariance = EditorGUILayout.FloatField("Variance", forceVariance);
			if (GUILayout.Button("Set Joint Break Force"))
				SetJointBreakForce(ar, force, forceVariance);

			EditorGUILayout.Separator();
			torque = EditorGUILayout.FloatField("Break Torque (<0 = inf)", torque);
			torqueVariance = EditorGUILayout.FloatField("Variance", torqueVariance);
			if (GUILayout.Button("Set Joint Break Torque"))
				SetJointBreakTorque(ar, torque, torqueVariance);

			EditorGUILayout.Separator();
		}
	}
	
	void SetMass(ActiveRagdoll argRagdoll, float argMass)
	{
		Rigidbody[] rbs = argRagdoll.GetComponentsInChildren<Rigidbody>();
		
		float mass = 0.0f;
		
		foreach (Rigidbody r in rbs)
			mass += r.mass;
		
		float diff = argMass / mass;
		
		foreach (Rigidbody r in rbs)
			r.mass *= diff;
	}
	
	float GetMass(ActiveRagdoll argRagdoll)
	{
		Rigidbody[] rbs = argRagdoll.GetComponentsInChildren<Rigidbody>();
		
		float mass = 0.0f;
		
		foreach (Rigidbody r in rbs)
			mass += r.mass;
		
		return mass;
	}
	
	void UnifyDensity(ActiveRagdoll argRagdoll)
	{
		float mass = GetMass(argRagdoll);
		
		Rigidbody[] rbs = argRagdoll.GetComponentsInChildren<Rigidbody>();
		
		foreach (Rigidbody r in rbs)
			r.SetDensity(1.0f);
		
		SetMass(argRagdoll, mass);
	}
	
	void NormalizeDensity(ActiveRagdoll argRagdoll)
	{
		float mass = GetMass(argRagdoll);

		Rigidbody[] rbs = argRagdoll.GetComponentsInChildren<Rigidbody>();

		foreach (Rigidbody r in rbs)
			r.mass = 1.0f;

		SetMass(argRagdoll, mass);
	}

	void SetJointBreakForce(ActiveRagdoll argRagdoll, float argValue, float argVariance)
	{
		Joint[] joints = argRagdoll.GetComponentsInChildren<Joint>();

		float val = argValue + Random.Range(-argVariance, argVariance);

		if (argValue < 0.0f)
			val = Mathf.Infinity;

		foreach (Joint j in joints)
			j.breakForce = val;
	}

	void SetJointBreakTorque(ActiveRagdoll argRagdoll, float argValue, float argVariance)
	{
		Joint[] joints = argRagdoll.GetComponentsInChildren<Joint>();

		float val = argValue + Random.Range(-argVariance, argVariance);

		if (argValue < 0.0f)
			val = Mathf.Infinity;
		
		foreach (Joint j in joints)
			j.breakTorque = val;
	}
}

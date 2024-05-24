using System;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	[Range(0f, 1f)]
	[SerializeField] private float _exploadingBase;
	[SerializeField] private float _explodingForseMultiplyer;

	private float _explodingRadius;
	private float _explodingForse;

	private void Start()
	{
		_explodingRadius = -(float)Math.Log(transform.localScale.x, _exploadingBase);
		_explodingForse = -(float)(Math.Log(transform.localScale.x, _exploadingBase) * _explodingForseMultiplyer);
	}

	public void ExploadCubesInRadius()
	{
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explodingRadius);
		ExploadCubes(hitColliders);
	}

	public void ExploadCubes(Collider[] hitColliders)
	{
		foreach (Collider hitCollider in hitColliders)
			hitCollider.attachedRigidbody?.AddExplosionForce(_explodingForse, transform.position, _explodingRadius);
	}
}

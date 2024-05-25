using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Explosion : MonoBehaviour
{
	[SerializeField] private float _explodingRadiusMultiplyer;
	[SerializeField] private float _explodingForseMultiplyer;

	private float _explodingRadius;
	private float _explodingForse;
	private float _startExplodingRadius = 400;
	private float _startExplodingForse = 400;

	private void Start()
	{
		_explodingRadius = 1.5f / transform.localScale.x * _explodingRadiusMultiplyer + _startExplodingRadius;
		_explodingForse = 1.5f / transform.localScale.x * _explodingForseMultiplyer + _startExplodingForse;

		print("Сила - " + _explodingForse + "радиус - " + _explodingRadius);
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

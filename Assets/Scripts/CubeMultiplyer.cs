using System;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class CubeMultiplyer : MonoBehaviour
{
	[SerializeField] private GameObject _folder;
	[SerializeField] private float _divideValue;
	[SerializeField] private float _explodingForseMultiplyer;

	[Range(0f, 1f)]
	[SerializeField] private float _exploadingBase;
	private float _divideAllChanse = 101;
	private float _divideWinChanse = 101;
	private float _divideChanseDivider = 2;

	private int _minNewCubeCount = 1;
	private int _maxNewCubeCount = 6;

	private float _explodingRadius;
	private float _explodingForse;

	public void SetChanses(float winChanse) => _divideWinChanse = winChanse;

	private void Start()
	{
		_explodingRadius = -(float)Math.Log(transform.localScale.x, _exploadingBase);
		_explodingForse = -(float)(Math.Log(transform.localScale.x, _exploadingBase) * _explodingForseMultiplyer);
		gameObject.GetComponent<MeshRenderer>().material.color = UnityEngine.Random.ColorHSV();
	}

	private void OnMouseUpAsButton()
	{
		if (TryWinRandom())
			SpawnCubes();
		else
			Exploading();
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(transform.position, _explodingRadius);
	}

	private void Exploading()
	{
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explodingRadius);

		foreach (Collider hitCollider in hitColliders)
			hitCollider.attachedRigidbody?.AddExplosionForce(_explodingForse, transform.position, _explodingRadius);

		gameObject.SetActive(false);
	}

	private bool TryWinRandom()
	{
		bool isWin = UnityEngine.Random.Range(0, _divideAllChanse) <= _divideWinChanse;
		_divideWinChanse /= _divideChanseDivider;
		return isWin;
	}

	private void SpawnCubes()
	{
		gameObject.transform.localScale /= _divideValue;
		float cubeSpawnCount = UnityEngine.Random.Range(_minNewCubeCount, _maxNewCubeCount);

		for (int i = 0; i < cubeSpawnCount; i++)
			Instantiate(gameObject, _folder.transform).GetComponent<CubeMultiplyer>().SetChanses(_divideWinChanse);
	}
}
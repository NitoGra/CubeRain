using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CubeMultiplyer), typeof(BoxCollider))]
public class CubeSpawner : MonoBehaviour
{
	[SerializeField] private GameObject _folder;
	[SerializeField] private float _divideValue = 2;

	private CubeMultiplyer _cube;
	private int _minNewCubeCount = 2;
	private int _maxNewCubeCount = 6;

	private void Start()
	{
		_cube = GetComponent<CubeMultiplyer>();
	}

	public Collider[] SpawnCubes(int chanses)
	{
		gameObject.transform.localScale /= _divideValue;
		int cubeSpawnCount = UnityEngine.Random.Range(_minNewCubeCount, _maxNewCubeCount);
		Collider[] spawnedCubes = new Collider[cubeSpawnCount];

		for (int i = 0; i < cubeSpawnCount; i++)
		{
			CubeMultiplyer cube = Instantiate(_cube, _folder.transform);
			cube.SetChances(chanses);
			spawnedCubes[i] = GetGuaranteedBoxCollider(cube);
		}

		return spawnedCubes;
	}

	private BoxCollider GetGuaranteedBoxCollider(CubeMultiplyer cube)
	{
		if (cube.TryGetComponent(out BoxCollider cubeCollider) == false)
			cubeCollider = cube.AddComponent<BoxCollider>();

		return cubeCollider;
	}
}
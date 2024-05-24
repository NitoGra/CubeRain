using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer), typeof(Explosion))]
public class CubeMultiplyer : MonoBehaviour
{
	[SerializeField] private GameObject _folder;
	[SerializeField] private float _divideValue;

	[SerializeField] private bool _exploadOnDestroy;
	[SerializeField] private bool _exploadOnSpawn;

	private Explosion _explosion;
	private float _divideAllChanse = 101;
	private float _divideWinChanse = 101;
	private float _divideChanseDivider = 2;

	private int _minNewCubeCount = 2;
	private int _maxNewCubeCount = 6;

	private void Start()
	{
		GetComponent<MeshRenderer>().material.color = UnityEngine.Random.ColorHSV();
		_explosion = GetComponent<Explosion>();
	}

	private void OnMouseUpAsButton()
	{
		if (TrySlicing())
			SpawnCubes();
		else
			DestroyCube();
	}

	public void SetChances(float winChanse) => _divideWinChanse = winChanse;

	private void DestroyCube()
	{
		if (_exploadOnDestroy)
			_explosion.ExploadCubesInRadius();

		gameObject.SetActive(false);
	}

	private bool TrySlicing()
	{
		bool isWin = UnityEngine.Random.Range(0, _divideAllChanse) <= _divideWinChanse;
		_divideWinChanse /= _divideChanseDivider;
		return isWin;
	}

	private void SpawnCubes()
	{
		gameObject.transform.localScale /= _divideValue;
		int cubeSpawnCount = UnityEngine.Random.Range(_minNewCubeCount, _maxNewCubeCount);
		Collider[] spawnedCubes = new Collider[cubeSpawnCount];

		for (int i = 0; i < cubeSpawnCount; i++)
		{
			CubeMultiplyer cube = Instantiate(this, _folder.transform);
			cube.SetChances(_divideWinChanse);
			spawnedCubes[i] = cube.GetComponent<Collider>();
		}

		if (_exploadOnSpawn)
			_explosion.ExploadCubes(spawnedCubes);

		gameObject.SetActive(false);
	}
}
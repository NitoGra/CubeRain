using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(Explosion),typeof(CubeSpawner))]
[RequireComponent(typeof(BoxCollider))]
public class CubeMultiplyer : MonoBehaviour
{
	[SerializeField] private bool _exploadOnDestroy;
	[SerializeField] private bool _exploadOnSpawn;

	private Explosion _explosion;
	private CubeSpawner _spawner;
	private float _divideAllChanse = 101;
	private float _divideWinChanse = 101;
	private float _divideChanseDivider = 2;

	private void Start()
	{
		GetComponent<MeshRenderer>().material.color = UnityEngine.Random.ColorHSV();
		_explosion = GetComponent<Explosion>();
		_spawner = GetComponent<CubeSpawner>();
	}

	private void OnMouseUpAsButton()
	{
		if (CanSlicing())
			SpawnAndExploadeCubes();
		else
			DestroyCube();
	}

	public void SetChances(float winChanse) => _divideWinChanse = winChanse;

	private void SpawnAndExploadeCubes()
	{
		Collider[] spawnedCubes = _spawner.SpawnCubes(_divideWinChanse);

		if (_exploadOnSpawn)
			_explosion.ExploadCubes(spawnedCubes);

		gameObject.SetActive(false);
	}

	private void DestroyCube()
	{
		if (_exploadOnDestroy)
			_explosion.ExploadCubesInRadius();

		gameObject.SetActive(false);
	}

	private bool CanSlicing()
	{
		bool isWin = UnityEngine.Random.Range(0, _divideAllChanse) <= _divideWinChanse;
		_divideWinChanse /= _divideChanseDivider;
		return isWin;
	}
}
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(Explosion),typeof(CubeSpawner))]
[RequireComponent(typeof(BoxCollider))]
public class CubeMultiplyer : MonoBehaviour
{
	[SerializeField] private bool _exploadOnDestroy;
	[SerializeField] private bool _exploadOnSpawn;

	private Explosion _explosion;
	private CubeSpawner _spawner;
	private int _divideAllChanse = 101;
	private int _divideWinChanse = 100;
	private int _divideChanseDivider = 2;

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

		gameObject.SetActive(false);
	}

	public void SetChances(int winChanse) => _divideWinChanse = winChanse;

	private void SpawnAndExploadeCubes()
	{
		Collider[] spawnedCubes = _spawner.SpawnCubes(_divideWinChanse);

		if (_exploadOnSpawn)
			_explosion.ExploadCubes(spawnedCubes);

	}

	private void DestroyCube()
	{
		if (_exploadOnDestroy)
			_explosion.ExploadCubesInRadius();
	}

	private bool CanSlicing()
	{
		bool isWin = UnityEngine.Random.Range(0, _divideAllChanse) <= _divideWinChanse;
		_divideWinChanse /= _divideChanseDivider;
		return isWin;
	}
}
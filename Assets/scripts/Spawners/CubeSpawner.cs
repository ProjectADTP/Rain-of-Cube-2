using UnityEngine;
using System.Collections.Generic;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;

    private List<Vector3> _cubePositions = new List<Vector3>();

    protected override void Start()
    {
        base.Start();

        StartCoroutine(SpawnObjects());

        OnObjectReturnedWithPosition += HandleCubeReturned;
    }

    private void OnDestroy()
    {
        OnObjectReturnedWithPosition -= HandleCubeReturned;
    }

    protected override void OnObjectDestroyed(Cube cube)
    {
        _cubePositions.Add(cube.transform.position);

        base.OnObjectDestroyed(cube);
    }

    private void HandleCubeReturned(Vector3 position)
    {
        _bombSpawner.SpawnBombAtPosition(position);
    }
}

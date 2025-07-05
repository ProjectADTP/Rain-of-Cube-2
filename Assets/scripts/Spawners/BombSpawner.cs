using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private float _explosionRadius = 3f;
    [SerializeField] private float _explosionForce = 500f;

    public void SpawnBombAtPosition(Vector2 position)
    {
        Bomb bomb = _pool.Get();

        if (bomb != null)
        {
            bomb.transform.position = position;
            bomb.SetupBomb(_explosionRadius, _explosionForce);
        }
    }
}

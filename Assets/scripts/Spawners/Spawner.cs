using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour, IPoolable<T>
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected int _poolSize = 10;
    [SerializeField] protected int _poolMaxSize = 20;
    [SerializeField] protected float _spawnTime = 1f;

    protected ObjectPool<T> _pool;
    protected float _rangeSpawn = 10f;

    protected WaitForSeconds _wait;

    public event Action<T> OnObjectStateChanged;

    public event Action<Vector3> OnObjectReturnedWithPosition;

    public int TotalSpawned { get; protected set; }   
    public int TotalCreated { get; protected set; }    
    public int ActiveCount => _pool.CountActive;      

    protected void Awake()
    {
        _wait = new WaitForSeconds(_spawnTime);
    }

    protected virtual void Start()
    {
        TotalSpawned = 0;
        TotalCreated = 0;

        _pool = new ObjectPool<T>
        (
            createFunc: () => 
            {
                T obj = Instantiate(_prefab, transform);
                TotalCreated++;
                obj.Initialize(OnObjectDestroyed);

                OnObjectStateChanged.Invoke(obj);

                return obj;
            },
            actionOnGet: obj => 
            {
                obj.gameObject.SetActive(true);
                TotalSpawned++;

                OnObjectStateChanged.Invoke(obj);
            },
            actionOnRelease: obj =>
            {
                obj.gameObject.SetActive(false);

                OnObjectStateChanged.Invoke(obj);
            },
            actionOnDestroy: obj => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: _poolSize,
            maxSize: _poolMaxSize
        );
    }

    protected virtual IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return _wait;

            SpawnObject();
        }
    }

    protected virtual void SpawnObject()
    {
        T obj = _pool.Get();

        if (obj != null)
        {
            obj.transform.position = new Vector2(UnityEngine.Random.Range(transform.position.x - _rangeSpawn,
                                                 transform.position.x + _rangeSpawn),transform.position.y);

            obj.Initialize(OnObjectDestroyed);
        }
    }

    protected virtual void OnObjectDestroyed(T obj)
    {
        Vector3 position = obj.transform.position;

        _pool.Release(obj);

        OnObjectReturnedWithPosition?.Invoke(position);
    }
}

public interface IPoolable<T> where T : MonoBehaviour
{
    public void Initialize(Action<T> onDestroyAction);
}
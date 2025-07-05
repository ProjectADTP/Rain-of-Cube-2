using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(ColorChanger))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public abstract class PoolableObject : MonoBehaviour, IPoolable
{
    [Header("Common Settings")]
    [SerializeField] protected float MinLiveTime;
    [SerializeField] protected float MaxLiveTime;

    protected ColorChanger ColorChanger;
    protected Rigidbody Rigidbody;
    protected Renderer Renderer;

    protected Action<IPoolable> OnDestroyAction;
    protected bool HasCollided = false;

    protected float LifeTime;

    protected virtual void Awake()
    {
        ColorChanger = GetComponent<ColorChanger>();
        Rigidbody = GetComponent<Rigidbody>();
        Renderer = GetComponent<Renderer>();

        LifeTime = UnityEngine.Random.Range(MinLiveTime, MaxLiveTime);
    }

    public void Initialize(Action<IPoolable> onDestroyAction)
    {
        OnDestroyAction = onDestroyAction;
    }

    public virtual void ResetState()
    {
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.angularVelocity = Vector3.zero;

        Renderer.material.color = Color.white;

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        HasCollided = false;
    }

    protected IEnumerator ReturnToPoolAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        OnDestroyAction?.Invoke(this);

        ResetState();
    }
}
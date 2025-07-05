using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : PoolableObject, IPoolable<Bomb>
{
    private float _explosionRadius;
    private float _explosionForce;

    private Action<Bomb> _onDestroy;

    protected override void Awake()
    {
        base.Awake();

        Renderer.material.SetFloat("_Mode", 3);
    }

    public void Initialize(Action<Bomb> onDestroyAction)
    {
        _onDestroy = onDestroyAction;
    }

    public void SetupBomb(float explosionRadius, float explosionForce)
    {
        _explosionRadius = explosionRadius;
        _explosionForce = explosionForce;

        ResetState();

        StartCoroutine(FadeAndExplode());
    }

    public override void ResetState()
    {
        base.ResetState();

        Renderer.material.color = Color.black;
    }

    private IEnumerator FadeAndExplode()
    {
        ColorChanger.ChangeAlpha(0f, LifeTime);

        yield return new WaitForSeconds(LifeTime);

        Explode();

        _onDestroy?.Invoke(this);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius, 7);

        foreach (Collider collider in colliders)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                Vector3 direction = (collider.transform.position - transform.position).normalized;
                rigidbody.AddForce(direction * _explosionForce, ForceMode.Impulse);
            }
        }
    }
}
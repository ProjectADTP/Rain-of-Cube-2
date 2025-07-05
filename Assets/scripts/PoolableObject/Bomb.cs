using System.Collections;
using UnityEngine;

public class Bomb : PoolableObject
{
    private float _explosionRadius;
    private float _explosionForce;

    protected override void Awake()
    {
        base.Awake();

        Renderer.material.SetFloat("_Mode", 3);
        ColorChanger.Initialize(Renderer.material);
    }

    public override void ResetState()
    {
        base.ResetState();

        Renderer.material.color = Color.black;
    }

    public void SetupBomb(float explosionRadius, float explosionForce)
    {
        _explosionRadius = explosionRadius;
        _explosionForce = explosionForce;

        ResetBomb();

        StartCoroutine(FadeAndExplode());
    }

    private void ResetBomb()
    {
        Renderer.material.color = Color.black;

        Rigidbody.velocity = Vector3.zero;
        Rigidbody.angularVelocity = Vector3.zero;

        transform.rotation = Quaternion.identity;
    }

    private IEnumerator FadeAndExplode()
    {
        ColorChanger.ChangeAlpha(0f, LifeTime);

        yield return new WaitForSeconds(LifeTime);

        Explode();

        OnDestroyAction?.Invoke(this);
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
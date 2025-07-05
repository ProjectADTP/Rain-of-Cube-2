using System;
using System.Collections;
using UnityEngine;

public class Cube : PoolableObject, IPoolable<Cube>
{
    private Action<Cube> _onDestroy;

    public void Initialize(Action<Cube> onDestroyAction)
    {
        _onDestroy = onDestroyAction;
    }

    public override void ResetState()
    {
        base.ResetState();

        Renderer.material.color = Color.white;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (HasCollided == false && collision.gameObject.TryGetComponent<Platform>(out _))
        {
            ChangeColor();

            StartCoroutine(ReturnToPoolAfterDelay(LifeTime));
        }
    }

    public void ChangeColor()
    {
        ColorChanger.ChangeToRandomColor();

        HasCollided = true;
    }

    private IEnumerator ReturnToPoolAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        _onDestroy?.Invoke(this);

        ResetState();
    }
}

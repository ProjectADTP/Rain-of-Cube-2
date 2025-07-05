using UnityEngine;

public class Cube : PoolableObject
{
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
        Renderer.material.color = ColorChanger.ChangeToRandomColor();
        HasCollided = true;
    }
}

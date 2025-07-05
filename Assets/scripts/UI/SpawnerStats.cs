using UnityEngine;
using TMPro;

public abstract class SpawnStatsUI<T> : MonoBehaviour where T : MonoBehaviour, IPoolable<T>
{
    [SerializeField] protected Spawner<T> Spawner;
    [SerializeField] protected TMP_Text SpawnedText;
    [SerializeField] protected TMP_Text CreatedText;
    [SerializeField] protected TMP_Text ActiveText;

    private void OnEnable()
    {
        if (Spawner != null)
        {
            Spawner.OnObjectStateChanged += ChangeText;
        }
    }

    private void OnDisable()
    {
        if (Spawner != null)
        {
            Spawner.OnObjectStateChanged -= ChangeText;
        }
    }

    protected virtual void ChangeText(T obj)
    {
        SpawnedText.text = Spawner.TotalSpawned.ToString();
        CreatedText.text = Spawner.TotalCreated.ToString();
        ActiveText.text = Spawner.ActiveCount.ToString();
    }
}
using UnityEngine;
using TMPro;

public class SpawnStatsUI : MonoBehaviour
{
    [Header("Cube Spawner")]
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private TMP_Text _cubeSpawnedText;
    [SerializeField] private TMP_Text _cubeCreatedText;
    [SerializeField] private TMP_Text _cubeActiveText;

    [Header("Bomb Spawner")]
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private TMP_Text _bombSpawnedText;
    [SerializeField] private TMP_Text _bombCreatedText;
    [SerializeField] private TMP_Text _bombActiveText;

    private void Update()
    {
        UpdateCubeStats();
        UpdateBombStats();
    }

    private void UpdateCubeStats()
    {
        if (_cubeSpawner == null) return;

        _cubeSpawnedText.text = $"{_cubeSpawner.TotalSpawned}";
        _cubeCreatedText.text = $"{_cubeSpawner.TotalCreated}";
        _cubeActiveText.text = $"{_cubeSpawner.ActiveCount}";
    }

    private void UpdateBombStats()
    {
        if (_bombSpawner == null) return;

        _bombSpawnedText.text = $"{_bombSpawner.TotalSpawned}";
        _bombCreatedText.text = $"{_bombSpawner.TotalCreated}";
        _bombActiveText.text = $"{_bombSpawner.ActiveCount}";
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [Header("Wave Info")]
    [SerializeField] private List<WaveData> enemies = new List<WaveData>(); // WaveData
    [SerializeField] private float spawnTimer;

    public int waveIndex = 0;
    public int enemyIndex = 0;

    private int enemiesSpawned = 0;

    public void StartWave()
    {
        StartCoroutine(WaveRoutine());
    }

    private IEnumerator WaveRoutine()
    {
        float t = 0;

        while (!IsWaveFinished())
        {
            t += Time.deltaTime;
            if (t >= enemies[waveIndex].enemiesData[enemyIndex].spawnDelay)
            {
                SpawnNext();
                t = 0;
            }
            yield return null;
        }
    }

    public void SpawnNext()
    {
        if (enemiesSpawned >= enemies[waveIndex].enemiesData[enemyIndex].quantity)
        {
            enemiesSpawned = 0;
            enemyIndex++;
            if (enemyIndex >= enemies[waveIndex].enemiesData.Count)
            {
                waveIndex++;
            }
        }
        if (!IsWaveFinished())
        {
            Instantiate(enemies[waveIndex].enemiesData[enemyIndex].enemy);
            enemiesSpawned++;
        }
    }

    public bool IsWaveFinished()
    {
        if (waveIndex >= enemies.Count)
            return true;
        return false;
    }
}

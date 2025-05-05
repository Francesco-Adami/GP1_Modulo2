using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [Header("Wave Info")]
    [SerializeField] private List<WaveData> waveData = new List<WaveData>();

    private int waveIndex = 0;
    private int enemyIndex = 0;
    private int enemiesSpawned = 0;
    private Transform spawnPoint;

    private void Start()
    {
        spawnPoint = WaveManager.instance.pathPoints[0];
    }

    public void StartWave()
    {
        StartCoroutine(WaveRoutine());
    }

    private IEnumerator WaveRoutine()
    {
        float t = 0;

        while (!IsWaveFinished())
        {
            if (!GameManager.instance.isGameActive) yield return null;
            else
            {
                t += Time.deltaTime;
                if (t >= waveData[waveIndex].enemiesData[enemyIndex].spawnDelay)
                {
                    SpawnNext();
                    t = 0;
                }
                yield return null;
            }
        }
    }

    public void SpawnNext()
    {
        if (enemiesSpawned >= waveData[waveIndex].enemiesData[enemyIndex].quantity)
        {
            enemiesSpawned = 0;
            enemyIndex++;
            if (enemyIndex >= waveData[waveIndex].enemiesData.Count)
            {
                waveIndex++;
            }
        }
        if (!IsWaveFinished())
        {
            Instantiate(waveData[waveIndex].enemiesData[enemyIndex].enemy, spawnPoint.position, Quaternion.identity);
            enemiesSpawned++;
        }
    }

    public bool IsWaveFinished()
    {
        if (waveIndex >= waveData.Count)
            return true;
        return false;
    }
}

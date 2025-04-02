using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }

    public List<StagesSO> stages;
    [Tooltip("from 0 to x, the delay between the spawn of each enemy of the stage")]
    [SerializeField] private float maxSpawnRateTimer;
    [SerializeField] private List<GameObject> SpawnPoints;

    public GameObject bossPrefab;

    private List<bool> waveHasFinished = new();

    private void Start()
    {
        foreach (var stage in stages)
        {
            waveHasFinished.Add(false);
        }
    }

    #region STAGE METHODS
    public void SpawnStage(int stageIndex)
    {
        StartCoroutine(StartSpawning(stageIndex));
    }
    #endregion

    #region COMPLEMENTARY METHODS
    private float GetRandomWaitingTime()
    {
        return Random.Range(0f, maxSpawnRateTimer);
    }

    private int GetRandomEnemy(StagesSO stage, List<int> enemiesToSpawn, List<int> enemiesSpawned)
    {
        List<int> availableEnemies = new();

        for (int i = 0; i < stage.enemies.Count; i++)
        {
            if (enemiesSpawned[i] < enemiesToSpawn[i])
            {
                availableEnemies.Add(i);
            }
        }

        if (availableEnemies.Count == 0)
        {
            return -1; // Tutti i nemici sono stati spawnati
        }

        return availableEnemies[Random.Range(0, availableEnemies.Count)];
    }
    #endregion

    #region ROUTINES
    private IEnumerator StartSpawning(int stageIndex)
    {
        waveHasFinished[stageIndex] = false;
        List<int> enemiesToSpawn = new();
        List<int> enemiesSpawned = new();

        foreach (var enemies in stages[stageIndex].enemies)
        {
            enemiesToSpawn.Add(enemies.quantity);
            //print("quantity: " + enemies.quantity);
            enemiesSpawned.Add(0);
        }

        while (!waveHasFinished[stageIndex])
        {
            int enemyType = GetRandomEnemy(stages[stageIndex], enemiesToSpawn, enemiesSpawned);
            if (enemyType != -1)
            {
                //print("Start SpawnEnemy");
                // Attende il completamento dello spawn prima di proseguire
                yield return StartCoroutine(SpawnEnemy(GetRandomWaitingTime(), enemyType, stageIndex));

                enemiesSpawned[enemyType]++;
            }

            // Ora controlla correttamente se tutti i nemici sono stati spawnati
            waveHasFinished[stageIndex] = true;

            for (int i = 0; i < enemiesToSpawn.Count; i++)
            {
                if (enemiesSpawned[i] < enemiesToSpawn[i])
                {
                    waveHasFinished[stageIndex] = false; // Se almeno un nemico deve ancora essere spawnato, non è finito
                    break;
                }
            }

            //print("hasFinished: " + hasFinished);
        }
    }


    private IEnumerator SpawnEnemy(float waitingTime, int randomEnemy, int stageIndex)
    {
        yield return new WaitForSeconds(waitingTime);
        // instantiate
        Vector2 pos = SpawnPoints[Random.Range(0, SpawnPoints.Count)].transform.position;
        Instantiate(stages[stageIndex].enemies[randomEnemy].enemy, pos, Quaternion.identity);
    }

    public bool IsStageFinished(int stageIndex)
    {
        return waveHasFinished[stageIndex];
    }
    #endregion
}

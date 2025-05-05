using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Enemies Settings")]
    public List<Transform> pathPoints = new();

    [Header("Wave Settings")]
    [SerializeField] private List<Wave> waves;
    [SerializeField] private float timeBetweenWaves = 5f;

    private int currentWaveIndex = 0;


    public static WaveManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(WaveSequence());
    }

    private IEnumerator WaveSequence()
    {
        while (currentWaveIndex < waves.Count)
        {
            // Controlla se il gioco è attivo
            if (!GameManager.instance.isGameActive)
            {
                yield return null;
            }

            // Avvia l'onda corrente
            Wave currentWave = waves[currentWaveIndex];
            currentWave.StartWave();

            // Attendi fino a quando l'onda ha finito di spawnare tutti i nemici
            while (!currentWave.IsWaveFinished())
            {
                yield return null;
            }

            // Attendi il tempo tra le wave
            yield return new WaitForSeconds(timeBetweenWaves);

            currentWaveIndex++;
        }

        StartCoroutine(CheckEnemies());
    }

    private IEnumerator CheckEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        while (enemies.Length > 0)
        {
            yield return null;
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }
        Debug.Log("Tutti i nemici sono stati sconfitti!");

        yield return new WaitForSeconds(1f);
        GameManager.instance.WinGame();
    }
}

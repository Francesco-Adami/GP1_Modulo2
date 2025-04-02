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
            // Avvia l'onda corrente
            Wave currentWave = waves[currentWaveIndex];
            currentWave.StartWave();

            // Attendi fino a quando l'onda ha finito di spawnare tutti i nemici
            while (!currentWave.IsWaveFinished())
            {
                yield return null;
            }

            // Attendi il tempo tra le onde (puoi anche inserire qui una logica per attendere che i nemici siano sconfitti)
            yield return new WaitForSeconds(timeBetweenWaves);

            currentWaveIndex++;
        }

        Debug.Log("Tutte le onde sono state completate!");
    }
}

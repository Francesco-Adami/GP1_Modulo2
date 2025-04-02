using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "")]
public class WaveData : ScriptableObject
{
    public List<EnemyWithQuantity> enemiesData;
}

[System.Serializable]
public class EnemyWithQuantity
{
    public Enemy enemy;
    public int quantity;
    public float spawnDelay;
}
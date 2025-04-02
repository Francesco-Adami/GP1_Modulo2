using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage", menuName = "ScriptableObject/Stage")]
public class StagesSO : ScriptableObject
{
    public List<EnemyWithQuantity> enemies;
}
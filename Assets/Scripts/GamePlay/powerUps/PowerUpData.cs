using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpData", menuName = "ScriptableObject/PowerUpData")]
public class PowerUpData : ScriptableObject
{
    [Tooltip("value of boost in percentage")]
    public float boostPercentage;
    public PowerUpType powerUpType;
}

public enum PowerUpType
{
    FireRate,
    Damage,
    SightRange,
}
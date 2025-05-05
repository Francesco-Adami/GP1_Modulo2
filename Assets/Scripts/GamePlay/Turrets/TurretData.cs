using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretData", menuName = "ScriptableObject/TurretData")]
public class TurretData : ScriptableObject
{
    public float damage;
    public float fireRate;
    public float sightRange;
    public Projectile projectile;
}

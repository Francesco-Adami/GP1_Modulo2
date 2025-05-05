using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        instance = this;
    }

    private List<Turret> turrets = new List<Turret>();

    public void AddTurret(Turret turret) { turrets.Add(turret); }
    public void RemoveTurret(Turret turret) { turrets.Remove(turret); }

    public void RemoveEnemyFromTurrets(Transform enemy)
    {
        foreach (Turret t in turrets)
        {
            if (t.enemies.Contains(enemy))
                t.RemoveEnemy(enemy);
        }
    }


}

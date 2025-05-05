using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] protected PowerUpData powerUpData;
    protected TowerPlacementZone zone;
    protected List<Turret> turretsBoosted = new List<Turret>();

    private void FixedUpdate()
    {
        if (zone == null) return;
        ApplyPowerupForeachTurret();
    }

    public void ApplyPowerupForeachTurret()
    {
        foreach (var item in zone.turrets)
        {
            bool alreadyBoosted = false;
            foreach (var boosted in turretsBoosted)
            {
                if (item == boosted)
                {
                    alreadyBoosted = true;
                    break;
                }
            }
            if (alreadyBoosted)
                continue;

            ApplyPowerUp(item);
        }
    }

    public void ApplyPowerUp(Turret turret)
    {
        switch (powerUpData.powerUpType)
        {
            case PowerUpType.FireRate:
                turret.AddFireRatePU(powerUpData.boostPercentage * turret.fireRate / 100);
                break;
            case PowerUpType.Damage:
                turret.AddDamagePU(powerUpData.boostPercentage * turret.damage / 100);
                break;
            case PowerUpType.SightRange:
                turret.AddSightRangePU(powerUpData.boostPercentage * turret.sightRange / 100);
                break;
        }
        turretsBoosted.Add(turret);
    }

    public void RemoveTurret(Turret turret)
    {
        foreach (var item in turretsBoosted)
        {
            if (item == turret)
            {
                turretsBoosted.Remove(item);
                break;
            }
        }
    }

    public void AssignZone(TowerPlacementZone zone) { this.zone = zone; }
}

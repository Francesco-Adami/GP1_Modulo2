using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Turret : MonoBehaviour
{
    [SerializeField] protected TurretData turretData;
    [SerializeField] private Transform firepoint;

    public List<Transform> enemies = new List<Transform>();

    protected float cooldownTimer;
    public bool isPlaced = false;
    protected SphereCollider sphereCollider;

    // Variables
    public float damage;
    public float fireRate;
    public float sightRange;

    private void Start()
    {
        enemies.Clear();
        EnemiesManager.instance.AddTurret(this);
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;

        damage = turretData.damage;
        fireRate = turretData.fireRate;
        sightRange = turretData.sightRange;
        sphereCollider.radius = sightRange;
    }

    private void Update()
    {
        if (!GameManager.instance.isGameActive) return;
        if (!isPlaced) return;

        Cooldown();

        if (enemies.Count > 0 && cooldownTimer <= 0)
        {
            Shoot();
        }

        LookAtEnemy();
    }

    private void LookAtEnemy()
    {
        if (enemies.Count > 0)
        {
            if (enemies.Count > 0)
            {
                Vector3 targetPos = enemies[0].transform.position;
                targetPos.y = transform.position.y;

                Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
    }

    protected void Shoot()
    {
        Projectile projectile = Instantiate(turretData.projectile, firepoint.position, Quaternion.identity);
        projectile.SetTarget(enemies[0]);
        projectile.SetDamage(damage);

        cooldownTimer = 1 / fireRate;
    }

    protected void Cooldown()
    {
        cooldownTimer -= Time.deltaTime;
    }

    #region POWER UPS
    public void AddDamagePU(float damage) { this.damage += damage; }
    public void RemoveDamagePU(float damage) { this.damage -= damage; }
    public void AddFireRatePU(float fireRate) { this.fireRate += fireRate; }
    public void RemoveFireRatePU(float fireRate) { this.fireRate -= fireRate; }
    public void AddSightRangePU(float sightRange) { this.sightRange += sightRange; sphereCollider.radius = this.sightRange; }
    public void RemoveSightRangePU(float sightRange) { this.sightRange -= sightRange; sphereCollider.radius = this.sightRange; }
    #endregion

    // COLLISIONS
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.transform);
        }
    }

    public void RemoveEnemy(Transform enemy)
    {
        enemies.Remove(enemy);
    }
}

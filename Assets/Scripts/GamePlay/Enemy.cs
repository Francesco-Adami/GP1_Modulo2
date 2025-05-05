using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp;
    public int damage;
    public float speed;
    public int moneyReward;

    private int currentTargetIndex = 0;
    private Transform target;

    private EnemyStatus enemyStatus;
    private float iceCooldown;
    private float speedBackUp;

    private void Start()
    {
        speedBackUp = speed;
        SetNewTarget();
    }

    private void Update()
    {
        if (!GameManager.instance.isGameActive) return;

        CheckStatus();
        MoveToTarget();

        if (Vector3.Distance(transform.position, target.position) < 0.02f)
        {
            SetNewTarget();
        }
    }

    #region ENEMY STATUS
    private void CheckStatus()
    {
        switch (enemyStatus)
        {
            case EnemyStatus.None:
                // al momento non serve fare nulla
                break;
            case EnemyStatus.Iced:
                iceCooldown -= Time.deltaTime;
                if (iceCooldown <= 0)
                {
                    enemyStatus = EnemyStatus.None;
                    speed = speedBackUp;
                }
                break;
        }
    }

    public void ApplyIce(float statusTimer)
    {
        speed = speedBackUp / 2;
        enemyStatus = EnemyStatus.Iced;
        iceCooldown = statusTimer;
    }
    #endregion

    #region MOVEMENT SYSTEM
    // MOVING SYSTEM
    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void SetNewTarget()
    {
        // non arriva ad essere out of range perché l'oggetto viene distrutto prima
        if (currentTargetIndex + 1 >= WaveManager.instance.pathPoints.Count) return; // non dovrebbe mai accadere

        currentTargetIndex++;
        target = WaveManager.instance.pathPoints[currentTargetIndex];
    }
    #endregion

    #region DAMAGE SYSTEM
    // DAMAGE SYSTEM
    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            EnemiesManager.instance.RemoveEnemyFromTurrets(transform);
            GameManager.instance.AddMoney(moneyReward);
            Destroy(gameObject);
        }
    }
    #endregion
}

public enum EnemyStatus
{
    None,
    Iced,
    // Fire
}

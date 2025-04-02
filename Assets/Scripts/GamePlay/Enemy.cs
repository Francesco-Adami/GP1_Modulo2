using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public int damage;
    public float speed;

    private int currentTargetIndex = 0;
    private Transform target;

    private void Start()
    {
        SetNewTarget();
    }

    private void Update()
    {
        MoveToTarget();

        if (Vector3.Distance(transform.position, target.position) < 0.02f)
        {
            SetNewTarget();
        }
    }

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

    // DAMAGE SYSTEM
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public int damage;

    private int currentTargetIndex = 0;
    private Transform target;

    private void Start()
    {
        currentTargetIndex++;
        target = WaveManager.instance.pathPoints[currentTargetIndex];
    }

    private void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}

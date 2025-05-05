using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed;

    protected float damage;
    protected Transform target;

    private void Update()
    {
        MoveToTarget();
    }

    // normal projectile
    public abstract void MoveToTarget();
    public void SetTarget(Transform target) { this.target = target; }
    public void SetDamage(float damage) { this.damage = damage; }
}

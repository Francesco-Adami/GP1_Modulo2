using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidProjectile : Projectile
{
    public override void MoveToTarget()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            // apply damage
            Enemy enemy = target.GetComponent<Enemy>();
            enemy.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}

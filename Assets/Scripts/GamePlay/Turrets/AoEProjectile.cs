using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEProjectile : Projectile
{
    [SerializeField] private float arcHeight = 5f;
    [SerializeField] private float explosionRadius = 3f;
    private Vector3 startPos;
    private Vector3 endPos;
    private float flightDuration;
    private float elapsedTime = 0f;
    private bool initialized = false;

    public override void MoveToTarget()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        if (!initialized)
        {
            startPos = transform.position;
            endPos = target.position;
            flightDuration = 1 / speed;
            elapsedTime = 0f;
            initialized = true;
        }

        // Aggiorna il tempo trascorso
        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / flightDuration);

        Vector3 horizontalPos = Vector3.Lerp(startPos, endPos, t);
        float verticalOffset = arcHeight * Mathf.Sin(Mathf.PI * t);
        horizontalPos.y += verticalOffset;

        transform.position = horizontalPos;

        if (t >= 1f)
        {
            Explode();
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Enemy"))
            {
                Enemy enemy = col.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

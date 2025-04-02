using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class GatePlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            Destroy(other.gameObject);
            GameManager.instance.TakeDamage(enemy.damage);
        }
    }
}

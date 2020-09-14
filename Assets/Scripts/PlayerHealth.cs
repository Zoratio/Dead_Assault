using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 100;

    public void TakeDamage(float damage)
    {
        health -= damage;
        print(health);
        if (health <= 0)
        {
            GetComponent<DeathHandle>().HandleDeath();
        }
    }
}

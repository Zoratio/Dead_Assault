using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerHealth target;
    float damage = 20f;


    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    public void AttackHitEvent()
    {
        if (target == null) return;
        if (GetComponent<EnemyAI>().hitableRange)
        {
            target.GetComponent<PlayerHealth>().TakeDamage(damage);
            Debug.Log("HIT");
        }
    }
}

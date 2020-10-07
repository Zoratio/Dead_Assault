using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerHealth target;
    [SerializeField] float damage = 10f;


    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    public void AttackHitEvent()    //*called from the attack animator animation - this is called twice per animation as he swings twice*
    {
        if (target == null) return; //error protection
        if (GetComponent<EnemyAI>().hitableRange)
        {
            target.GetComponent<PlayerHealth>().TakeDamage(damage);
            target.GetComponent<DisplayDamage>().ShowDamageCanvas();
        }
    }
}

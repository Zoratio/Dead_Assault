﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    float chaseRange = 15f;
    float turnSpeed = 5f;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;    //protection against the starting distance between the player and enemy being 0 before its calculated
    bool isProvoked = false;
    EnemyHealth health;

    public bool hitableRange;   //when the enemy attack animation happens (eg swings arm), is the player still within range - otherwise player will always take damage if theyve activated attack which is wrong

    void Start()
    {
        target = FindObjectOfType<PlayerHealth>().transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (health.IsDead())
        {
            navMeshAgent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            enabled = false;    //disable script
        }
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (isProvoked) //this could be turned into 'if spawned'
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)    //and this could possibly be turned into 'if close enough, spawn enemy'
        {
            isProvoked = true;
        }
        //else
        //{
        //    navMeshAgent.SetDestination(transform.position);    //to stop the enemy from following last known location of the player
        //}
    }

    public void OnDamageTaken() //*called from enemyhealth*
    {
        isProvoked = true;
    }

    private void EngageTarget()
    {
        FaceTarget();
        hitableRange = (distanceToTarget <= navMeshAgent.stoppingDistance);
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        else
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("Attack", false);
        GetComponent<Animator>().SetTrigger("Move");
        if (health.IsDead())
        {
            return;
        }
        else
        {
            navMeshAgent.SetDestination(target.position);
        }
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("Attack", true);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    //dont edit the y axis
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }


    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}

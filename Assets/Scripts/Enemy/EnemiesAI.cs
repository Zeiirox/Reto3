using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemiesAI : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform target;
    [SerializeField] private Transform[] wayPoints;

    [SerializeField] private float enemyRange;
    [SerializeField] private float startWaitTime;


    private NavMeshAgent agent;
    private Animator animator;
    private bool isInRange;
    private float waitTime;
    private int i = 0;

    private void Start()
    {
        waitTime = startWaitTime;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
        isInRange = Physics.CheckSphere(transform.position, enemyRange, layerMask);
        if (isInRange 
            && Vector3.Distance(transform.position, target.position) > 0.1f 
            && !target.GetComponent<PlayerController>().isDead)
        {
            agent.destination = target.position;
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= agent.stoppingDistance)
            {
                animator.SetBool("Attack", true);
                transform.LookAt(target);
            } else
            {
                animator.SetBool("Attack", false);
                animator.SetBool("Walk", true);
            }
        } 
        else
        {
            animator.SetBool("Attack", false);
            agent.destination = wayPoints[i].position;
            float distance = Vector3.Distance(transform.position, wayPoints[i].transform.position);
            if (distance <= agent.stoppingDistance)
            {
                if (waitTime <= 0)
                {
                    if (wayPoints[i] != wayPoints[wayPoints.Length - 1])
                    {
                        i++;
                    }
                    else
                    {
                        i = 0;
                    }
                    waitTime = startWaitTime;
                }
                else
                {
                    animator.SetBool("Walk", false);
                    waitTime -= Time.deltaTime;
                }
            } 
            else
            {
                animator.SetBool("Walk", true);
            }

        }
    }

    //public void awaitAttack()

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyRange);
    }

    
}

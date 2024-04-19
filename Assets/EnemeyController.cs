using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
public class EnemeyController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    private NavMeshAgent agent;
    public bool isRunning;

    public static EnemeyController instance;
    public float jumpForce;
    public bool isJumping;
    private bool isAttacking;

    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        agent=transform.GetComponent<NavMeshAgent>();

        StartCoroutine(MoveToNextPos());
        EnemyAttack.OnBallThrow += StopAction;
        EnemyAttack.OnBallThrowEnd += StartAction;
    }

    private void StartAction(object sender, EventArgs e)
    {
        isAttacking = false;
    }

    private void StopAction(object sender, EventArgs e)
    {
        isAttacking = true;
        agent.SetDestination(transform.position);
    }

    IEnumerator MoveToNextPos()
    {
        if (!isAttacking)
        {
            Vector3 newPos = NewPosition();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((newPos - transform.position).normalized), Time.deltaTime * 5f);
            isRunning = true;
            agent.SetDestination(newPos);



            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {

                yield return null;

            }
            if (isAttacking)
            {
                isRunning = false;
                agent.SetDestination(transform.position);



            }
            while (isAttacking)
            {
                yield return null;
            }


            isRunning = false;



            yield return new WaitForSeconds(UnityEngine.Random.Range(1, 3));



            StartCoroutine(MoveToNextPos());
        }


    }



    Vector3 NewPosition()
    {
       float x= UnityEngine.Random.Range(transform.position.x-3f,transform.position.x+3f);
       float z= UnityEngine.Random.Range(transform.position.z- 3f, transform.position.z+ 3f);

        return new Vector3(x, 0, z);
    }

    private void OnDisable()
    {
        EnemyAttack.OnBallThrow -= StopAction;
        EnemyAttack.OnBallThrowEnd -= StartAction;
    }

}

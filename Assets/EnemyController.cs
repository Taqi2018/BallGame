using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public enum EnemyState
{
     Idle,
     Patrolling,
     Attacking,
     Shooting
}


public class EnemyController : MonoBehaviour
{
     [Header("EnemyProperties")]
     public int maxHealth = 100;
     public int currentHealth;
     public int enemyDamage = 10;
     public float detectionRadius = 5f;
     public float rotationSpeed = 5f; 
     public Slider enemyHealthSlider;
     public Transform playerTransform;
     public bool isEnemyDead { get; private set; }
     public Animator enemyAnimator;

     [Header("EnemyMovementUsingNavMesh")]
     private NavMeshAgent navMeshAgent;
     public EnemyState currentState = EnemyState.Patrolling;

     [Header("ReferenceScripts")]
     public EnemyDetection enemyDetection;
     public PatrolSystem patrolSystem;


     [Header("AttackTimer")]
     private bool isAttacking = false;
     private float attackTimer = 0f;
     public float maxAttackTime = 1f;


     private void Start()
     {
          currentHealth = maxHealth;
          navMeshAgent = GetComponent<NavMeshAgent>();
          StartCoroutine(EnemyStateMachine());
  /*         playerTransform = enemyDetection.playerObject.transform;*/
     }

     private void Update()
     {
          enemyHealthSlider.value = currentHealth;
          if (enemyDetection.isPlayerDetected)
          {
               CheckIfPlayerInRange();
          }

        if (Vector3.Distance(playerTransform.position, this.transform.position) < UnityEngine.Random.Range(1.8f,3f) /*&&currentState==EnemyState.Attacking*/)
        {
            navMeshAgent.SetDestination(transform.position);
            currentState = EnemyState.Shooting;
     
        }

          

          

     }

     private IEnumerator EnemyStateMachine()
     {
          while (true)
          {
               switch (currentState)
               {
                    case EnemyState.Idle:
                         yield return new WaitForSeconds(Random.Range(3f, 5f));
                         currentState = EnemyState.Patrolling;
                         break;

                    case EnemyState.Patrolling:
                         float time = Patrol();
                         yield return new WaitForSeconds(time);
                         currentState = EnemyState.Idle;
                         enemyAnimator.SetBool("isIdle", true);
                         enemyAnimator.SetBool("isPatrolling", false);
                         break;

                    case EnemyState.Attacking:
                         enemyAnimator.SetBool("isIdle", false);
                         enemyAnimator.SetBool("isPatrolling", true);
                         yield return new WaitUntil(() => currentState==EnemyState.Attacking);
                        /* currentState = EnemyState.Patrolling;*/
                         break;
                case EnemyState.Shooting:
                    Debug.Log("s");
                    break;

            
            }
          }
     }

     

     private float Patrol()
     {
          
          if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
          {
               Transform givenPatrolPoint = patrolSystem.giveRandomPatrolPoint();
               enemyAnimator.SetBool("isPatrolling", true);
               enemyAnimator.SetBool("isIdle", false);
               float distance = Vector3.Distance(givenPatrolPoint.transform.position, transform.position);
               float MovingTime = distance / navMeshAgent.speed;

               navMeshAgent.SetDestination(givenPatrolPoint.position);
               return MovingTime;
          }
          else
          {
             return 0f;
          }
     }

     private void CheckIfPlayerInRange()
     {
          currentState = EnemyState.Attacking;
       

       /* Vector3 displacement = (transform.position - playerTransform.position)/2;
*/
          navMeshAgent.SetDestination(playerTransform.position);

       /*   Debug.Log("Chasing");*/
     }


     public void TakeDamage(int damageAmount)
     {
          currentHealth -= damageAmount;
          Debug.Log("Current Health Enemy From Bullet: " + currentHealth);

          if (currentHealth <= 0)
          {
               isEnemyDead = true;
               Destroy(gameObject); // Destroy the enemy when health reaches 0
          }
     }


}

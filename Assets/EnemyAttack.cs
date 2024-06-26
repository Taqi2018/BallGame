using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyAttack : MonoBehaviour
{

     public GameObject player;
     public float attackDistance;
     public GameObject ballPrefab;
     public Transform parentPoint;
     private Vector3 initial;
     private Rigidbody rb;
     private Vector3 targetPosition;
     private Vector3 direction;
     private float timeOfFlight;
     public float heightOfParabola;
     public float timeFactor;
     public bool isAttacking;
     private GameObject ball;
     private bool isDieing;
     public float throwDelay;
     private BallController ballController;

     public event EventHandler OnBallThrow;
     public event EventHandler OnBallThrowEnd;

     void Start()
     {
          initial = ballPrefab.transform.position;
          //ballController = GetComponentInChildren<BallController>();
          BallController.OnDieBall += PlayerDead;  
     }

     private void PlayerDead(object sender, BallController.OnEnemyDieEventArgs e)
     {
          if (e.collisionPer.gameObject == this.gameObject)
          {
               isDieing = true;

            StartCoroutine(DestroyEnemy());
          }
          
     }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);
    }

    private void ChangeDirectionOfPlayer(object sender, BallController.OnChangeDirEventArgs e)
     {
          transform.forward = new Vector3(e.dir.x, transform.forward.y, e.dir.z);
     }

     void Update()
     {
          if (!isDieing)
          {
               if (Vector3.Distance(player.transform.position, transform.position) < attackDistance && !isAttacking)
               {
                    isAttacking = true;
                    OnBallThrow?.Invoke(this, EventArgs.Empty);
                    ball = Instantiate(ballPrefab, parentPoint);
                    ball.GetComponent<Rigidbody>().isKinematic = true;
                    ball.GetComponent<SphereCollider>().isTrigger = true;
                    ball.transform.position = parentPoint.position;
                    ball.transform.SetParent(parentPoint);
                    targetPosition = player.transform.position; /*GetMouseWorldPosition();*/
                    direction = (targetPosition - ball.transform.position).normalized;
                    transform.forward = new Vector3(direction.x, transform.forward.y, direction.z);
                    StartCoroutine(ThrowDelay());
               }
               if (Vector3.Distance(player.transform.position, transform.position) > attackDistance && isAttacking)
               {
                    isAttacking = false;
                    OnBallThrowEnd?.Invoke(this, EventArgs.Empty);

               }
          }
     }

     IEnumerator ThrowDelay()
     {
          yield return new WaitForSeconds(throwDelay);

          ball.transform.parent = null;
          ball.GetComponent<Rigidbody>().isKinematic = false;

          float dis = Vector3.Distance(targetPosition, transform.position);

          timeOfFlight = Mathf.Sqrt(2 * heightOfParabola / 9.8f) * timeFactor;
          float vix = dis / timeOfFlight; //horizantol
          float viy = Mathf.Sqrt(2 * 9.8f * heightOfParabola);
          float vf = Mathf.Sqrt(Mathf.Pow((vix), 2) + Mathf.Pow((viy), 2));
          float theta = Mathf.Atan2(viy, vix) * Mathf.Rad2Deg;

          ball.GetComponent<Rigidbody>().velocity = vix * direction;
          ball.GetComponent<Rigidbody>().velocity += Vector3.up * viy;

          StartCoroutine(SetBallIsTriggerOff());
     }

     IEnumerator SetBallIsTriggerOff()
     {
          yield return new WaitForSeconds(0.2f);
          ball.GetComponent<Rigidbody>().transform.GetComponent<SphereCollider>().isTrigger = false;

        var b = ball;
        StartCoroutine(DestroyBall(b));
     }

    IEnumerator DestroyBall(GameObject b)
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(b);
    }

    private void OnDisable()
     {
          BallController.OnDieBall -= PlayerDead;
     }

}



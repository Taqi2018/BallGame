


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
     private Animator animator;
     public GameObject ParentObject;
     private EnemyAttack enemyAttack;
     private BallController ballController;

     // Start is called before the first frame update
     void Start()
     {
          animator = transform.GetComponent<Animator>();
          enemyAttack = ParentObject.GetComponent<EnemyAttack>();
          //ballController = ParentObject.GetComponentInChildren<BallController>();
          enemyAttack.OnBallThrow += PlayThrowAnimation;
          
          BallController.OnDieBall += PlayEndAnimation;
     }

     private void PlayEndAnimation(object sender, BallController.OnEnemyDieEventArgs e)
     {
          if (e.collisionPer.gameObject == ParentObject.gameObject)
          {
               animator.SetBool("isDieing", true);
          }
          
     }

     private void PlayThrowAnimation(object sender, EventArgs e)
     {
          animator.SetBool("isThrowing", true);
     }

     // Update is called once per frame
     void Update()
     {
          animator.SetBool("isRunning", ParentObject.GetComponent<EnemeyController>().isRunning);
     }
     public void StopThrowAnimation()
     {
          animator.SetBool("isThrowing", false);
          ParentObject.GetComponent<EnemyAttack>().isAttacking = false;
     }


     private void OnDisable()
     {
          enemyAttack.OnBallThrow -= PlayThrowAnimation;
          BallController.OnDieBall -= PlayEndAnimation;
     }

}

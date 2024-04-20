


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
     private Animator animator;
     public GameObject ParentObject;

     // Start is called before the first frame update
     void Start()
     {
          animator = transform.GetComponent<Animator>();
          EnemyAttack.OnBallThrow += PlayThrowAnimation;
          BallController.OnDieBall += PlayEndAnimation;
     }

     private void PlayEndAnimation(object sender, EventArgs e)
     {
          animator.SetBool("isDieing", true);
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
          ParentObject.GetComponent<enemyAttack>().isAttacking = false;

     }


     private void OnDisable()
     {
          enemyAttack.OnBallThrow -= PlayThrowAnimation;
          BallController.OnDieBall -= PlayEndAnimation;
     }

}

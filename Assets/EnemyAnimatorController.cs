


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
        BallController.OnDieBall+= PlayEndAnimation;
        /*   EnemyAttack. += PlayEndAnimation;*/
    }

    private void PlayEndAnimation(object sender, EventArgs e)
    {
        Debug.Log("isss");
        animator.SetBool("isDieing", true);
    }

    private void PlayThrowAnimation(object sender, EventArgs e)
    {
    
        animator.SetBool("isThrowing", true);
/*
        StartCoroutine(StopThrowAnimation());*/
    }




    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isRunning", ParentObject.GetComponent<EnemeyController>().isRunning);

        /*    Debug.Log(EnemeyController.instance.isRunning);*/
        /*     if (PlayerController.instance.isJumping)
             {*/
        /*animator.SetBool("isJumping", PlayerController.instance.isJumping);*/

        /*    }*/
    }
    public void StopThrowAnimation()
    {
/*        yield return new WaitForSeconds((3.5f / 2f));*/
        animator.SetBool("isThrowing", false);
        ParentObject.GetComponent<EnemyAttack>().isAttacking = false;

    }

    /*    public void StopJumpAnimation()
        {
            PlayerController.instance.isJumping = false;
            animator.SetBool("isJumping", PlayerController.instance.isJumping);
        }
    */
    private void OnDisable()
    {
       EnemyAttack.OnBallThrow -= PlayThrowAnimation;
;
        BallController.OnDieBall -= PlayEndAnimation;
        /*   EnemyAttack. += PlayEndAnimation;*/
    }

}

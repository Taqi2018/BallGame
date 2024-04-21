using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
     private Animator animator;
     private BallController ballControllerInstance;
     public GameObject ParentObject;

     // Start is called before the first frame update
     void Start()
     {
          animator = transform.GetComponent<Animator>();
          //ballControllerInstance = GetComponentInChildren<BallController>();
          BallController.OnBallThrow += PlayThrowAnimation;
          Ball.OnPlayerDie += PlayerDead;

     }

     private void PlayerDead(object sender, EventArgs e)
     {
          animator.SetBool("isDieing", true);
     }

     private void PlayThrowAnimation(object sender, EventArgs e)
     {
          animator.SetBool("isThrowing", true);
          StartCoroutine(StopThrowAnimation());
     }


     // Update is called once per frame
     void Update()
     {
          animator.SetBool("isRunning", PlayerController.instance.isRunning);
          animator.SetBool("isJumping", PlayerController.instance.isJumping);
     }
     IEnumerator StopThrowAnimation()
     {
          yield return new WaitForSeconds((3.5f / 2f));
          animator.SetBool("isThrowing", false);
     }

     public void StopJumpAnimation()
     {
          PlayerController.instance.isJumping = false;
          animator.SetBool("isJumping", PlayerController.instance.isJumping);
     }

     private void OnDisable()
     {
          BallController.OnBallThrow -= PlayThrowAnimation;
          Ball.OnPlayerDie -= PlayerDead;
     }

}

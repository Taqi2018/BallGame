using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    public bool isRunning;

    public static PlayerController instance;
    public float jumpForce;
    public  bool isJumping;
    private bool isDieing;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        instance = this;
        Ball.OnPlayerDie += PlayerDead;
    }

    private void PlayerDead(object sender, EventArgs e)
    {
        isDieing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDieing)
        {
            Vector3 dir = new Vector3(0, 0, 0);
            if (Input.GetKey(KeyCode.Z))
            {
                dir += new Vector3(0, 0, 1 * Time.deltaTime * speed);

            }

            if (Input.GetKey(KeyCode.S))
            {
                dir += new Vector3(0, 0, -1 * Time.deltaTime * speed);

            }
            if (Input.GetKey(KeyCode.Q))
            {
                dir += new Vector3(-1 * Time.deltaTime * speed, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                dir += new Vector3(1 * Time.deltaTime * speed, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                isJumping = true;

            };
            if (dir == Vector3.zero)
            {
                isRunning = false;
            }
            else
            {
                isRunning = true;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5f);
                transform.position += dir;
            }
        }
    }
}

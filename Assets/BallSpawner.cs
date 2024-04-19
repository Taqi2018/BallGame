using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{

    public GameObject ballPrefab;
    public Transform parentPoint;
    private Vector3 initial;

    // Start is called before the first frame update
    void Start()
    {
        initial = ballPrefab.transform.position;
        BallController.OnChangeDirection += ChangeDirectionOfPlayer;
    }

    private void ChangeDirectionOfPlayer(object sender, BallController.OnChangeDirEventArgs e)
    {
        transform.forward = new Vector3(e.dir.x, transform.forward.y, e.dir.z);
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) == true)
        {
           var ball= Instantiate(ballPrefab, parentPoint);
           ball.GetComponent<Rigidbody>().isKinematic=true;

            ball.transform.position = parentPoint.position;
            ball.transform.SetParent(parentPoint);
           
        }
    }


    private void OnDisable()
    {
        BallController.OnChangeDirection -= ChangeDirectionOfPlayer;
    }
}

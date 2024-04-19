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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) == true)
        {
            /*Instantiate(ballPrefab, parentPoint);*/
            ballPrefab.GetComponent<Rigidbody>().isKinematic=true;

            ballPrefab.transform.position = parentPoint.position;
           
        }
    }
}

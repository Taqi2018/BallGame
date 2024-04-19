



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyBallController : MonoBehaviour
{
    Rigidbody rb;
    Camera mainCamera;
    public float forceMultiplier;
    public float heightOfParabola;
    private float timeOfFlight;
    public float timeFactor;
    public bool alreadyThrown;
    private Vector3 targetPosition;
    private Vector3 direction;

    public static event EventHandler<OnChangeDirEventArgs> OnChangeDirection;
    public static event EventHandler OnBallThrow;


    public class OnChangeDirEventArgs : EventArgs
    {
        public Vector3 dir;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !alreadyThrown)
        {
            alreadyThrown = true;
            OnBallThrow?.Invoke(this, EventArgs.Empty);

            targetPosition = GetMouseWorldPosition();

            direction = (targetPosition - transform.position).normalized;

            OnChangeDirection?.Invoke(this, new OnChangeDirEventArgs { dir = direction });



            StartCoroutine(ThrowDelay());







        }
    }


    IEnumerator ThrowDelay()
    {
        yield return new WaitForSeconds(1f);
        transform.parent = null;
        rb.isKinematic = false;


        float dis = Vector3.Distance(targetPosition, transform.position);

        timeOfFlight = Mathf.Sqrt(2 * heightOfParabola / 9.8f) * timeFactor;
        float vix = dis / timeOfFlight; //horizantol

        float viy = Mathf.Sqrt(2 * 9.8f * heightOfParabola);

        float vf = Mathf.Sqrt(Mathf.Pow((vix), 2) + Mathf.Pow((viy), 2));

        float theta = Mathf.Atan2(viy, vix) * Mathf.Rad2Deg;

        GetComponent<Rigidbody>().velocity = vix * direction;
        GetComponent<Rigidbody>().velocity += Vector3.up * viy;

        StartCoroutine(SetBallIsTriggerOff());






    }


    IEnumerator SetBallIsTriggerOff()
    {
        yield return new WaitForSeconds(0.2f);
        this.transform.GetComponent<SphereCollider>().isTrigger = false;




    }





    Vector3 GetMouseWorldPosition()
    {
        Vector3 worldPos = Vector3.zero;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            worldPos = hit.point;
        }

        return worldPos;
    }


}

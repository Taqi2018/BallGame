using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream
using System;
=======
>>>>>>> Stashed changes

public class BallController : MonoBehaviour
{
    Rigidbody rb;
    Camera mainCamera;
    public float forceMultiplier;
<<<<<<< Updated upstream
    public float heightOfParabola;
    private float timeOfFlight;
    public float timeFactor;
    public bool alreadyThrown;
    private Vector3 targetPosition;
    private Vector3 direction;
    public float throwDelay;
    public static event EventHandler<OnChangeDirEventArgs>OnChangeDirection;

    public static event EventHandler OnDieBall;
    public static event EventHandler OnBallThrow;


    public  class OnChangeDirEventArgs : EventArgs
    {
        public Vector3 dir;
    }
=======
>>>>>>> Stashed changes

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        if (Input.GetMouseButtonDown(0)&&!alreadyThrown)
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
        yield return new WaitForSeconds(throwDelay);
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





=======
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 targetPosition = GetMouseWorldPosition();

            Vector3 direction = (targetPosition - transform.position).normalized;

            // Calculate initial velocity for a parabolic trajectory
            Vector3 velocity = direction * CalculateInitialVelocity(targetPosition);

            rb.isKinematic = false;
            rb.velocity = velocity;
        }
    }

>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.transform.GetComponent<PlayerCombatController>().currentHealthPlayer -= 20;
            
            if (collision.transform.GetComponent<PlayerCombatController>().currentHealthPlayer <= 0)
            {
                OnDieBall?.Invoke(this, EventArgs.Empty);

            }
        }
    }

=======
    float CalculateInitialVelocity(Vector3 targetPosition)
    {
        // Get the height difference between the ball and the target position
        float heightDifference = targetPosition.y - transform.position.y;

        // Calculate the horizontal distance
        Vector2 horizontalDirection = new Vector2(targetPosition.x - transform.position.x, targetPosition.z - transform.position.z);
        float horizontalDistance = horizontalDirection.magnitude;

        // Calculate the initial velocity for parabolic trajectory using projectile motion equations
        // You may need to tweak this formula based on your scene scale and requirements
        float gravity = Physics.gravity.magnitude;
        float initialVelocity = Mathf.Sqrt((horizontalDistance * gravity) / Mathf.Sin(2 * Mathf.PI / 4));

        return initialVelocity;
    }
>>>>>>> Stashed changes
}

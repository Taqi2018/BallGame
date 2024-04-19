using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Ball : MonoBehaviour
{

    public static EventHandler OnPlayerDie;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

            if (collision.transform.tag == "Player")
            {
                collision.transform.GetComponent<PlayerCombatController>().currentHealthPlayer -= 10;

            if (collision.transform.GetComponent<PlayerCombatController>().currentHealthPlayer <= 0)
            {
                OnPlayerDie?.Invoke(this, EventArgs.Empty);
            }

            }
      

    }
}

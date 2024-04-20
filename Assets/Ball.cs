using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Ball : MonoBehaviour
{

     public static EventHandler OnPlayerDie;

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

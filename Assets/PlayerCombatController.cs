using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatController : MonoBehaviour
{
     
     public int maxplayerHealth = 100;
     public int currentHealthPlayer;
     public int playerDamageAmount = 10;

     public Slider playerHealthSlider;

     private void Start()
     {
          currentHealthPlayer = 100;
          Debug.Log(currentHealthPlayer);
     }

     private void Update()
     {
          playerHealthSlider.value = currentHealthPlayer;


     }

}

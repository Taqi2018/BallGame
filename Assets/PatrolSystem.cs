using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSystem : MonoBehaviour
{
     public Transform[] patrolPoints;

     public Transform giveRandomPatrolPoint()
     {
          var randomPatrolPoint = Random.Range(0, patrolPoints.Length);
          Transform randomTransform = patrolPoints[randomPatrolPoint];
          return randomTransform;
     }
}

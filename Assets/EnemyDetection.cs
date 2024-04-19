using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
     public bool isPlayerDetected = false;

     public GameObject playerObject;

     [Header("Colliders")]
     private SphereCollider sphereCollider;
     private SpriteRenderer circleRenderer;

     public float detectionRadius = 4f;

     private void Start()
     {
          sphereCollider = GetComponent<SphereCollider>();
          circleRenderer = GetComponentInChildren<SpriteRenderer>();
     }

     private void Update()
     {
          UpdateSphereColliderRadius();
     }
     
     private void OnTriggerEnter(Collider other)
     {
          if (other.CompareTag("Player"))
          {
               isPlayerDetected = true;
               playerObject = other.gameObject;
               Debug.Log("Player In Area");
          }
          
     }

     private void OnTriggerExit(Collider other)
     {
          if (other.CompareTag("Player"))
          {
               isPlayerDetected = false;
               playerObject = null;
               Debug.Log("Player Out of Area");
          }
          
     }

     private void UpdateSphereColliderRadius()
     {
          sphereCollider.radius = detectionRadius;

          float visualScale = 0.4f * detectionRadius; 
          circleRenderer.transform.localScale = new Vector3(visualScale, visualScale, 1f);
     }

    
}

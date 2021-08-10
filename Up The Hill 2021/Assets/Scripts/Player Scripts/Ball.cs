using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Ball : MonoBehaviour {
    
    [SerializeField] private float pushForce = 20f;
    private Rigidbody rb;
    public bool hasFired;
    public bool hasReachedEndTable;

    private void Awake(){
        rb = GetComponent<Rigidbody>();
    }
    
    public void Shoot(Vector3 direction){
        if(transform.parent != null){
            transform.parent = null;
        }
        
        rb.isKinematic = false;
        rb.AddForce(transform.forward * pushForce,ForceMode.Impulse);
        hasFired = true;
    }
   
    public void MoveTheBallsToTheTray(){
        rb.isKinematic = true;
    }
    private void OnCollisionEnter(Collision coli){
        if(hasFired){
           if(coli.gameObject.CompareTag("Ball")){
               Ball ball = coli.gameObject.GetComponent<Ball>();
                if(!ball.hasFired){
                    
                    rb.AddForce((Vector3.up + Vector3.back).normalized * 10f, ForceMode.Impulse);
                    rb.useGravity = true;
                    Destroy(coli.gameObject,10f);
                }
           }
           else if(coli.gameObject.CompareTag("Boarder")){
               Debug.Log("collision Name " + coli.transform.name);
            
                rb.AddForce((Vector3.up + coli.GetContact(0).normal).normalized * 10f, ForceMode.Impulse);
                rb.useGravity = true;
                Destroy(coli.gameObject,5f);
           }
        }
    }

}

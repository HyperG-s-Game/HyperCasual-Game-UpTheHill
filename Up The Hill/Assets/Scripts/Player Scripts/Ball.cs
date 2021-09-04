using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Ball : MonoBehaviour {
    
    [SerializeField] private float shootSpeed = 6f;
    private Rigidbody rb;
    private bool hasFired;
    private bool hasReachedEndTable;
    private bool hascollidedWithObjects;

    private void Awake(){
        rb = GetComponent<Rigidbody>();
    }
    
    public void Shoot(Vector3 direction){
        if(transform.parent != null){
            transform.parent = null;
        }
        rb.isKinematic = false;
        
        rb.AddForce(direction * shootSpeed,ForceMode.Impulse);
        hasFired = true;
    }
   
    public void MoveTheBallsToTheTray(){
        
        rb.isKinematic = true;
    }
    private void OnCollisionEnter(Collision coli){
        if(hasFired){
        //    if(coli.gameObject.CompareTag("Ball")){
        //        Ball ball = coli.gameObject.GetComponent<Ball>();
        //         if(!ball.hasFired){
        //             rb.useGravity = true;
        //         }
        //     }
        //    if(coli.gameObject.CompareTag("Boarder")){
        //         rb.AddForce((Vector3.up + coli.GetContact(0).normal).normalized * 10f, ForceMode.Impulse);
        //         rb.useGravity = true;
        //     }
            if(coli != null){
                hascollidedWithObjects = true;
                rb.AddForce((Vector3.up + Vector3.back).normalized * 10f, ForceMode.Impulse);
                rb.useGravity = true;
                Destroy(gameObject,5f);
            }
        }
    }

    public void SetReachedTable(bool hasReached){
        hasReachedEndTable = hasReached;
    }
    public bool GetHasReachedTable{
        get{
            return hasReachedEndTable;
        }
    }
    public bool GetHasFired{
        get{
            return hasFired;
        }
    }
    public bool GetHascollided{
        get{
            return hascollidedWithObjects;
        }
    }
    
    

}

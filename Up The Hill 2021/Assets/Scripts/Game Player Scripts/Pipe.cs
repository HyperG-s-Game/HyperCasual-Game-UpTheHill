using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pipe : MonoBehaviour {
    
    
    public List<Ball> collectedBallsList = new List<Ball>();
    private void OnTriggerEnter(Collider coli){
        if(coli.gameObject.CompareTag("Ball")){
            Ball ball = coli.transform.GetComponent<Ball>();
            if(ball != null){
                
                Debug.Log(coli.transform.name + " Has sussefully enterd the pipe");
                ball.MoveTheBallsToTheTray();
                
                if(!collectedBallsList.Contains(ball)){
                    collectedBallsList.Add(ball);
                }
            }
            
        }
    }
    


}

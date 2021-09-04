using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pipe : MonoBehaviour {
    
    [SerializeField] private StartingTable table;
    public List<Ball> collectedBallsList = new List<Ball>();
    private void OnTriggerEnter(Collider coli){
        if(coli.gameObject.CompareTag("Ball")){
            Ball ball = coli.transform.GetComponent<Ball>();
            if(ball != null){
                if(!ball.GetHascollided){
                
                    ball.MoveTheBallsToTheTray();
                    
                    if(!collectedBallsList.Contains(ball)){
                        collectedBallsList.Add(ball);
                    }

                }
            }
            
        }
    }
    


}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndTable : MonoBehaviour {
    
    public Pipe pipe;
    public List<Transform> collectedBallTransforms = new List<Transform>();
    public Transform checkPoint;
    public int currentPosIndex;

    public float moveSpeed = 1f;
    public iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;
    
    private void Awake(){
        currentPosIndex = 0;
    }
    private void Update(){
        if(pipe.collectedBallsList.Count > 0){
            for (int i = 0; i < pipe.collectedBallsList.Count; i++){
                MoveTheBallToEndTablePoint(pipe.collectedBallsList[i]);
                pipe.collectedBallsList.Remove(pipe.collectedBallsList[i]);
            }
        }
    }
    public void MoveTheBallToEndTablePoint(Ball _ball){
        if(!_ball.hasReachedEndTable){
            StartCoroutine(MoveBalls(_ball.gameObject));
            
            _ball.hasReachedEndTable = true;
        }
    }
   
    public IEnumerator MoveBalls(GameObject _ball){
        
        while(_ball.transform.position != checkPoint.transform.position){
            iTween.MoveTo(_ball.gameObject,iTween.Hash (
                "x",checkPoint.position.x,
                "y",checkPoint.position.y,
                "z",checkPoint.position.z,
                "moveSpeed",moveSpeed,
                "easeType",easeType
            ));
            yield return null;
        }
        // yield return new WaitForSeconds(0.1f);
        iTween.MoveTo(_ball.gameObject,iTween.Hash (
            "x",collectedBallTransforms[currentPosIndex].position.x,
            "y",collectedBallTransforms[currentPosIndex].position.y,
            "z",collectedBallTransforms[currentPosIndex].position.z,
            "moveSpeed",moveSpeed,
            "easeType",easeType
        ));
        _ball.transform.SetParent(collectedBallTransforms[currentPosIndex]);
        Debug.Log("has Move to the end in the end table");
        
        currentPosIndex++;
    }
    


}

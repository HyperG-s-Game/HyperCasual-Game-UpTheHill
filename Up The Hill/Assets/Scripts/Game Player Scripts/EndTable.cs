using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndTable : MonoBehaviour {
    

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private List<Transform> collectedBallTransforms = new List<Transform>();
    [SerializeField] private Transform checkPoint;
    [SerializeField] private int currentPosIndex;
    [SerializeField] private Pipe pipe;
    [SerializeField] private iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;
    [SerializeField] private List<Ball> collectedBalls;
    [SerializeField] private bool ballMovingThroughPipe;

    
    private void Start(){
        collectedBalls = new List<Ball>();
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
        if(!_ball.GetHasReachedTable){
            StartCoroutine(MoveBalls(_ball.gameObject));
            _ball.SetReachedTable(true);
            
            collectedBalls.Add(_ball);
        }
    }
   
    public IEnumerator MoveBalls(GameObject _ball){
        ballMovingThroughPipe = true;
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
        
        Debug.Log(transform.name + " has Move to the end in the end table");
        
        currentPosIndex++;
        ballMovingThroughPipe = false;
    }
    
    public List<Ball> GetListOfCollectedBalls{
        get{
            return collectedBalls;
        }
    }
    public bool GetIsBallMovingThroughPipe{
        get{
            return ballMovingThroughPipe;
        }
    }

}

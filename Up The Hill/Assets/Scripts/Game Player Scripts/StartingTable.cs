using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WolfGamer.Utilty;
using Cinemachine;
public class StartingTable : MonoBehaviour {
    
    public RotatingTable rotator;
    public CinemachineVirtualCamera tableCam;
    public float timePerSecond;
    public int maxTime;
    private int currentTime;
    public Transform forwardShooterDirection;
    public float timeToMove;
    public iTween.EaseType easeType = iTween.EaseType.easeInExpo;
    public List<Ball> ballsList = new List<Ball>();
    public List<Transform> ballPositionPoint = new List<Transform>();
    public Transform startingPoint;
    private int currentPosIndex;
    
    private Ball currentBall;
    public bool currentActiveTable;
    public bool isTableCleared;
    public bool StartTimer;
    
    private void Start(){
        currentTime = maxTime;
        UIManager.instance.SetTimer(currentTime);
        CameraMovementManager.instance.GetCameras(tableCam);
    }

   
    public void InitTable(){
        for (int i = 0; i < ballsList.Count; i++){
            iTween.MoveTo(ballsList[i].gameObject,iTween.Hash(
                "x", ballPositionPoint[i].position.x,
                "y", ballPositionPoint[i].position.y,
                "z", ballPositionPoint[i].position.z,
                "time",timeToMove,
                "easyType",easeType
            ));
        }
    }
    public void TableRotate(bool _state){
        Debug.Log("Start Rotation");
        rotator.startRotation = _state;

    }
    private void Update(){
        if(ballsList.Count == 0){
            if(!isTableCleared){
                isTableCleared = true;

            }
        }
        if(StartTimer){
            if(currentActiveTable && !isTableCleared){
                if(currentTime > 0){
                    timePerSecond -= Time.deltaTime;
                    if(timePerSecond <= 0f){
                        currentTime--;
                        UIManager.instance.SetTimer(currentTime);
                        
                        timePerSecond = 1;
                    }
                }else{
                    GameManager.instance.isLoss = true;
                }
            }

        }
        
    }
    public void ShotBall(Vector3 direction){
        currentBall.Shoot(direction);
    }
    
    public void ResetTimer(){
        timePerSecond = 1f;
        currentTime += maxTime;
    }
    
    public void RemoveBallFromList(){
        ballsList.RemoveAt(0);
    }
    public void MoveBallToShootingPoint(){
        
        if(ballsList.Count > 0){
            currentBall = ballsList[0];
            iTween.MoveTo(currentBall.gameObject,iTween.Hash(
                "x", startingPoint.position.x,
                "y", startingPoint.position.y,
                "z", startingPoint.position.z,
                "time",timeToMove,
                "easyType",easeType
            ));
            currentBall.transform.rotation = forwardShooterDirection.transform.rotation;
            currentBall.transform.SetParent(forwardShooterDirection.transform);
             

        }
    }
    
    
    public bool GetCompletedTheCurrentTable{
        get{
            return isTableCleared;
        }
    }
    public int GetCurrentTime{
        get{
            return currentTime;
        }
    }
    


}

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
    public float timerToMoveToShootingPos = 0.1f;
    public iTween.EaseType easyTypeForLaterBalls = iTween.EaseType.easeInOutExpo;
    public List<Ball> ballsList = new List<Ball>();
    public List<Transform> ballPositionPoint = new List<Transform>();
    public EndTable endTable;
    public Transform startingPoint;
    private int currentPosIndex;
    
    private Ball currentBall;
    public bool currentActiveTable;
    private bool isTableCleared;
    private bool StartTimer;
    private GameManager gameManager;
    private bool readyToShoot;
    private bool hasReachedToShootingPos;
    private void Start(){
        currentTime = maxTime;
        UIManager.instance.SetTimer(currentTime);
        CameraMovementManager.instance.GetCameras(tableCam);
        gameManager = GameManager.instance;
        
    }

    public void InitTable(){
       StartCoroutine(InitTableRoutine());
    }
    private IEnumerator InitTableRoutine(){
        yield return new WaitForSeconds(0.2f);
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
    private IEnumerator CheckForTableClear(){
        yield return new WaitForSeconds(1f);
        if(ballsList.Count <= 0){
            yield return new WaitForSeconds(0.1f);
            if(!isTableCleared){
                isTableCleared = true;
            }
        }
        
    }
    
    
    private void Update(){
        
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
                    gameManager.isLoss = true;
                }
            }

        }
        
    }
    
    public void ShotBall(Vector3 direction){
        currentBall.Shoot(direction);
        StartCoroutine(CheckForTableClear());
    }
    
    public void ResetTimer(){
        timePerSecond = 1f;
        currentTime += maxTime;
    }
    
    public void RemoveBallFromList(){
        hasReachedToShootingPos = false;
        ballsList.RemoveAt(0);
        
    }
    
    public void MoveBallToShootingPoint(){
        if(!hasReachedToShootingPos){
            StartCoroutine(MoveBallToShootingPointRoutine());
            hasReachedToShootingPos = true;
        }
    }
    private IEnumerator MoveBallToShootingPointRoutine(){
        yield return new WaitForSeconds(0.3f);
        if(ballsList.Count > 0){
            readyToShoot = false;
            currentBall = ballsList[0];
            iTween.MoveTo(currentBall.gameObject,iTween.Hash(
                "x", startingPoint.position.x,
                "y", startingPoint.position.y,
                "z", startingPoint.position.z,
                "time",timerToMoveToShootingPos,
                "easyType",easyTypeForLaterBalls
            ));
            currentBall.transform.rotation = forwardShooterDirection.transform.rotation;
            currentBall.transform.SetParent(forwardShooterDirection.transform);
            yield return new WaitForSeconds(0.1f);
            readyToShoot = true; 

        }
    }
    public void TurnOnTimer(bool start){
        StartTimer = start;
    }
    
    public bool GetCompletedTheCurrentTable{
        get{
            return isTableCleared;
        }
    }
    public bool isGameLost{
        get{
            if(!endTable.GetIsBallMovingThroughPipe){
                if(isTableCleared && endTable.GetListOfCollectedBalls.Count == 0){
                    return true;
                }

            }
            return false;
        }
    }
    public int GetCurrentTime{
        get{
            return currentTime;
        }
    }
    public bool GetReadyToShoot{
        get{
            return readyToShoot;
        }
    }


}

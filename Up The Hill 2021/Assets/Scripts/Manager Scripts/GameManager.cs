using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using WolfGamer.Utilty;
public class GameManager : MonoBehaviour {
    
    public int currentTime;
    public Shooter shooter;

    // Events.
    public UnityEvent StartLevelEvents;
    public UnityEvent gamePlayEvents;
    public UnityEvent winningEvents;
    public UnityEvent lossEvents;


    // current Indexes
    private int currentTableIndex;
    public static GameManager instance{get;private set;}
    public bool m_levelStarted,m_isGameOver,m_moveToNextTable;
    public bool isLoss;
    private List<StartingTable> clearedTablesList = new List<StartingTable>();
    public LevelManager levelManager;
    private void Awake(){
        instance = this;
        clearedTablesList = new List<StartingTable>();
    }
    private void Start(){

        currentTableIndex = 0;
        StartCoroutine(StartLevelRoutine());
    }


    private IEnumerator StartLevelRoutine(){
        levelManager.CreateLevel();
        levelManager.SetCurrentTable(currentTableIndex);
        StartLevelEvents?.Invoke();
        while(!m_levelStarted){
            Debug.Log("Tap Play Button to Start the Level");
            // Shows the Starting screen.
            // Tap the Play Button to Start the Game.
            yield return null;
        }
        shooter.table = levelManager.GetCurrentTable;
        
        
        yield return StartCoroutine(PlayLevelRoutine());
    }
    
    private IEnumerator PlayLevelRoutine(){
        
        gamePlayEvents?.Invoke();
        UIManager.instance.SetTableNumber(currentTableIndex);
        // Move the First Ball to the shooting Point.
        levelManager.GetCurrentTable.MoveBallToShootingPoint();
        yield return new WaitForSeconds(0.5f);
        shooter.canShoot = true;
        levelManager.GetCurrentTable.StartTableRotation(true);
        levelManager.GetCurrentTable.StartTimer = true;
        while(!m_isGameOver) {
            while(!shooter.hasShootTheBall){
                
                Debug.Log("Ready To shoot the Ball");
                // move to the next Table.
                m_isGameOver = hasWon() || isLoss;
                if(m_isGameOver){
                    if(hasWon()){
                        Debug.Log("has Won");
                        winningEvents?.Invoke();
                    }else{
                        Debug.Log("you Loss");
                        lossEvents?.Invoke();
                    }
                    yield break;
                    
                }
                // Wait for the Player to shoot the Ball.

                yield return null;

            }
            if(levelManager.GetCurrentTable.isTableCleared){

               yield return StartCoroutine(moveToNextTable());
            }
            shooter.hasShootTheBall = false;
            // WaitFor Seconds.
            yield return new WaitForSeconds(0.1f);
            Debug.Log("Move the ball to the shooting Pos");
            // Move the Next Ball to the shooting Point.
            levelManager.GetCurrentTable.MoveBallToShootingPoint();
            


            // Loop Go on Until all the ball are in the end Table.
            yield return null;
        }
        // Check for Table Clear or Loss.
            // if Player wins then advance to next Table (if Has).
            // if player loss then show game over window and ask for restart.
        

    }
    private IEnumerator moveToNextTable(){
         
        levelManager.MoveOut(levelManager.GetCurrentTable);
        shooter.canShoot = false;
        levelManager.GetCurrentTable.currentActiveTable = false;
        
        levelManager.GetCurrentTable.StartTableRotation(false);
        if(!clearedTablesList.Contains(levelManager.GetCurrentTable)){
            clearedTablesList.Add(levelManager.GetCurrentTable);
        }

        currentTableIndex++;
        yield return new WaitForSeconds(2f);
        UIManager.instance.SetTableNumber(currentTableIndex);
        levelManager.SetCurrentTable(currentTableIndex);
        levelManager.GetCurrentTable.ResetTimer();
        
        yield return new WaitForSeconds(0.1f);
        levelManager.GetCurrentTable.StartTableRotation(true);
        levelManager.GetCurrentTable.StartTimer = true;
        shooter.table = levelManager.GetCurrentTable;
        shooter.canShoot = true;
    }
    
    
    private bool hasWon(){
        if(levelManager.GetTableList.Count == clearedTablesList.Count){
            foreach(StartingTable tables in clearedTablesList){
                return tables.isTableCleared;
            }
            
        }
        return false;
        
    }
    
    
    
    public void StartLevel(){
        m_levelStarted = true;
    }
    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    


}

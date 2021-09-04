using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using WolfGamer.Utilty;
public class GameManager : MonoBehaviour {
    


    public Shooter shooter;
    private int currentTime;

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
    public List<StartingTable> clearedTablesList = new List<StartingTable>();
    public LevelManager levelManager;
    private int totalTableCount;
    private void Awake(){
        #if UNITY_EDITOR
            Debug.unityLogger.logEnabled = true;
        #else 
            Debug.unityLogger.logEnabled = false;
        #endif
        if(instance == null){
            instance = this;
        }
    }
    private void Start(){
        clearedTablesList = new List<StartingTable>();
        
        currentTableIndex = 0;
        StartCoroutine(nameof(StartLevelRoutine));
    }


    private IEnumerator StartLevelRoutine(){

        levelManager.CreateLevel();
        levelManager.SetCurrentTable(currentTableIndex);
        StartLevelEvents?.Invoke();
        totalTableCount = levelManager.GetTableList.Count;
        while(!m_levelStarted){
            Debug.Log("Tap Play Button to Start the Level");
            // Shows the Starting screen.
            // Tap the Play Button to Start the Game.
            yield return null;
        }
        shooter.SetTable(levelManager.GetCurrentTable);
        
        
        yield return StartCoroutine(PlayLevelRoutine());
    }
    
    private IEnumerator PlayLevelRoutine(){
        
        gamePlayEvents?.Invoke();
        UIManager.instance.SetTableNumber(currentTableIndex);
        // Move the First Ball to the shooting Point.
        levelManager.GetCurrentTable.MoveBallToShootingPoint();
        yield return new WaitForSeconds(0.5f);
        shooter.SetCanShoot(true);
        levelManager.GetCurrentTable.TableRotate(true);
        levelManager.GetCurrentTable.TurnOnTimer(true);
        while(!m_isGameOver) {
            
            Debug.Log("Ready To shoot the Ball");
            // move to the next Table.
            m_isGameOver = hasWon() || hasLost();
            if(m_isGameOver){
                break;
            }
            while(!shooter.GetCanShoot){
                
                // Wait for the Player to shoot the Ball.

                yield return null;

            }
            if(levelManager.GetCurrentTable.GetCompletedTheCurrentTable){
               yield return StartCoroutine(moveToNextTable());
            }
            shooter.SetHasShotTheBall(false);
            // WaitFor Seconds.
            Debug.Log("Move the ball to the shooting Pos");
            // Move the Next Ball to the shooting Point.
            levelManager.GetCurrentTable.MoveBallToShootingPoint();
            // Loop Go on Until all the ball are in the end Table.
            yield return null;
        }
        if(hasWon()){
            Debug.Log("has Won");
            levelManager.SetLevelCompleted(true);
            winningEvents?.Invoke();
        }if(hasLost()){
            Debug.Log("you Loss");
            lossEvents?.Invoke();
        }
        levelManager.SetLevelCompleted(hasWon());
        
        // Check for Table Clear or Loss.
            // if Player wins then advance to next Table (if Has).
            // if player loss then show game over window and ask for restart.
        

    }
    private IEnumerator moveToNextTable(){
         
        levelManager.MoveOut(levelManager.GetCurrentTable);

        shooter.SetHasShotTheBall(false);
        levelManager.GetCurrentTable.currentActiveTable = false;
        yield return new WaitForSeconds(0.5f);
        
        if(!clearedTablesList.Contains(levelManager.GetCurrentTable)){
            clearedTablesList.Add(levelManager.GetCurrentTable);
        }

        currentTableIndex++;
        yield return new WaitForSeconds(1f);
        UIManager.instance.SetTableNumber(currentTableIndex);
        levelManager.SetCurrentTable(currentTableIndex);
        levelManager.GetCurrentTable.ResetTimer();
        
        yield return new WaitForSeconds(0.1f);
        levelManager.GetCurrentTable.TableRotate(true);
        levelManager.GetCurrentTable.TurnOnTimer(true);
        shooter.SetTable(levelManager.GetCurrentTable);
        shooter.SetCanShoot(true);
    }
    
    
    private bool hasWon(){
        if(!levelManager.GetCurrentTable.isGameLost){
            if(clearedTablesList.Count == totalTableCount){
                for (int i = 0; i < clearedTablesList.Count; i++){
                    if(clearedTablesList[i].GetCompletedTheCurrentTable){
                        return true;
                    }
                }
            }

        }
        return false;
    }
    private bool hasLost(){
        if(levelManager.GetCurrentTable.isGameLost || isLoss){
            return true;
        }
        return false;
    }
    
    
    
    public void StartLevel(){
        m_levelStarted = true;
    }
    public void RestartLevel(){
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
    public void MoveToMenu(){
        LevelLoader.instance.SwitchScene(SceneIndex.Main_Menu);
    }
    public void MoveToNextLevel(){
        LevelLoader.instance.MoveToNextLevel();
    }


}

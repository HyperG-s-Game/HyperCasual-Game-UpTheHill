using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
    
    
    public Transform[] spawnPoints; 
    public float moveSpeed;
    public iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;
    public LevelSO currentLevelData;
    private StartingTable currentTable;
    
    private List<StartingTable> tablesList;
    
    
    public void CreateLevel(){
        tablesList = new List<StartingTable>();
        for (int i = 0; i < currentLevelData.tablePrefab.Count; i++){
            StartingTable table = Instantiate(currentLevelData.tablePrefab[i],spawnPoints[i].position,Quaternion.identity);
            table.gameObject.SetActive(false);
            tablesList.Add(table);
        }
    }
    public void SetCurrentTable(int _intdex){
        for (int i = 0; i < tablesList.Count; i++){
            if(i == _intdex){
                CameraMovementManager.instance.MoveCamera(_intdex);
                currentTable = tablesList[i];
                currentTable.gameObject.SetActive(true);
                currentTable.currentActiveTable = true;
                currentTable.InitTable();
                
            }else{
                tablesList[i].currentActiveTable = false;
                
            }
        }
        
    }
    public void MoveOut(StartingTable _table){
        StartCoroutine(MoveOutOfTheViewPoint(_table));
    }
    public IEnumerator  MoveOutOfTheViewPoint(StartingTable _obj){
        _obj.StartTableRotation(false);
        yield return new WaitForSeconds(10f);
        _obj.gameObject.SetActive(false);
        
        
    }
    
    public StartingTable GetCurrentTable{
        get{
            return currentTable;
        }
    }
    public List<StartingTable> GetTableList{
        get{
            return tablesList;
        }
    }
}

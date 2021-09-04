using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;




public class SavingAndLoadingManager : MonoBehaviour{
    public static SavingAndLoadingManager instance {get;private set;}
    
    public SaveData saveData;
    
    private void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(instance);
        }
        DontDestroyOnLoad(instance.gameObject);
        
        LoadGame();
        

    }
    [ContextMenu("SAVE GAME")]
    public void SaveGame(){
        
        for (int i = 0; i < saveData.levelData.Length; i++){
            saveData.levelData[i].Save();
        }
        
    }
    [ContextMenu("LOAD GAME")]
    public void LoadGame(){
        for (int i = 0; i < saveData.levelData.Length; i++){
            saveData.levelData[i].Load();
        }
    }      
    private void OnApplicationQuit(){
        SaveGame();
    }  
}
[System.Serializable]
public struct SaveData{
    // public SettingsSO settingsData;
    public LevelSO[] levelData;
    // public CoinDataSO coins;
}


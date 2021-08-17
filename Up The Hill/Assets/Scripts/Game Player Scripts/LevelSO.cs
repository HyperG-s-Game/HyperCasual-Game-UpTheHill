using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
[CreateAssetMenu(menuName = "LevelSO",fileName = "ScriptableObject/New Level")]
public class LevelSO : ScriptableObject {
    public SceneIndex currentScene;

    public string levelName;
    public Color levelColor; // it will be replaced with the privew of the level.
    private List<StartingTable> tablePrefabList;
    public bool hasUnloacked;
    public bool isCompleted;
    public void CreateNewTableList(){
        tablePrefabList = new List<StartingTable>();
    }
    public void AddTable(StartingTable tables){

        if(!tablePrefabList.Contains(tables)){
            tablePrefabList.Add(tables);
        }
    }
    [ContextMenu("Save")]
    public void Save(){
        string data = JsonUtility.ToJson(this,true);
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath,"/","Level Data",currentScene));
        formatter.Serialize(file,data);
        file.Close();
    }

    [ContextMenu("Load")]
    public void Load(){
        if(File.Exists((string.Concat(Application.persistentDataPath,"/","Level Data",currentScene)))){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream Stream = File.Open(string.Concat(Application.persistentDataPath,"/","Level Data",currentScene),FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(Stream).ToString(),this);
            Stream.Close();
        }
    }
    public List<StartingTable> GetTableList(){
        return tablePrefabList;
    }
    
    

}

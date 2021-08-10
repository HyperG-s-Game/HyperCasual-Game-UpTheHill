using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "LevelSO",fileName = "ScriptableObject/New Level")]
public class LevelSO : ScriptableObject {


    public string levelName;
    public Color levelColor; // it will be replaced with the privew of the level.
    public List<StartingTable> tablePrefab;
    public bool hasUnloacked;



}

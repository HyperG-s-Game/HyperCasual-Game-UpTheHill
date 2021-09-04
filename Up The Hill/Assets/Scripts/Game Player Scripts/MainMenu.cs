using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {
    
    
    
    public void PlayGame(){
        LevelLoader.instance.PlayLevel();
    }
    


}

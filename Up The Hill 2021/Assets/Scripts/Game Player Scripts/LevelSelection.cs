using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class LevelSelection : MonoBehaviour {
    
    public LevelSO[] levels;
    public TextMeshProUGUI levelName;
    public Button selectButton;
    public Image priviewImage;
    
    public int currentLevelNumber;

    private void Start(){
        ActivateLevelView(currentLevelNumber);
    }

    public void SeeLeft(){
        currentLevelNumber--;
        if(currentLevelNumber < 0){
            currentLevelNumber = levels.Length - 1;
        }
    }
    private void Update(){

        ActivateLevelView(currentLevelNumber);
    }
    private void ActivateLevelView(int _index){
        for (int i = 0; i < levels.Length; i++){
            if(i == currentLevelNumber){
                levelName.SetText(levels[i].levelName);
                priviewImage.color = levels[i].levelColor;
                if(levels[i].hasUnloacked){
                    selectButton.interactable = true;
                }else{
                    selectButton.interactable = false;
                }
            }
        }
    }
    public void SeeRight(){
        currentLevelNumber++;
        if(currentLevelNumber > levels.Length - 1){
            currentLevelNumber = -1;
        }
        
        
    }

    

}

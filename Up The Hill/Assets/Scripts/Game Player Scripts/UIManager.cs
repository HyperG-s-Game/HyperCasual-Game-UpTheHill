using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class UIManager : MonoBehaviour {


    public TextMeshProUGUI tMProTextTableNumber,timerText;
    public static UIManager instance{get;private set;}

    private void Awake(){
        instance = this;
    }
    

    public void SetTableNumber(int _textNumber){
        tMProTextTableNumber.SetText("Table Number:  " + (_textNumber + 1).ToString());
    }
    public void SetTimer(int _time){
        timerText.SetText("Time Left is " + _time.ToString("0"));
    }
    


}

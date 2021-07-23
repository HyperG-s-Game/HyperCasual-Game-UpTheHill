using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
public class Shooter : MonoBehaviour {



    public StartingTable table{get;set;}
    public bool canShoot;
    public bool hasShootTheBall;
    
    private void Update(){
        if(!EventSystem.current.IsPointerOverGameObject() && canShoot){
            if(Input.GetMouseButtonDown(0) && !hasShootTheBall){
                table.ShotBall(transform.forward);
                table.RemoveBallFromList();
                hasShootTheBall = true;
            }

        }
    }


}

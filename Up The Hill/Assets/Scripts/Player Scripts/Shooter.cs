using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
public class Shooter : MonoBehaviour {
    private StartingTable table;
    private bool canShoot;
    private bool hasShotTheBall;
    
    private void Update(){

        #if UNITY_EDITOR
        if(!EventSystem.current.IsPointerOverGameObject() && canShoot){
            if(Input.GetMouseButtonDown(0) && !hasShotTheBall && table.GetReadyToShoot){
                Debug.Log("is On Editor");
                table.ShotBall(transform.forward);

                table.RemoveBallFromList();
                
                hasShotTheBall = true;
            }
        }
        #elif UNITY_ANDROID
            if(TouchingWithFinger(out int fingerId)){
                if(!EventSystem.current.IsPointerOverGameObject(fingerId) && canShoot){
                    if(!hasShotTheBall && table.GetReadyToShoot){
                        Debug.Log("Is On Android");
                        table.ShotBall(transform.forward);
                        
                        table.RemoveBallFromList();
                        hasShotTheBall = true;

                    }

                }
            }
        #endif        
    }
    private IEnumerator RemoveBallsFromListRoutine(){
        yield return new WaitForSeconds(0.1f);
        table.RemoveBallFromList();
    }
    public bool TouchingWithFinger(out int _fingerId){
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began){
            _fingerId = Input.touches[0].fingerId;
            return true;
        }
        _fingerId = -1;
        return false;
    
    }
    public void SetTable(StartingTable table){
        this.table = table;
    }
    public void SetCanShoot(bool _shoot){
        canShoot = _shoot;
    }
    public bool GetCanShoot{
        get{
            return canShoot;
        }
    }
    public void SetHasShotTheBall(bool hasShot){
        hasShotTheBall = hasShot;
    }
    public bool GethasShot{
        get{
            return hasShotTheBall;
        }
    }

}

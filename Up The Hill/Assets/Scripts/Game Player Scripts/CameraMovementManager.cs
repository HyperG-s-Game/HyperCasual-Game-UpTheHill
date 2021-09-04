using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
public class CameraMovementManager : MonoBehaviour {
    
    
    public static CameraMovementManager instance {get;private set;}
    private List<CinemachineVirtualCamera> virtualCameras;
    private void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(instance.gameObject);
        }
    }
    private void Start(){
        virtualCameras = new List<CinemachineVirtualCamera>();

    }
    public void GetCameras(CinemachineVirtualCamera cam){
        virtualCameras.Add(cam);
    }
    public void MoveCamera(int _cameraIndex){
        for (int i = 0; i < virtualCameras.Count; i++){
            if(i == _cameraIndex){
                virtualCameras[i].Priority = 20;
            }else{
                virtualCameras[i].Priority = 10;
            }
        }
    }

}

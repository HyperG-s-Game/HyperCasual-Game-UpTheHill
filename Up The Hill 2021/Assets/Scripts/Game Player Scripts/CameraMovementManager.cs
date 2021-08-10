using UnityEngine;
using Cinemachine;

public class CameraMovementManager : MonoBehaviour {
    
    
    public CinemachineVirtualCamera[] virtualCameras;
    public static CameraMovementManager instance {get;private set;}
    private void Awake(){
        instance = this;
    }
    public void MoveCamera(int _cameraIndex){
        for (int i = 0; i < virtualCameras.Length; i++){
            if(i == _cameraIndex){
                virtualCameras[i].Priority = 20;
            }else{
                virtualCameras[i].Priority = 10;
            }
        }
    }

}

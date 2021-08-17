using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum RotationDirection{
    ClockWise,Anti_clockWise,
}
public class RotatingTable : MonoBehaviour {
    
    [SerializeField] private float rotatingSpeed = 20f;
    [SerializeField] private RotationDirection direction;
    [SerializeField] private float rotationIncreasedTime = 20f;

    public bool startRotation;
    
    private void Update(){
        if(startRotation){
            rotatingSpeed += Time.deltaTime / rotationIncreasedTime;
            switch (direction){
                
                case RotationDirection.ClockWise :
                    transform.Rotate(Vector3.up * rotatingSpeed * Time.deltaTime,Space.World);

                break;
                case RotationDirection.Anti_clockWise :
                    transform.Rotate(Vector3.up * -rotatingSpeed * Time.deltaTime,Space.World);

                break;
                
            }
        }
    }     



}

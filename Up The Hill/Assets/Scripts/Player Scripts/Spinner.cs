using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spinner : MonoBehaviour {
    
    
    [SerializeField] private float spineSpeed = 20f;
    [SerializeField] private RotationDirection direction;

    private void Start(){
        iTween.RotateBy(gameObject,iTween.Hash(
            "y",(direction == RotationDirection.ClockWise ? 360f : -360f),
            "loopType",iTween.LoopType.loop,
            "Speed",spineSpeed,
            "easyType",iTween.EaseType.linear
        ));
    }
}

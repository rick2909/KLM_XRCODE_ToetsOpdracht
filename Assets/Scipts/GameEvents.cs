using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake(){
        current = this;
    }

    public event Action<int> onHangerDoorTriggerEnter;
    public void HangerDoorTriggerEnter(int id){
        if(onHangerDoorTriggerEnter != null){
            onHangerDoorTriggerEnter(id);
            Debug.Log("Trigger");
        }
    }

    public event Action<int> onHangerDoorTriggerExit;
    public void HangerDoorTriggerExit(int id){
        if(onHangerDoorTriggerExit != null){
            onHangerDoorTriggerExit(id);
            Debug.Log("Trigger Exit");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public int id;
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Plane"){
            Debug.Log("Collide: " + id);
            GameEvents.current.HangerDoorTriggerEnter(id);
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.gameObject.tag == "Plane"){
            Debug.Log("exit");
            GameEvents.current.HangerDoorTriggerExit(id);
        }
    }
}

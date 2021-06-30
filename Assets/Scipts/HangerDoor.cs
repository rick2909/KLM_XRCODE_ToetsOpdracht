using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HangerDoor : MonoBehaviour
{
    public int id;
    Animator animator;

    // Use this for initialization
    private void Start () {
        animator = GetComponent<Animator>();
        GameEvents.current.onHangerDoorTriggerEnter += OnHangerOpen;
        GameEvents.current.onHangerDoorTriggerExit += OnHangerClose;
    }

    private void OnHangerOpen(int id) {
        if(id == this.id){
            Doorcontroller("Open");
        }
    }
    private void OnHangerClose(int id)
    {
        if(id == this.id){
            Doorcontroller("Close");
        }
    }

    private void Doorcontroller(string direction)
    {
        animator.SetTrigger(direction);
    }

    private void OnDestroy () {
        GameEvents.current.onHangerDoorTriggerEnter -= OnHangerOpen;
        GameEvents.current.onHangerDoorTriggerExit -= OnHangerClose;
    }
}

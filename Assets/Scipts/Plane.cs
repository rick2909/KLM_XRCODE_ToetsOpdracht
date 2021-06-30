using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Plane", menuName = "Plane")]
public class Plane : ScriptableObject {

    public new string name;
    public string type;
    public string typeCode;
    public string company;
    public Transform[] waitingPoints;

    public Vector3 getWaitingPoint(){
        return waitingPoints[Random.Range(0, waitingPoints.Length)].position;
    }
}

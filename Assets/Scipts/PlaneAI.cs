using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaneAI : MonoBehaviour {

    public Plane plane;
    public Transform spawn;
    public Transform hanger;
    public Vector3 destination;
    public TextMeshPro nameText;

    public GameObject[] lights;

    bool reachedDestination = false;
    public bool reachedParking = false;

    public int afstand = 1;
    UnityEngine.AI.NavMeshAgent agent;

    // Start is called before the first frame update
    void Start() {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nameText.text = plane.name;
        SetDestination();
    }

    // Update is called once per frame
    void Update() {
        if (reachedDestination) {
            reachedDestination = false;
            SetDestination();
        } else {
            if (Vector3.Distance(agent.transform.position, destination) < afstand){
                reachedDestination = true;
            }

            if (Vector3.Distance(agent.transform.position, hanger.position) < afstand){
                reachedParking = true;
            }
        }
    }

    public void Lights(bool lightOn){
        foreach (GameObject l in lights)
        {
            l.GetComponent<Light>().enabled = lightOn;
        }
    }

    public void Park(){
        agent.destination = hanger.position;
    }

    private void SetDestination(){
        destination = plane.getWaitingPoint();
        agent.destination = destination;
    }

    
}

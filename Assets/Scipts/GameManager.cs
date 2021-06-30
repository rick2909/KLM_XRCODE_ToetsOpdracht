using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour{
    public Transform[] outsideStops;
    public GameObject[] hangerPostions;

    public GameObject planeObject;
    public Plane[] planeInfo;

    private int spawnAmount;

    private int stopsAmount = 4;

    private bool lightsOn = false;
    public TextMeshProUGUI lightsText;
    public TextMeshProUGUI parkedText;
    public List<GameObject> planesParked;
    public List<GameObject> spawnedPlanes;
    // Start is called before the first frame update
    void Start()
    {
        // set amount of planes
        spawnAmount = planeInfo.Length;

        // set spawn and hanners to list so they can be randomly assigned to planes
        List<Transform> spawns = new List<Transform>(outsideStops);
        List<GameObject> hangers = new List<GameObject>(hangerPostions);

        if(hangers.Count < spawnAmount){
            spawnAmount = hangers.Count;
        }

        for(int i = 0; i < spawnAmount; i++){
            // select random spawn/hanger
            Transform spawn = spawns[Random.Range(0, spawns.Count)];
            GameObject hanger = hangers[hangers.Count-1];

            List<Transform> stops = new List<Transform>();
            stops = createStops(spawns, stops, 0);

            Transform parkingSpot = hanger.transform.Find("ParkingSpot");
            hanger.transform.Find("NameText").GetComponent<TextMeshPro>().text = "Hanger " + (i+1);

            // get script component
            PlaneAI planes = planeObject.GetComponent<PlaneAI>();
            // remove used points
            spawns.Remove(spawn);
            hangers.Remove(hanger);

            planeInfo[i].waitingPoints = stops.ToArray();
            planeInfo[i].name = "Vliegtuig " + (i+1);
            planes.spawn = spawn;
            planes.hanger = parkingSpot;
            planes.plane = planeInfo[i];
            spawnedPlanes.Add(Instantiate(planeObject, spawn.position, spawn.rotation));
        }
    }

    // Update is called once per frame
    void Update() {
        foreach (GameObject p in spawnedPlanes)
        {
            if(p.GetComponent<PlaneAI>().reachedParking && !planesParked.Contains(p) ){
                planesParked.Add(p);
            }
        }

        if(planesParked.Count == spawnedPlanes.Count){
            Debug.Log("All parked");
            parked();
        }
    }

    private void parked(){
        Debug.Log("All parked");
        parkedText.enabled = true;
    }

    private List<Transform> createStops(List<Transform> points , List<Transform> stops, int stopNumber){
        // Break loop
        if(stopNumber >= stopsAmount){
            return stops;
        }

        Transform stop = points[Random.Range(0, points.Count-1)];
        if(!stops.Contains(stop)){
            stops.Add(stop);
            stopNumber++;
        }

        return createStops(points, stops, stopNumber);
    }

    public void Lights(){
        lightsOn = !lightsOn;

        if(lightsOn){
            lightsText.text = "Lights off";
        }else{
            lightsText.text = "Lights on";
        }
        foreach (GameObject p in spawnedPlanes)
        {
            p.GetComponent<PlaneAI>().Lights(lightsOn);
        }
    }

    public void Park(){
        foreach (GameObject p in spawnedPlanes)
        {
            p.GetComponent<PlaneAI>().Park();
        }
    }
}

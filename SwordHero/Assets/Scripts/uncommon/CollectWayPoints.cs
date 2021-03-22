using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectWayPoints : MonoBehaviour
{
    public Transform[] waypoints;
    public GameObject[] objects;

    void Awake()
    {
        objects = GameObject.FindGameObjectsWithTag("WayPoint");
        waypoints = new Transform[objects.Length];

        //for (int i = 0; i < objects.Length; i++)
        //{
        //    Debug.Log(i);

        //    GameObject.FindGameObjectWithTag("WayPoint").transform.parent = this.transform;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

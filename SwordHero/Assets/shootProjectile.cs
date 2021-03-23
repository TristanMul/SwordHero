using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootProjectile : MonoBehaviour
{

    public GameObject projectile;
    public GameObject character;
    public Transform startingPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if
    }

    void spawnProjectile()
    {
        Instantiate(projectile, startingPoint);
    }
}

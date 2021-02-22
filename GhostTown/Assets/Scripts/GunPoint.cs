using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPoint : Standards
{
    public GameObject prefab;
    public float distance = 10.0f;
    public bool CanShoot;
    public float waitTime = 5.0f;
    public float charge = 0.0f;
    float force;
    public ParticleSystem fireParticle;
    public GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        force = gameManager.BallSpeed;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Fire();
        }
        if (Input.GetMouseButton(0))
        {
            charge += Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(0))
        {
            charge = 0.0f;
        }
        if (charge >= waitTime)
        {
            //Debug.Log("Firing");
            //Fire();
            charge = 0.0f;
        }
    }

    public void Fire(GameObject target)
    {
        //var position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        //position = Camera.main.ScreenToWorldPoint(position);
        var go = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
        Vector3 look = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);

        go.transform.LookAt(look, Vector3.up);
        //Debug.Log(position);

        fireParticle.Play();

        go.GetComponent<ShotBehavior>().speed = force;
    }
}

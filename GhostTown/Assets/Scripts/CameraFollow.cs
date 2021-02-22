using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Standards
{
    public GameObject player;
    public Vector3 offset;
    public Quaternion standardRotation;
    float speed;
    public GameManager gameManager;
    public MobileKeyboard target;

    // Update is called once per frame
    void Start()
    {
        standardRotation = transform.rotation;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        speed = gameManager.CameraSpeed;
    }
    void Update()
    {
        transform.position = player.transform.position + offset;
        //if (target.lockedEnemy != null) {
        //    Vector3 relativePos = target.lockedEnemy.transform.position - transform.position;
        //    Quaternion toRotation = Quaternion.LookRotation(relativePos);
        //    transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime); }
        //else { transform.rotation = standardRotation; }
        
    }

}

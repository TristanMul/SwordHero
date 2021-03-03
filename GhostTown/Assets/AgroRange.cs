using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgroRange : MonoBehaviour
{
    public GameObject player;
    public float spotRange;
    public float attackRange;
    float distance;
    SpriteRenderer plane;

    // Start is called before the first frame update
    void Start()
    {
        plane = this.gameObject.GetComponent<SpriteRenderer>();
        plane.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //get distance between player and enemy awake trigger
        distance = Vector3.Distance(player.transform.position, gameObject.transform.position);

        //player in range of enemy group
        if (distance < spotRange)
        {
            plane.enabled = true;
            plane.color = new Color(plane.color.r, plane.color.g, plane.color.b, (1-((distance - (spotRange / 1.5f)) /10)));
        }
    }
}

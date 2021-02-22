using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheck : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;

    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;

        m_Collider = GetComponent<Collider>();
    }

    private void Update()
    {
        this.transform.position = player.transform.position + offset;
    }

    public float m_MaxDistance;
    public float m_Speed;
    public bool m_HitDetect;
    public Collider m_Collider;
    public RaycastHit m_Hit;
    public LayerMask layer;

    void FixedUpdate()
    {
        //Test to see if there is a hit using a BoxCast
        //Calculate using the center of the GameObject's Collider(could also just use the GameObject's position), half the GameObject's size, the direction, the GameObject's rotation, and the maximum distance as variables.
        //Also fetch the hit data
        m_HitDetect = Physics.BoxCast(transform.position, transform.localScale, transform.forward, out m_Hit, transform.rotation, m_MaxDistance, layer);
        if (m_HitDetect)
        {
            if (m_Hit.collider.gameObject.tag == "Enemy")
            {
                //Debug.Log("Stop");
                player.GetComponent<Player>().go = false;
            }
           
        }
        else 
        {
            //Debug.Log("Go");
            player.GetComponent<Player>().go = true;
        }
    }

    //Draw the BoxCast as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (m_HitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, transform.forward * m_Hit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + transform.forward * m_Hit.distance, transform.localScale);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, transform.forward * m_MaxDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + transform.forward * m_MaxDistance, transform.localScale);
        }
    }
}

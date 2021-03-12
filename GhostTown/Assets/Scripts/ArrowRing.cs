using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRing : MonoBehaviour
{
    public GameObject arrow;
    public int numObjects;
    public float arrowSpeed;
    float ang;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnArrows();
        }
    }


    Vector3 ArrowCircle(Vector3 center, float radius)
    {
        ang = ang + (360 / numObjects);
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }

    public void SpawnArrows()
    {
        Vector3 center = transform.position;
        for (int i = 0; i < numObjects; i++)
        {
            Vector3 pos = ArrowCircle(center, 0.5f);
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            GameObject _arrow = Instantiate(arrow, pos, rot);
            _arrow.GetComponent<Rigidbody>().AddForce(_arrow.transform.forward * arrowSpeed, ForceMode.Impulse);
        }
    }
}

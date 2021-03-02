using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeGhosts : MonoBehaviour
{
    public GameObject ghosts;

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Player")
        {
            ghosts.SetActive(true);
        }
    }
}

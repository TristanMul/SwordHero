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
            Debug.Log("Awaken");
            //ghosts.GetComponentInChildren<GameObject>().SetActive(true);
            ghosts.SetActive(true);
        }
    }
}

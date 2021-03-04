using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeGhosts : MonoBehaviour
{
    public GameObject trigger;

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Player")
        {
            // ghosts.SetActive(true);
            foreach (GameObject enemy in trigger.transform)
            {
                if(enemy.GetComponent<GhostAwaken>()){
                    enemy.GetComponent<GhostAwaken>().WakeUpGhost();
                }
            }
        }
    }
}

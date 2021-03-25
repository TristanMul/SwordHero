using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageEnemies : MonoBehaviour
{
    private PlayerMovement playerMove;

    void Awake()
    {
        playerMove = transform.parent.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            playerMove.AddEnemy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            playerMove.RemoveEnemy(other.gameObject);
        }
    }
}

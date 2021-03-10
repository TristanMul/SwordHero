using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemies : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        // Activate when player enters range.
        if(other.tag == "Player"){
            foreach (Transform enemy in transform)
            {
                // Hide range indicator.
                if(enemy.name == "AllAxis_Outline"){
                    enemy.gameObject.SetActive(false);
                }

                // All enemies start attacking the player.
                if(enemy.GetComponent<EnemyBaseClass>()){
                    enemy.GetComponent<EnemyBaseClass>().enemyState = EnemyBaseClass.EnemyState.Move;
                }
            }
        }
    }
}
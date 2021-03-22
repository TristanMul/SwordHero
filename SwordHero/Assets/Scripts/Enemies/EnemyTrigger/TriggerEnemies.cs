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
                // Turn off collider so trigger won't trigger multiple times.
                GetComponent<Collider>().enabled = false;

                // Hide range indicator.
                if(enemy.name == "AllAxis_Outline" || enemy.name == "Ring Mesh"){
                    enemy.gameObject.SetActive(false);
                }

                // Individual enemies start following player.
                if(enemy.gameObject.tag == "Enemy"){
                    enemy.GetComponent<FollowPlayer>().enabled = true;
                    enemy.GetComponent<EnemyBaseClass>().enemyState = EnemyBaseClass.EnemyState.Move;
                }

                // Group of enemies start following player.
                if(enemy.gameObject.tag == "EnemyGroup"){
                    foreach (Transform enemyIndividual in enemy.transform)
                    {
                        enemyIndividual.GetComponent<FollowPlayer>().enabled = true;
                        enemyIndividual.GetComponent<EnemyBaseClass>().enemyState = EnemyBaseClass.EnemyState.Move;
                    }
                }
            }
        }
    }
}

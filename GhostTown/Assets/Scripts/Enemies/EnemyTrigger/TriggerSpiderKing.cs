using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpiderKing : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        // Activate when player enters range.
        if(other.tag == "Player"){
            foreach (Transform enemy in transform)
            {
                // Hide range indicator.
                if(enemy.name == "AllAxis_Outline" || enemy.name == "Ring Mesh"){
                    enemy.gameObject.SetActive(false);
                }

                // All enemies start attacking the player.
                if(enemy.GetComponent<EnemyBaseClass>()){
                    // Turn off collider so trigger won't trigger multiple times.
                    GetComponent<Collider>().enabled = false;

                    // Enemies start following player.
                    enemy.GetComponent<MoveSpiderKing>().enabled = true;
                    enemy.GetComponent<EnemyBaseClass>().enemyState = EnemyBaseClass.EnemyState.Move;
                }
            }
        }
    }
}

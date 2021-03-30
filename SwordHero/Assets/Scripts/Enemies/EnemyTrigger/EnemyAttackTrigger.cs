using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTrigger : MonoBehaviour
{
    private EnemyAttack parentEnemy;

    private void Start() {
        parentEnemy = transform.parent.GetComponent<EnemyAttack>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            parentEnemy.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player"){
            parentEnemy.enabled = false;
        }
    }
}

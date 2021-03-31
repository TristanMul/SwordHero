using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTrigger : MonoBehaviour
{
    private Transform parentEnemy;

    private void Start() {
        parentEnemy = transform.parent;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            parentEnemy.GetComponent<EnemyBaseClass>().enemyState = EnemyBaseClass.EnemyState.Attack;
            parentEnemy.GetComponent<EnemyAttack>().enabled = true;
            parentEnemy.GetComponent<FollowPlayer>().enabled = false;
        }
    }
    private void OnTriggerStay(Collider other) {
        if(other.tag == "Player"){
            parentEnemy.GetComponent<EnemyBaseClass>().enemyState = EnemyBaseClass.EnemyState.Attack;
            parentEnemy.GetComponent<EnemyAttack>().enabled = true;
            parentEnemy.GetComponent<FollowPlayer>().enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player"){
            parentEnemy.GetComponent<EnemyBaseClass>().enemyState = EnemyBaseClass.EnemyState.Move;
            parentEnemy.GetComponent<FollowPlayer>().enabled = true;
            parentEnemy.GetComponent<EnemyAttack>().enabled = false;
            parentEnemy.GetComponent<FollowPlayer>().enabled = true;
        }
    }
}

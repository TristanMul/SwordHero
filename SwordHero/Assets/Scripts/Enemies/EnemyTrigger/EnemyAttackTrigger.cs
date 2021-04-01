using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackTrigger : MonoBehaviour
{
    private Transform parentEnemy;

    private void Start() {
        parentEnemy = transform.parent;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            PlayerIsInRange();
        }
    }
    private void OnTriggerStay(Collider other) {
        if(other.tag == "Player"){
            PlayerIsInRange();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player"){
            PlayerWentOutOfRange();
        }
    }

    private void PlayerIsInRange(){
        parentEnemy.GetComponent<EnemyBaseClass>().enemyState = EnemyBaseClass.EnemyState.Attack;
        parentEnemy.GetComponent<EnemyAttack>().enabled = true;
        parentEnemy.GetComponent<NavMeshAgent>().SetDestination(parentEnemy.transform.position);
        parentEnemy.GetComponent<FollowPlayer>().enabled = false;
    }

    private void PlayerWentOutOfRange(){
        parentEnemy.GetComponent<EnemyBaseClass>().enemyState = EnemyBaseClass.EnemyState.Move;
        parentEnemy.GetComponent<FollowPlayer>().enabled = true;
        parentEnemy.GetComponent<EnemyAttack>().enabled = false;
    }
}

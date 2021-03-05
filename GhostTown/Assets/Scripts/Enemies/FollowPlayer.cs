using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public EnemyBaseClass controllerClass;  // The controller class of this enemy object.
    private GameObject player;
    private NavMeshAgent navMeshAgent;
    

    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = controllerClass.speed;
    }

    private void FixedUpdate() {
        MoveToPlayer();
    }

    // Face the player and move towards them.
    private void MoveToPlayer(){
        if(player && controllerClass.enemyState == EnemyBaseClass.EnemyState.Move){
            transform.LookAt(player.transform);
            navMeshAgent.SetDestination(player.transform.position);
        }
    }

}

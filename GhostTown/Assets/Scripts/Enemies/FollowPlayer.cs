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
        // Get reference to player.
        player = GameManager.instance._player;

        // Set NavMesch settings.
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = controllerClass.Speed;
    }

    private void FixedUpdate() {
        MoveToPlayer();
    }

    // Face the player and move towards them.
    private void MoveToPlayer(){
        if(player && controllerClass.enemyState == EnemyBaseClass.EnemyState.Move){
            navMeshAgent.SetDestination(player.transform.position);
        }
    }
}

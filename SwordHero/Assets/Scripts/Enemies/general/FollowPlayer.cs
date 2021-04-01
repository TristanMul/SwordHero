using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyBaseClass))]
public class FollowPlayer : MonoBehaviour
{
    public EnemyBaseClass controllerClass;  // The controller class of this enemy object.
    [HideInInspector] public GameObject player;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    
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
    public virtual void MoveToPlayer(){
        if(player && controllerClass.enemyState == EnemyBaseClass.EnemyState.Move && navMeshAgent.enabled){
            navMeshAgent.SetDestination(player.transform.position);
        }
    }
}

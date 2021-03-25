using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemFollow : FollowPlayer
{
    public float attackRange;
    public float triggerRange;

    [HideInInspector] public bool isAttacking;

    public override void MoveToPlayer()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        Debug.Log(dist);
        if (player)
        {
            if(dist > triggerRange && !isAttacking)
            {
                ExecuteAction(EnemyBaseClass.EnemyState.Idle, transform.position);
            }
            else if (dist < attackRange)
            {
                ExecuteAction(EnemyBaseClass.EnemyState.Attack, transform.position);
            }
            else if(dist > attackRange && dist < triggerRange && !isAttacking)
            {
                ExecuteAction(EnemyBaseClass.EnemyState.Move, player.transform.position);
            }
        }
    }

    void ExecuteAction(EnemyBaseClass.EnemyState state, Vector3 _position)
    {
        navMeshAgent.SetDestination(_position);
        controllerClass.enemyState = state;
    }
}

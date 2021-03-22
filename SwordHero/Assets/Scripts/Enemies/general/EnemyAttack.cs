using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBaseClass))]
public class EnemyAttack : MonoBehaviour
{
    public EnemyBaseClass controllerClass;  // The controller class of this enemy object.
    private GameObject player;
    
    void Start()
    {
        player = GameManager.instance._player;
    }

    // Check if the player is in the range of this enemy's attack.
    private void CheckAttackRange(){
        // The player is in the range of this enemies attack.
        if(Vector3.Distance(transform.position, player.transform.position) <= controllerClass.AttackRange){
            controllerClass.enemyState = EnemyBaseClass.EnemyState.Attack;
            GetComponent<FollowPlayer>().enabled = false;
        }
    }
}

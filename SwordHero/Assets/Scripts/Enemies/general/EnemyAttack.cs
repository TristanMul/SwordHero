using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBaseClass))]
public class EnemyAttack : MonoBehaviour
{
    public EnemyBaseClass controllerClass;  // The controller class of this enemy object.
    private Animator animator;
    private GameObject player;
    private bool isAttacking;
    private float attackTimer;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameManager.instance._player;
        isAttacking = false;
    }

    private void Update() {
        Attack();
    }

    // Check if the player is in the range of this enemy's attack.
    private void Attack(){
        // The player is in the range of this enemies attack.
        isAttacking = true;
        controllerClass.enemyState = EnemyBaseClass.EnemyState.Attack;
        GetComponent<FollowPlayer>().enabled = false;

        attackTimer = animator.GetCurrentAnimatorStateInfo(0).length;
        Invoke("AttackCompleted", attackTimer);
    }

    private void AttackCompleted(){
        isAttacking = false;
        controllerClass.enemyState = EnemyBaseClass.EnemyState.Move;
        GetComponent<FollowPlayer>().enabled = true;
    }
}

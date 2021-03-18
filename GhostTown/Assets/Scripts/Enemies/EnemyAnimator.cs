using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private EnemyBaseClass enemy;
    private Animator animator;

    public virtual void Start()
    {
        enemy = GetComponent<EnemyBaseClass>();
        animator = GetComponent<Animator>();
        enemy.setAnimations();
    }

    private void FixedUpdate()
    {
        AnimationHandler();
    }

    // Play animation according to spider's state
    private void AnimationHandler()
    {
        switch (enemy.enemyState)
        {
            case EnemyBaseClass.EnemyState.Idle:
                animator.Play(enemy.IdleAnimation);
                break;
            case EnemyBaseClass.EnemyState.Move:
                animator.Play(enemy.MoveAnimation);
                break;
            case EnemyBaseClass.EnemyState.Attack:
                animator.Play(enemy.AttackAnimation);
                break;
            case EnemyBaseClass.EnemyState.Damaged:
                animator.Play(enemy.DamagedAnimation);
                break;
            case EnemyBaseClass.EnemyState.Death:
                animator.Play(enemy.DeathAnimation);
                break;
            case EnemyBaseClass.EnemyState.SpecialAttack:
                animator.Play(enemy.SpecialAttackAnimation);
                break;
            default:
                animator.Play(enemy.IdleAnimation);
                break;
        }
    }
}

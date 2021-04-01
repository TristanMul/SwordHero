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
                AnimationSetter(enemy.IdleAnimation);
                break;
            case EnemyBaseClass.EnemyState.Move:
                AnimationSetter(enemy.MoveAnimation);
                break;
            case EnemyBaseClass.EnemyState.Attack:
                AnimationSetter(enemy.AttackAnimation);
                break;
            case EnemyBaseClass.EnemyState.Damaged:
                AnimationSetter(enemy.DamagedAnimation);
                break;
            case EnemyBaseClass.EnemyState.Death:
                AnimationSetter(enemy.DeathAnimation);
                break;
            case EnemyBaseClass.EnemyState.SpecialAttack:
                AnimationSetter(enemy.SpecialAttackAnimation);
                break;
            case EnemyBaseClass.EnemyState.Emote:
                AnimationSetter(enemy.EmoteAnimation);
                break;
            default:
                AnimationSetter(enemy.IdleAnimation);
                break;
        }
    }

    private void AnimationSetter(string state)
    {
        foreach(string _state in enemy.AnimationList)
        {
            if(_state == state)
            {
                animator.SetBool(state, true);
            }
            else
            {
                animator.SetBool(_state, false);
            }
        }
    }
}

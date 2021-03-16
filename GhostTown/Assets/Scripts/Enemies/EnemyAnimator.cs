using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [HideInInspector] public EnemyBaseClass enemy;
    [HideInInspector] public Animator animator;

    public virtual void Start()
    {
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
                animator.Play("Idle");
                break;
            case EnemyBaseClass.EnemyState.Move:
                animator.Play("Crawl Forward Fast In Place");
                break;
            case EnemyBaseClass.EnemyState.Attack:
                animator.Play("Bite Attack");
                break;
            case EnemyBaseClass.EnemyState.Damaged:
                animator.Play("Take Damage");
                break;
            case EnemyBaseClass.EnemyState.Death:
                animator.Play("Die");
                break;
            default:
                animator.Play("Idle");
                break;
        }
    }
}

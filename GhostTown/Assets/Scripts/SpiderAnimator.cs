using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimator : MonoBehaviour
{
    private Spider spider;
    private Animator spiderAnimator;

    void Start()
    {
        spider = GetComponent<Spider>();
        spiderAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        AnimationHandler();
    }

    // Play animation according to spider's state
    private void AnimationHandler(){
        switch(spider.enemyState){
            case EnemyBaseClass.EnemyState.Idle:
                spiderAnimator.Play("Idle");
                break;
            case EnemyBaseClass.EnemyState.Move:
                spiderAnimator.Play("Crawl Forward Fast In Place");
                break;
            case EnemyBaseClass.EnemyState.Attack:
                spiderAnimator.Play("Bite Attack");
                break;
            case EnemyBaseClass.EnemyState.Damaged:
                spiderAnimator.Play("Take Damage");
                break;
            case EnemyBaseClass.EnemyState.Death:
                spiderAnimator.Play("Die");
                break;
            default:
                spiderAnimator.Play("Idle");
                break;
        }
    }
}

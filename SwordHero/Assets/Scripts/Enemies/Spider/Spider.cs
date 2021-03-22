using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : EnemyBaseClass
{

    void Start()
    {
        enemyState = EnemyState.Idle;
        setAnimations();
    }
    public override void setAnimations()
    {
        IdleAnimation = "Idle";
        MoveAnimation = "Crawl Forward Fast In Place";
        AttackAnimation = "Bite Attack";
        DeathAnimation = "Die";
        DamagedAnimation = "Take Damage";

    }
}

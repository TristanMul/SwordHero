using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gollem : EnemyBaseClass
{
    void Start()
    {
        enemyState = EnemyState.Idle;
        setAnimations();
    }
    public override void setAnimations()
    {
        IdleAnimation = "Idle";
        MoveAnimation = "Move";
        AttackAnimation = "Attack";
        DeathAnimation = "Death";
        DamagedAnimation = "Take Damage";
    }
}

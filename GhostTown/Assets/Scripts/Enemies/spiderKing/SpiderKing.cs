using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SpiderKing : EnemyBaseClass
{
    [SerializeField] private MoveSpiderKing movement;
    public Animator npcAnimator;
    int hashCastSpell; 
    void Start()
    {
        setAnimations();
        //enemyState = EnemyState.Idle;
    }

    public override void setAnimations()
    {
        IdleAnimation = "Idle";
        MoveAnimation = "Crawl forward slow in place";
        SpecialAttackAnimation = "Cast Spell";

    }
}

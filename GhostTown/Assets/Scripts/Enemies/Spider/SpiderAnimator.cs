using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimator : EnemyAnimator
{

    public override void Start()
    {
        enemy = GetComponent<Spider>();
        animator = GetComponent<Animator>();
    }
}

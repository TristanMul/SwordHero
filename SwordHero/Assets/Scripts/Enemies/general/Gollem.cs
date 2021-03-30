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
}

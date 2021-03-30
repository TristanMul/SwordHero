using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireGolem : EnemyBaseClass
{
    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.Idle;
        setAnimations();
    }
}

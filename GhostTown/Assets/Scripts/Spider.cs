using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : EnemyBaseClass
{

    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.Idle;
    }

}

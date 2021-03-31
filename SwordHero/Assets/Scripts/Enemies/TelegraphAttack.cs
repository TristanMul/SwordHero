using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelegraphAttack : MonoBehaviour
{
    EnemyBaseClass thisEnemy;
    SpriteRenderer sprite;
    bool isAttacking;
    // Start is called before the first frame update
    void Start()
    {
        thisEnemy = GetComponentInParent<EnemyBaseClass>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForAttack();
        if (isAttacking)
        {
            sprite.enabled = true;
        }
        else
        {
            sprite.enabled = false;
        }
    }

    void CheckForAttack()
    {
        if (thisEnemy.enemyState == EnemyBaseClass.EnemyState.Attack)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }
}

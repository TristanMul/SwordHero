using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderKingLogic : MonoBehaviour
{
    public EnemyBaseClass controllerClass;
    private Animator animator;
    bool hasCastSpell = false;
    [SerializeField] private Object spiderBallPrefab;
    [SerializeField] private Transform mouth;
    [SerializeField] private int maxFireBalls;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        StartCoroutine(returnToIdle());
       
    }
    IEnumerator returnToIdle()
    {
        if(controllerClass.enemyState == EnemyBaseClass.EnemyState.SpecialAttack && !hasCastSpell)
        {
            hasCastSpell = true;
            yield return new WaitForSeconds(0.6f);
            animator.speed = 0;
            StartCoroutine(shootProjectiles());
            animator.speed = 1;
            yield return new WaitForSeconds(0.4f);
            hasCastSpell = true;
            controllerClass.enemyState = EnemyBaseClass.EnemyState.Idle;
        }
    }
    IEnumerator shootProjectiles()
    {
        for(int i = 0; i < maxFireBalls; i++)
        {
        Instantiate(spiderBallPrefab, mouth);      
        yield return new WaitForSeconds(0.1f);
        }
        StopCoroutine(shootProjectiles());
    }
}

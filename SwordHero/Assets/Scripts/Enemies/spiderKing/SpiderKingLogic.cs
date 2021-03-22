using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderKingLogic : MonoBehaviour
{
    public EnemyBaseClass controllerClass;
    private Animator animator;
    bool hasCastSpell = false;
    private GameObject bombArea;

    [SerializeField] private Object spiderBallPrefab;
    [SerializeField] private Transform mouth1, mouth2, mouth3;
    [SerializeField] private int maxFireBalls;

    private void Start()
    {
        animator = GetComponent<Animator>();
        bombArea = GameObject.Find("BombDropPlace");
        bombArea.gameObject.SetActive(false);
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
            controllerClass.enemyState = EnemyBaseClass.EnemyState.Idle;
        }
        StopCoroutine(returnToIdle());
    }

    IEnumerator shootProjectiles()
    {
        /*for(int i = 0; i < maxFireBalls; i++)
        {
            Instantiate(spiderBallPrefab, mouth);      
            yield return new WaitForSeconds(0.1f);
        }*/
        Instantiate(spiderBallPrefab, mouth1);
        Instantiate(spiderBallPrefab, mouth2);
        Instantiate(spiderBallPrefab, mouth3);
        yield return new WaitForSeconds(1f);
        bombArea.gameObject.SetActive(true);
        StopCoroutine(shootProjectiles());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderKing : EnemyBaseClass
{
    [SerializeField] private MoveSpiderKing movement;
    public Animator npcAnimator;
    int hashCastSpell; 
    void Start()
    {
        hashCastSpell = Animator.StringToHash("castSpell");
        enemyState = EnemyState.Idle;
    }
    private void FixedUpdate()
    {
        shootProjectiles();
    }
    IEnumerator shootProjectiles()
    {
        Debug.Log(npcAnimator.GetBool(movement.hashReachedDestination));
        if (npcAnimator.GetBool(movement.hashReachedDestination))
        {
           yield return new WaitForSeconds(0.6f);
            npcAnimator.SetBool(hashCastSpell, true);
        }
    }
}

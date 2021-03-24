using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackPlayer : MonoBehaviour
{
    public EnemyBaseClass controllerClass;  // The controller class of this enemy object.
    [SerializeField] private GameObject player;
    // Start is called before the first frame update

    private void AttackTimer()
    {

    }
    private void CheckAttackRange()
    {
        // The player is in the range of this enemies attack.
        if (Vector3.Distance(transform.position, player.transform.position) <= controllerClass.AttackRange)
        {
            controllerClass.enemyState = EnemyBaseClass.EnemyState.Attack;
            GetComponent<FollowPlayer>().enabled = false;
        }
    }
}

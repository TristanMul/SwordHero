using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveSpiderKing : MonoBehaviour
{
    public EnemyBaseClass controllerClass;
    [SerializeField] private GameObject target;
    private NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    private void Start()
    {
        // Set NavMesch settings.
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = controllerClass.Speed;
    }
    private void FixedUpdate()
    {
        Debug.Log(controllerClass.enemyState);
        moveToTarget();
    }
    void moveToTarget()
    {
        if(controllerClass.enemyState == EnemyBaseClass.EnemyState.Move)
        {
            navMeshAgent.SetDestination(target.transform.position);
        }
    }
}

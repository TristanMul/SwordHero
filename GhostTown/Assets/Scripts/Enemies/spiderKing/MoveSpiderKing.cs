using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveSpiderKing : MonoBehaviour
{
    public EnemyBaseClass controllerClass;
    [SerializeField] private GameObject target;
    private NavMeshAgent navMeshAgent;
    public Animator npcAnimator;
    public int hashSpeed, hashReachedDestination;
    [SerializeField] float movementSpeed;
    bool hasReachedDestination = false;
    // Start is called before the first frame update
    private void Start()
    {
        // increases preformance of game by turning the string into a hashcode
        hashSpeed = Animator.StringToHash("speed");
        hashReachedDestination = Animator.StringToHash("hasReachedDestination");
        // Set NavMesch settings.
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = controllerClass.Speed;
    }
    private void FixedUpdate()
    {
        moveToTarget();
        checkIfDestinationReached();
       // Debug.Log("Cast Spell:" + npcAnimator.GetAnimatorTransitionInfo(0).IsName("hasCastSpell"));
        //Debug.Log("Transitioning:" + npcAnimator.IsInTransition(0));
    }
    void moveToTarget()
    {
        if(controllerClass.enemyState == EnemyBaseClass.EnemyState.Move)
        {
            navMeshAgent.speed = movementSpeed;
            navMeshAgent.SetDestination(target.transform.position);
            npcAnimator.SetFloat(hashSpeed, navMeshAgent.speed);
        }
    }
    void checkIfDestinationReached()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget < 1)
        {

            navMeshAgent.speed = 0;
            controllerClass.enemyState = EnemyBaseClass.EnemyState.SpecialAttack;
        }
    }
}

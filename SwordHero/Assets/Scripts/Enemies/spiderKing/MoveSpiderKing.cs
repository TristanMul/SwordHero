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
    public bool isMoving;
    public GameObject chargeParticle;

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
        if (isMoving)
        {
            moveToTarget();
        }
        checkIfDestinationReached();
    }

    void moveToTarget()
    {
        if (controllerClass.enemyState == EnemyBaseClass.EnemyState.Move)
        {
            navMeshAgent.speed = movementSpeed;
            navMeshAgent.SetDestination(target.transform.position);
            npcAnimator.SetFloat(hashSpeed, navMeshAgent.speed);
        }
    }

    /// <summary>
    /// Activates the movement of the player
    /// </summary>
    public void StartMoving()
    {
        isMoving = true;
        GetComponent<EnemyBaseClass>().enemyState = EnemyBaseClass.EnemyState.Move;
    }

    void checkIfDestinationReached()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget < 1f)
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        navMeshAgent.speed = 0;
        GetComponent<EnemyBaseClass>().enemyState = EnemyBaseClass.EnemyState.Idle;
        chargeParticle.SetActive(true);
        yield return new WaitForSeconds(1f);
        controllerClass.enemyState = EnemyBaseClass.EnemyState.SpecialAttack;
        this.GetComponent<MoveSpiderKing>().enabled = false;
        yield return new WaitForSeconds(0.75f);
        chargeParticle.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class shootProjectile : MonoBehaviour
{
    EnemyBaseClass controllerClass;
    public GameObject projectile;
    public GameObject character;
    public GameObject player;
    public Transform startingPoint;
    public Animator npcAnimator;
    private NavMeshAgent navMeshAgent;

    private float decreaseCooldown;
    bool hasCastSpell = false;
    [SerializeField] private float cooldownTimer;
    [SerializeField] private float minRandomTime, maxRandomTime;
    float setTimer, animationTimer;
    // Start is called before the first frame update
    void Start()
    {
       // animationTimer = ;
        controllerClass = gameObject.GetComponent<EnemyBaseClass>();
        setTimer = Random.Range(minRandomTime, maxRandomTime);
        decreaseCooldown = cooldownTimer;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        checkIfInRange();
        CooldownTimer();
    }
    void checkIfInRange()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= controllerClass.AttackRange && !hasCastSpell)
        {
            Attack();
        }
        else if( distance <= controllerClass.AttackRange)
        {
            controllerClass.enemyState = EnemyBaseClass.EnemyState.Idle;
            navMeshAgent.speed = 0;  
        }
        else
        {
            controllerClass.enemyState = EnemyBaseClass.EnemyState.Move;
            navMeshAgent.speed = controllerClass.Speed;
            animationTimer = 0;
        }

    }
    void Attack()
    {
        animationTimer += Time.deltaTime;
        controllerClass.enemyState = EnemyBaseClass.EnemyState.Attack;
        navMeshAgent.speed = 0;
        if (animationTimer >= npcAnimator.GetCurrentAnimatorStateInfo(0).length)
        {
            hasCastSpell = true;
            Instantiate(projectile, startingPoint.position, startingPoint.rotation);
           // projectile.transform.Rotate(0, 0, 0);
            animationTimer = 0;
            controllerClass.enemyState = EnemyBaseClass.EnemyState.Move;
            navMeshAgent.speed = controllerClass.Speed;
        }
    }
    void CooldownTimer()
    {
        decreaseCooldown -= Time.deltaTime;
        if(decreaseCooldown <= 0 && hasCastSpell)
        {
            decreaseCooldown = cooldownTimer;
            hasCastSpell = false;
        }
    }
}

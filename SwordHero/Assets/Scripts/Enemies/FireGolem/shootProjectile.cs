using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class shootProjectile : EnemyAttack
{
    public GameObject projectile;
    public GameObject character;
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
        controllerClass = GetComponent<EnemyBaseClass>();
        player = GameManager.instance._player;
        // animationTimer = ;
        setTimer = Random.Range(minRandomTime, maxRandomTime);
        decreaseCooldown = cooldownTimer;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        checkIfHasCast();
        CooldownTimer();
    }
    void checkIfHasCast()
    {
            if (!hasCastSpell)
            {
                controllerClass.enemyState = EnemyBaseClass.EnemyState.Attack;
                Attack();
            }
            else
            {
                controllerClass.enemyState = EnemyBaseClass.EnemyState.Idle;
            }
    }
    void Attack()
    {
        if (!hasCastSpell && controllerClass.enemyState != EnemyBaseClass.EnemyState.Fall)
        {
            animationTimer += Time.deltaTime;
            if (animationTimer >= npcAnimator.GetCurrentAnimatorStateInfo(0).length)
            {
                hasCastSpell = true;
                navMeshAgent.SetDestination(transform.position);
                transform.LookAt(player.transform);
                Instantiate(projectile, startingPoint.position, startingPoint.rotation);
                // projectile.transform.Rotate(0, 0, 0);
                animationTimer = 0;
            }
        }

    }
    void CooldownTimer()
    {
        decreaseCooldown -= Time.deltaTime;
        if (decreaseCooldown <= 0 && hasCastSpell)
        {
            decreaseCooldown = cooldownTimer;
            hasCastSpell = false;
        }
    }
}

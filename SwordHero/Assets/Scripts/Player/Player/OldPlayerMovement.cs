using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPlayerMovement : MonoBehaviour
{
    public GameObject defaultTarget;
    public float moveSpeed = 5f;
    public int lookSpeed;

    private GameManager gameManager;
    private DynamicJoystick dynamicJoystick;
    private Rigidbody character;

    [HideInInspector] public Animator animator;
    [HideInInspector] public Transform firingPoint;
    [HideInInspector] public GameObject _enemy;
    [HideInInspector] public bool attackAnim;
    [HideInInspector] public Vector3 movement;

    GameObject enemy;
    SpecialAbility ability;
    ArrowRing ringOfArrows;

    void Awake()
    {
        gameManager = GameObject.Find("EventSystem").GetComponent<GameManager>();
        //gameManager = GameManager.instance;
        dynamicJoystick = GameObject.Find("Dynamic Joystick").GetComponent<DynamicJoystick>();
        animator = GetComponentInChildren<Animator>();
        character = GetComponent<Rigidbody>();
        ability = transform.Find("Charging Circle").GetComponent<SpecialAbility>();
        firingPoint = transform.Find("FiringPoint").GetComponent<Transform>();
        gameManager.playerAlive = true;
        ringOfArrows = GetComponentInChildren<ArrowRing>();
    }

    void FixedUpdate()
    {
        movement.x = dynamicJoystick.Horizontal;
        movement.z = dynamicJoystick.Vertical;

        animator.SetFloat("AngleController", Mathf.Atan2(movement.z, movement.x) * Mathf.Rad2Deg);
        animator.SetFloat("AnglePlayer", transform.eulerAngles.y);
        animator.SetFloat("MovementX", Mathf.Abs(movement.x * 10), 0.1f, Time.deltaTime);
        animator.SetFloat("MovementZ", Mathf.Abs(movement.z * 10), 0.1f, Time.deltaTime);
        animator.SetFloat("MovementXZ", (Mathf.Abs(movement.x * 10) + Mathf.Abs(movement.z * 10)) / 2, 0.1f, Time.deltaTime);
        animator.SetFloat("AttackSpeed", 1 / gameObject.GetComponentInChildren<ShootArrow>().fireRate);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        FaceClosestEnemy();
        //transform.rotation = Quaternion.LookRotation(movement);

        // Play footstep smoke effect if player is moving.
        if(movement.x != 0 || movement.z != 0){
            transform.GetComponent<FootstepSmoke>().PlayFootstepSmokeEffect();
            ability.ChargePower();
        }
        
        //whenever activate ability whenever player is not moving.
        if (movement.x == 0 && movement.z == 0 && ability.powerCharged)
        {
            StartCoroutine(SpecialAttack());
            ability.powerCharged = false;
        }

        if (attackAnim)
        {
            animator.SetBool("AttackRange", true);
        }
        else
        {
            animator.SetBool("AttackRange", false);
        }
    }
  
    void FaceClosestEnemy()
    {
        float closestEnemy = Mathf.Infinity;
        GameObject enemy = null;
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject currentEnemy in allEnemies)
        {
            var targetRotation = currentEnemy.transform.position - transform.position;
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < closestEnemy)
            {
                closestEnemy = distanceToEnemy;
                enemy = currentEnemy;
            }
        }

        if (enemy != null)
        {
            // Only target enemies that move towards or attack player.
            if(enemy.GetComponent<EnemyBaseClass>().enemyState == EnemyBaseClass.EnemyState.Move ||
                enemy.GetComponent<EnemyBaseClass>().enemyState == EnemyBaseClass.EnemyState.Attack)
            {
                StartCoroutine(DoRotationAtTargetDirection(enemy.transform));
                _enemy = enemy;
            }
            // Ther are no enemies moving towards or attacking player.
            else
            {
                enemy = null;
            }
            
        }
        if(enemy == null)
        {
            StartCoroutine(DoRotationAtTargetDirection(defaultTarget.transform));
            //Not a great solution, but needed if the code isn't going to be rewritten. needs a defaultTarget to be assigned-
        }
    }

    IEnumerator DoRotationAtTargetDirection(Transform opponentPlayer)
    {
        Quaternion targetRotation = Quaternion.identity;
        do
        {
            Vector3 targetDirection = (new Vector3(opponentPlayer.position.x, 0, opponentPlayer.position.z) - new Vector3(transform.position.x, 0, transform.position.z)).normalized;
            targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * (moveSpeed * lookSpeed));
            yield return null;

        } while (Quaternion.Angle(transform.rotation, targetRotation) < 0.01f && gameObject != null && opponentPlayer);
    }

    public IEnumerator OnDeath()
    {
        movement = new Vector3(0, 0, 0);
        gameObject.GetComponentInChildren<Animator>().enabled = false;
        gameManager.playerAlive = false;
        yield return null;
    }


    IEnumerator SpecialAttack()
    {
        animator.SetLayerWeight(1, 0.0f);
        ringOfArrows.SpawnArrows();
        animator.SetBool("SuperAttack", true);
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("SuperAttack", false);
        ability.ResetCircleSize();
        animator.SetLayerWeight(1, 1);
    }
}
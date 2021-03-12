using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public DynamicJoystick FixedJoystick;
    public GameManager gameManager; 
    public Animator animator;
    public Rigidbody character;
    public Transform firingPoint;
    //public GameObject finish;
    public float moveSpeed = 5f;
    public int lookSpeed;
    //public bool shrinkPos;
    [HideInInspector]
    public GameObject _enemy;
    [HideInInspector]
    public bool attackAnim;
    Vector3 movement;
    GameObject enemy;
    SpecialAbility ability;
    void Awake()
    {
        ability = transform.Find("Charging Circle").GetComponent<SpecialAbility>();
        gameManager.playerAlive = true;
        enemy = gameManager._enemy;
    }

    void FixedUpdate()
    {
        movement.x = FixedJoystick.Horizontal;
        movement.z = FixedJoystick.Vertical;

        animator.SetFloat("AngleController", Mathf.Atan2(movement.z, movement.x) * Mathf.Rad2Deg);
        animator.SetFloat("AnglePlayer", transform.eulerAngles.y);
        animator.SetFloat("MovementX", Mathf.Abs(movement.x * 10), 0.1f, Time.deltaTime);
        animator.SetFloat("MovementZ", Mathf.Abs(movement.z * 10), 0.1f, Time.deltaTime);
        animator.SetFloat("MovementXZ", (Mathf.Abs(movement.x * 10) + Mathf.Abs(movement.z * 10)) / 2, 0.1f, Time.deltaTime);
        animator.SetFloat("AttackSpeed", 1 / gameObject.GetComponentInChildren<ShootArrow>().fireRate);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        FaceClosestEnemy();
        //transform.rotation = Quaternion.LookRotation(movement);
        Debug.Log(movement.x + " " + movement.y);
        // Play footstep smoke effect if player is moving.
        if(movement.x != 0 || movement.z != 0){
            transform.GetComponent<FootstepSmoke>().PlayFootstepSmokeEffect();
            ability.ChargePower();
        }
        //whenever activate ability whenever player is not moving.
        if(movement.x == 0 && movement.z == 0 && ability.powerCharged)
        {
            Debug.Log("Is not moving");
            ability.ResetCircleSize();
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
            StartCoroutine(DoRotationAtTargetDirection(enemy.transform));
            _enemy = enemy;
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

        } while (Quaternion.Angle(transform.rotation, targetRotation) < 0.01f && gameObject != null);
    }

    public IEnumerator OnDeath()
    {
        movement = new Vector3(0, 0, 0);
        gameObject.GetComponentInChildren<Animator>().enabled = false;
        gameManager.playerAlive = false;
        yield return null;
    }
}
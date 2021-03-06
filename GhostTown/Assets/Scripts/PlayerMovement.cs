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
    Vector3 movement;
    GameObject enemy;
    Vector3 regularSize; 

    void Awake()
    {
        gameManager.playerAlive = true;
        regularSize = new Vector3(0.2f, 0.5f, 1f);
        enemy = gameManager._enemy;
    }

    void FixedUpdate()
    {
        animator.SetFloat("AngleController", Mathf.Atan2(movement.z, movement.x) * Mathf.Rad2Deg);
        animator.SetFloat("AnglePlayer", transform.eulerAngles.y);

        movement.x = FixedJoystick.Horizontal;
        movement.z = FixedJoystick.Vertical;

        FaceClosestEnemy();
        //transform.rotation = Quaternion.LookRotation(movement);
        
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        // Play footstep smoke effect if player is moving.
        if(movement.x > 0 || movement.z > 0){
            transform.GetComponent<FootstepSmoke>().PlayFootstepSmokeEffect();
        }
        
        animator.SetFloat("MovementX", Mathf.Abs(movement.x * 10), 0.1f, Time.deltaTime);
        animator.SetFloat("MovementZ", Mathf.Abs(movement.z * 10), 0.1f, Time.deltaTime);
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


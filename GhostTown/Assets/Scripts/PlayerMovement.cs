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
    [SerializeField] GameObject circle; 
    [SerializeField] private float increaseSize;
    private float sizeIncreased = 0;
    Vector3 circleResetSize;
    [SerializeField] private float triggerSize;
    private bool changedColor = false;
    //private Color translucentYellow = new Color(249,166,2,)
    void Awake()
    {
        gameManager.playerAlive = true;
        enemy = gameManager._enemy;
        sizeIncreased += circle.transform.localScale.x;
        circleResetSize = circle.transform.localScale;
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
        if (Input.GetKey(KeyCode.Y))
        {
            ResetCircleSize();
        }
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        FaceClosestEnemy();
        ChangeCirclesize();
        Debug.Log(changedColor);
        //transform.rotation = Quaternion.LookRotation(movement);

        // Play footstep smoke effect if player is moving.
        if(movement.x != 0 || movement.z != 0){
            transform.GetComponent<FootstepSmoke>().PlayFootstepSmokeEffect();
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
    private void ChangeCirclesize()
    {
        if (sizeIncreased <= triggerSize)
        {
        circle.transform.localScale += new Vector3(increaseSize, increaseSize, 0);
            sizeIncreased += increaseSize;

        }
        else if (!changedColor)
        {
            Debug.Log("Special power ready");
            //circle.GetComponent<SpriteRenderer>().color = translucentYellow;
                changedColor = true;
        }

    }
    private void ResetCircleSize()
    {
        //circle.GetComponent<SpriteRenderer>().color = translucentWhite;
        circle.transform.localScale = circleResetSize;
        sizeIncreased = circle.transform.localScale.x;
        changedColor = false;
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
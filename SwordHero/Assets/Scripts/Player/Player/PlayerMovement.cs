using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject defaultTarget;
    public float moveSpeed = 5f;
    public int lookSpeed;
    [SerializeField] private float dashSpeed;
    /// <summary>
    /// The speed with which the dash speed reduces to the movement speed 0-1
    /// </summary>
    [SerializeField] private float dashSpeedReduction = 10f;
    [SerializeField] private float dashTime = .4f;
    private float currentDashSpeed;

    private Rigidbody projectile;
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
    List<GameObject> allEnemies = new List<GameObject>();
    ShootArrow shootArrow;


    #region events
    public delegate void DashDelegate();
    public DashDelegate onDash;
    public delegate void StopDashDelegate();
    public StopDashDelegate stopDash;
    #endregion

    #region Dashing Variables
    public bool isMoving { get { return movement.magnitude != 0f; } }
    bool isDashing;
    public bool IsDashing { get { return isDashing; } }
    #endregion

    void Awake()
    {
        //projectile = GameObject.FindGameObjectWithTag("Projectile").GetComponent<Rigidbody>();
        //allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        gameManager = GameObject.Find("EventSystem").GetComponent<GameManager>();
        gameManager.playerAlive = true;

        //gameManager = GameManager.instance;
        dynamicJoystick = GameObject.Find("Dynamic Joystick").GetComponent<DynamicJoystick>();
        animator = GetComponentInChildren<Animator>();
        character = GetComponent<Rigidbody>();
        firingPoint = transform.Find("FiringPoint").GetComponent<Transform>();
        ringOfArrows = GetComponentInChildren<ArrowRing>();
        ability = transform.Find("Charging Circle").GetComponent<SpecialAbility>();
        shootArrow = GetComponentInChildren<ShootArrow>();

    }

    void FixedUpdate()
    {

        movement.x = dynamicJoystick.Horizontal;
        movement.z = dynamicJoystick.Vertical;

        if (!isDashing) { transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World); }//Normal movement

        else if (isDashing)
        {
            currentDashSpeed = Mathf.Lerp(currentDashSpeed, moveSpeed, dashSpeedReduction * Time.deltaTime);
            transform.Translate(transform.forward * currentDashSpeed * Time.deltaTime, Space.World);
        }

        animator.SetFloat("AngleController", Mathf.Atan2(movement.z, movement.x) * Mathf.Rad2Deg);
        animator.SetFloat("AnglePlayer", transform.eulerAngles.y);
        animator.SetFloat("MovementX", Mathf.Abs(movement.x * 10), 0.1f, Time.deltaTime);
        animator.SetFloat("MovementZ", Mathf.Abs(movement.z * 10), 0.1f, Time.deltaTime);
        animator.SetFloat("MovementXZ", (Mathf.Abs(movement.x * 10) + Mathf.Abs(movement.z * 10)) / 2, 0.1f, Time.deltaTime);
        if (shootArrow != null)
        {
            animator.SetFloat("AttackSpeed", 1 / shootArrow.fireRate);
        }
        //transform.rotation = Quaternion.LookRotation(movement);

        //makes the player rotate towards the walking direction
        if (!isDashing)
        {
            float rotationSpeed = Time.deltaTime * lookSpeed;
            Vector3 targetDirection = movement;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        // Play footstep smoke effect if player is moving.
        if (movement.x != 0 || movement.z != 0)
        {
            transform.GetComponent<FootstepSmoke>().PlayFootstepSmokeEffect();
            if (ability != null)
            {
                ability.ChargePower();
            }
        }

        //whenever activate ability whenever player is not moving.

        if (movement.x == 0 && movement.z == 0 && ability.powerCharged)
        {
            if (ability != null)
            {
                StartCoroutine(SpecialAttack());

                ability.powerCharged = false;
            }
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


    public void Dash()
    {
        isDashing = true;
        StartCoroutine(StopDashInTime(dashTime));
        currentDashSpeed = dashSpeed;
        onDash();
    }
    private IEnumerator StopDashInTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        isDashing = false;
        stopDash();
    }

    void FaceClosestEnemy()
    {
        if (allEnemies.Count > 0)
        {
            float closestEnemy = Mathf.Infinity;
            GameObject enemy = null;

            foreach (GameObject currentEnemy in allEnemies)
            {
                //var targetRotation = currentEnemy.transform.position - transform.position;
                if (currentEnemy != null)
                {
                    //float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
                    float distanceToEnemy = Vector3.Distance(currentEnemy.transform.position, this.transform.position);

                    if (distanceToEnemy < closestEnemy)
                    {
                        closestEnemy = distanceToEnemy;
                        enemy = currentEnemy;
                    }
                }
            }

            if (enemy != null)
            {
                DoRotationToTarget(enemy.transform);
                _enemy = enemy;
            }

        }
        else
        {
            DoRotationToTarget(defaultTarget.transform);
        }
    }

    void DoRotationToTarget(Transform targetEnemy)
    {
        float rotationSpeed = Time.deltaTime * lookSpeed;
        if (targetEnemy != null)
        {
            Vector3 targetDirection = targetEnemy.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    public void AddEnemy(GameObject enemy)
    {
        allEnemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        allEnemies.Remove(enemy);
    }


    IEnumerator SpecialAttack()
    {
        animator.SetLayerWeight(1, 0.0f);
        ringOfArrows.SpawnArrows();
        animator.SetBool("SuperAttack", true);
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("SuperAttack", false);
        if (ability != null)
        { ability.ResetCircleSize(); }
        animator.SetLayerWeight(1, 1);
    }

    public IEnumerator OnDeath()
    {
        movement = new Vector3(0, 0, 0);
        gameObject.GetComponentInChildren<Animator>().enabled = false;
        gameManager.playerAlive = false;
        yield return null;
    }
}
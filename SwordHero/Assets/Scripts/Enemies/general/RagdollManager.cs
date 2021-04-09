using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(EnemyHealth))]
public class RagdollManager : MonoBehaviour
{
    List<Collider> ragdollColliders;
    PhysicsDamage physicsDamage;
    Animator animator;
    NavMeshAgent agent;
    Rigidbody rb;
    EnemyHealth health;
    EnemyBaseClass controllerclass;
    GameObject hitTarget;
    [SerializeField] float maxRotation;
    [Header("fall variables")]
    #region variablesForFalling
    [SerializeField] float startRotationSpeed;
    float currentRotationSpeed;
    [SerializeField] float rotationAcceleration;
    bool fallRotationStarted;

    public bool FallRotationStarted
    {
        get { return fallRotationStarted; }
        set
        {
            fallRotationStarted = value;

        }
    }

    [SerializeField] float timeToStandUp;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        ragdollColliders = GetRagdollColliders();
        physicsDamage = GetComponent<PhysicsDamage>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        health = GetComponent<EnemyHealth>();
        controllerclass = GetComponent<EnemyBaseClass>();

        health.StartRagdoll += SetupRagdoll;
        DisableRagdoll();
    }

    List<Collider> GetRagdollColliders()
    {
        List<Collider> bones = new List<Collider>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("Bone"))
            {
                bones.Add(child.GetComponent<Collider>());
            }
        }
        return bones;
    }
    void SetupRagdoll()
    {
        if (!fallRotationStarted)
        {
            animator.enabled = false;
            agent.enabled = false;
            ChangeRagdollColliders(true);


            StartCoroutine(FallRotation());
            fallRotationStarted = true;
        }
    }
    void DisableRagdoll()
    {
        animator.enabled = true;
        agent.enabled = true;
        ChangeRagdollColliders(false);
    }


    /// <summary>
    /// Either disables or enables many colliders to do with the ragdoll
    /// </summary>
    /// <param name="value">Wether to enable or disable the colliders</param>
    void ChangeRagdollColliders(bool value)
    {
        foreach (Collider col in ragdollColliders)
        {
            col.enabled = value;
        }
    }

    IEnumerator FallRotation()
    {
        float distanceRotated = 0f;
        float fallDelay;
        currentRotationSpeed = startRotationSpeed;
        Quaternion startRotation = transform.rotation;//saves the rotation at the beginning
        //transform.Rotate(transform.up,)
        while (distanceRotated < 90f)
        {
            transform.Rotate(new Vector3(-currentRotationSpeed * Time.deltaTime, transform.rotation.y, transform.rotation.z));
            // transform.Rotate(Vector3.right, -currentRotationSpeed * Time.deltaTime);
            distanceRotated += currentRotationSpeed * Time.deltaTime;
            currentRotationSpeed += Time.deltaTime * rotationAcceleration;//Speeds up the falling

            yield return null;
        }
        yield return new WaitForSeconds(timeToStandUp);
        if (health.controllerClass.enemyState != EnemyBaseClass.EnemyState.Death)
        {
            animator.enabled = true;
            //transform.rotation = startRotation;
            transform.Rotate(new Vector3(currentRotationSpeed, 0, 0));
            animator.Play("StandUp");
            DisableRagdoll();
            fallDelay = animator.GetCurrentAnimatorStateInfo(0).length;
            Invoke("CharacterIsStanding", fallDelay);
            yield return new WaitForSeconds(fallDelay);
            controllerclass.enemyState = EnemyBaseClass.EnemyState.Move;
            GetComponent<FollowPlayer>().enabled = true;
        }
    }

    private void CharacterIsStanding()
    {
        fallRotationStarted = false;//Enables the ability to fall down
        Debug.Log("Standing");
    }

    public void ReceiveDirection(GameObject target)
    {
        hitTarget = target;
    }

}

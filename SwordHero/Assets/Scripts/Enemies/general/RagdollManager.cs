using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RagdollManager : MonoBehaviour
{
    CapsuleCollider[] ragdollColliders;
    PhysicsDamage physicsDamage;
    Animator animator;
    NavMeshAgent agent;
    Rigidbody rb;
    EnemyHealth health;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.rotation);
        ragdollColliders = GetComponentsInChildren<CapsuleCollider>();
        physicsDamage = GetComponent<PhysicsDamage>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        health = GetComponent<EnemyHealth>();

        health.StartRagdoll += SetupRagdoll;
        health.StopRagdoll += DisableRagdoll;
        DisableRagdoll();

    }


    void SetupRagdoll() {
        animator.enabled = false;
        agent.enabled = false;
        ChangeRagdollColliders(true);
        rb.isKinematic = false;
    }
    void DisableRagdoll()
    {
        animator.enabled = true;
        agent.enabled = true;
        ChangeRagdollColliders(false);
        rb.isKinematic = true;
    }


    /// <summary>
    /// Either disables or enables many colliders to do with the ragdoll
    /// </summary>
    /// <param name="value">Wether to enable or disable the colliders</param>
    void ChangeRagdollColliders(bool value) {
        foreach (CapsuleCollider col in ragdollColliders) {
            col.enabled = value;
        } 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SpiderKing : EnemyBaseClass
{
    [SerializeField] private MoveSpiderKing movement;
    public Animator npcAnimator;
    int hashCastSpell;
    bool hasDropped;
    NavMeshAgent agent;
    Rigidbody rb;
    CameraManager cam;
    public ParticleSystem dropParticles;
    MoveSpiderKing moveSpiderKing;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main.GetComponent<CameraManager>();
        rb = GetComponent<Rigidbody>();
        setAnimations();
        moveSpiderKing = GetComponent<MoveSpiderKing>();
        //enemyState = EnemyState.Idle;
    }

    public override void setAnimations()
    {
        IdleAnimation = "Idle";
        MoveAnimation = "Crawl forward slow in place";
        SpecialAttackAnimation = "Cast Spell";
        EmoteAnimation = "Bite Attack";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasDropped && collision.gameObject.tag == "Walkable")//What happens when the spider king hits the ground
        {
            hasDropped = true;
            agent.enabled = true;
            rb.useGravity = false;
            StartCoroutine(cam.Shake(1f, .2f));


            // Enemies start following player.
            GetComponent<MoveSpiderKing>().enabled = true;
            GetComponent<SpiderKing>().enabled = true;


            dropParticles.Play();
            GetComponent<EnemyBaseClass>().enemyState = EnemyBaseClass.EnemyState.Emote;
            StartCoroutine(waitAndStartWalking(npcAnimator.GetCurrentAnimatorStateInfo(0).length / npcAnimator.GetCurrentAnimatorStateInfo(0).speed));
        }
    }

    /// <summary>
    /// Waits for a designated time and starts the walking process
    /// </summary>
    /// <param name="time">Useful to get the duration of the animation that's currently running</param>
    /// <returns></returns>
    IEnumerator waitAndStartWalking(float time)
    {
        yield return new WaitForSeconds(time);
        moveSpiderKing.StartMoving();
    }

    public void Drop()
    {
        rb.useGravity = true;
    }
}

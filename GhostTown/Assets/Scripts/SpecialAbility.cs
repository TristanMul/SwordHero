using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbility : MonoBehaviour
{
   
    [SerializeField] private float increaseSize;
    private float sizeIncreased = 0;
    Vector3 circleResetSize;
    [SerializeField] private float triggerSize;
    [HideInInspector] public bool powerCharged = false;
    Color translucentYellow = new Color(0.9f,0.75f,0f,0.45f);
    Color translucentWhite = new Color(1,1,1,0.45f);
    public PlayerParticles particles;
    PlayerMovement player;
    ArrowRing arrowRing;


    private void Awake()
    {
        player = GetComponentInParent<PlayerMovement>();
        arrowRing = player.GetComponentInChildren<ArrowRing>();
        sizeIncreased += transform.localScale.x;
        circleResetSize = transform.localScale;
        //translucentYellow = new Color(249 / 255, 215 / 255, 0 / 255, 100 / 255);
        //translucentWhite = new Color(1, 1, 1, 100 / 255);
    }

    private void FixedUpdate()
    {
        ChangeCirclesize();
    }

    public void ChangeCirclesize()
    {
        if (player.movement.x != 0 && player.movement.z != 0)
        {
            if (sizeIncreased <= triggerSize)
            {
                transform.localScale += new Vector3(increaseSize, increaseSize, 0);
                sizeIncreased += increaseSize;
                if (!particles.ChargingParticlesActive)
                {
                    particles.ChargingParticlesActive = true;
                }
            }
            else if (!powerCharged)
            {
                particles.PlayIsCharged();
                particles.WhileChargedActive = true;
                particles.ChargingParticlesActive = false;

                GetComponent<SpriteRenderer>().color = translucentYellow;
                powerCharged = true;
            }
        }

        //if(player.movement.x == 0 && player.movement.z == 0 && powerCharged)
        //{
        //    StartCoroutine(PlayAnimation());
        //    StopCoroutine(PlayAnimation());
        //}

    }
    public void ResetCircleSize()
    {
        GetComponent<SpriteRenderer>().color = translucentWhite;
        transform.localScale = circleResetSize;
        sizeIncreased = transform.localScale.x;
        powerCharged = false;

        particles.WhileChargedActive = false;
    }

    //IEnumerator PlayAnimation()
    //{
    //    player.animator.SetBool("SuperAttack", true);
    //    player.animator.SetLayerWeight(1, 0);
    //    arrowRing.SpawnArrows();
    //    yield return new WaitForSeconds(player.animator.GetCurrentAnimatorStateInfo(0).length + player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
    //    player.animator.SetBool("SuperAttack", false);
    //    player.animator.SetLayerWeight(1, 1);
    //    ResetCircleSize();
    //}
}

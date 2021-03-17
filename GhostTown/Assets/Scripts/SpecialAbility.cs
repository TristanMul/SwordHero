using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbility : MonoBehaviour
{
   
    [SerializeField] private float increaseSize;
    [SerializeField] private float triggerSize;
    private float sizeIncreased = 0;
    private Transform rangeRing;
    private ArrowRing arrow;

    [HideInInspector] public bool powerCharged = false;
    public Color translucentYellow;
    public Color translucentWhite;
    public PlayerParticles particles;

    PlayerMovement player;
    ArrowRing arrowRing;
    Vector3 circleResetSize;
    DestroyAfterSeconds arrowRange;

    private void Awake()
    {
        arrow = GameObject.Find("Special Attack").GetComponent<ArrowRing>();
        arrowRange = arrow.arrow.GetComponent<DestroyAfterSeconds>();
        player = GetComponentInParent<PlayerMovement>();
        arrowRing = player.GetComponentInChildren<ArrowRing>();
        sizeIncreased += transform.localScale.x;
        circleResetSize = transform.localScale;
        rangeRing = GameObject.Find("Arrow Range").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        arrowRange.timer = triggerSize / 10f;
        rangeRing.localScale = new Vector3(triggerSize / 1.22f, triggerSize / 1.22f, 0);
    }

    public void ChargePower()
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
    }
    public void ResetCircleSize()
    {
        GetComponent<SpriteRenderer>().color = translucentWhite;
        transform.localScale = circleResetSize;
        sizeIncreased = transform.localScale.x;
        powerCharged = false;

        particles.WhileChargedActive = false;
    }
}

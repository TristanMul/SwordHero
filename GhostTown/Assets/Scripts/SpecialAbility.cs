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
    public Color translucentYellow;
    public Color translucentWhite;
    public PlayerParticles particles;
    PlayerMovement player;
    ArrowRing arrowRing;


    private void Awake()
    {
        player = GetComponentInParent<PlayerMovement>();
        arrowRing = player.GetComponentInChildren<ArrowRing>();
        sizeIncreased += transform.localScale.x;
        circleResetSize = transform.localScale;
    }

    private void FixedUpdate()
    {
        
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

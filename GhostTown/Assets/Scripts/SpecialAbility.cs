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

    private void Awake()
    {
        sizeIncreased += transform.localScale.x;
        circleResetSize = transform.localScale;
        //translucentYellow = new Color(249 / 255, 215 / 255, 0 / 255, 100 / 255);
        //translucentWhite = new Color(1, 1, 1, 100 / 255);
    }
    private void FixedUpdate()
    {
        
    }
    public void ChargePower()
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
    public void ResetCircleSize()
    {
        GetComponent<SpriteRenderer>().color = translucentWhite;
        transform.localScale = circleResetSize;
        sizeIncreased = transform.localScale.x;
        powerCharged = false;

        particles.WhileChargedActive = false;
    }
}

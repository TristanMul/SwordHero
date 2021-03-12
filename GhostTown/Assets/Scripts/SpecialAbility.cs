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

    private void Awake()
    {
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
        
        if (sizeIncreased <= triggerSize)
        {
            transform.localScale += new Vector3(increaseSize, increaseSize, 0);
            sizeIncreased += increaseSize;
            if (!particles.ParticlesRunning)
            {
                particles.ParticlesRunning = true;
            }
        }
        else if (!powerCharged)
        {
            particles.PlayIsCharged();
            GetComponent<SpriteRenderer>().color = translucentYellow;
            powerCharged = true;
            particles.ParticlesRunning = false;
        }

    }
    public void ResetCircleSize()
    {
        GetComponent<SpriteRenderer>().color = translucentWhite;
        transform.localScale = circleResetSize;
        sizeIncreased = transform.localScale.x;
        powerCharged = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTest : MonoBehaviour
{
    bool particlesRunning;
    private ParticleSystem particles;

    public bool ParticlesRunning//set to activate/deactivate particles
    {
        get { return particlesRunning; }
        set
        {
            Debug.Log(value);
            if (value) { 
                particles.Play();
            }
            else {
                particles.Stop();
            }
            particlesRunning = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (particlesRunning)
            {
                ParticlesRunning = false;
            }
            else { ParticlesRunning = true; }
        }
    }

    void StartParticles()
    {

    }

    void StopParticles()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTest : MonoBehaviour
{
    public bool testing;
    bool particlesRunning;//To activate the charging particles set ParticlesRunning to true, mind the capitol letter
    public ParticleSystem chargingParticles;
    public ParticleSystem isChargedParticles;

    public bool ParticlesRunning//set to activate/deactivate particles
    {
        get { return particlesRunning; }
        set
        {
            Debug.Log(value);
            if (value) { 
                chargingParticles.Play();
            }
            else {
                chargingParticles.Stop();
            }
            particlesRunning = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
            if (testing && Input.GetKeyDown(KeyCode.Mouse0))
            {
                ParticlesRunning = !particlesRunning;
            }
    }

    public void PlayIsCharged()
    {
        isChargedParticles.Play();
    }

}

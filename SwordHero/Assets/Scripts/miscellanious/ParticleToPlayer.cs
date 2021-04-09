using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleToPlayer : MonoBehaviour
{
    public float timeToStart;

    private Transform Target;
    private ParticleSystem system;
    int count;
    bool systemIsActive;
    private static ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1000];
    

    void Start()
    {

        Target = GameManager.instance._player.transform;
        GameManager.onAllenemiesDefeated += ActivateParticleSystem; 
        systemIsActive = false;
        
    }

    void FixedUpdate()
    {
        if(systemIsActive){
            timeToStart -= Time.deltaTime;
        }

        if (timeToStart < 0)
        {
            count = system.GetParticles(particles);

            for (int i = 0; i < count; i++)
            {
                ParticleSystem.Particle particle = particles[i];

                Vector3 v1 = system.transform.TransformPoint(particle.position);
                Vector3 v2 = Target.transform.position;
                Vector3 tarPosi = (v2 - v1) * (particle.remainingLifetime / particle.startLifetime);

                particle.position = system.transform.InverseTransformPoint(v2 - tarPosi);
                particles[i] = particle;
            }
            system.SetParticles(particles, count);
        }

        if(timeToStart >= -2 && timeToStart < -0.1)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void ActivateParticleSystem(){
        transform.GetChild(0).gameObject.SetActive(true);
        system = transform.GetChild(0).GetComponent<ParticleSystem>();
        system.Play();
        systemIsActive = true;
    }

    private void OnDisable() {
        GameManager.onAllenemiesDefeated -= ActivateParticleSystem;
    }
}

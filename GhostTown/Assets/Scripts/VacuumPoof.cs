using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumPoof : MonoBehaviour
{
    
    public ParticleSystem poof;
    private ParticleSystem.MainModule main;

    private void Start() {
        main = poof.main;
    }

    // Change the color of the particle effect to match the caught ghost color.
    // Play the particle effect.
    public void PlayPoofEffect(Color color){
        main.startColor = color;
        poof.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSmoke : MonoBehaviour
{
    public ParticleSystem footstepSmoke;

    // Play the footstep smoke particle effect.
    public void PlayFootstepSmokeEffect(){
        footstepSmoke.Play();
    }
}

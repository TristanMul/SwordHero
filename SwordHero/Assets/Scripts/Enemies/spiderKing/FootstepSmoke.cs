using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSmoke : MonoBehaviour
{
    public ParticleSystem footstepSmoke;

    // Play the footstep smoke particle effect if not already playing.
    public void PlayFootstepSmokeEffect(){
        if(!footstepSmoke.isPlaying){
            footstepSmoke.Play();
        }
    }
}

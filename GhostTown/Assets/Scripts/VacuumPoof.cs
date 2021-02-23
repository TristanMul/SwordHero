using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumPoof : MonoBehaviour
{
    
    public ParticleSystem poof;

    public void PlayPoofEffect(){
        poof.Play();
    }
}

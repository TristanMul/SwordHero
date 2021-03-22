using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootEffectBoss : MonoBehaviour
{
    CameraManager cam;
    ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraManager>();
        particles = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Walkable")
        {
            StartCoroutine(cam.Shake(.2f, .05f));
            particles.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardEffect : MonoBehaviour
{
    private Transform cam;

    private void Start() {
        cam = Camera.main.transform;
    }

    private void LateUpdate() {
        // Face the camera at all time.
        transform.LookAt(transform.position + cam.forward);
    }
}

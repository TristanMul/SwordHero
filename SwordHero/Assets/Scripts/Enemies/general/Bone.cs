using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    private Collider collider;
    private float colliderTime = 2f;
    private float removeTime = 1f;
    private bool goingToRemove = false;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!goingToRemove &&  collision.gameObject.tag == "Walkable")
        {
            StartCoroutine(RemoveBone());
        }
    }

    IEnumerator RemoveBone() {
        goingToRemove = true;
        yield return new WaitForSeconds(colliderTime);
        collider.enabled = false;
        yield return new WaitForSeconds(removeTime);
        Destroy(gameObject);
    }
}

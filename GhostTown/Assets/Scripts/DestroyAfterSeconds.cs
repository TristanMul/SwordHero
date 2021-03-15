using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float timer;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(DestroyObject(timer));
    }

    IEnumerator DestroyObject(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}

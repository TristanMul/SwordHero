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
        
        transform.position = transform.position;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}

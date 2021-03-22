using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactParticle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Destruct());
    }

    private IEnumerator Destruct()
    {
        yield return new WaitForSeconds(0.3f);

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

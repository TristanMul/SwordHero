using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explode : MonoBehaviour
{ 
    public GameObject explosion;
    public SphereCollider coll;


    private void Awake()
    {
        coll.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Walkable")
        {
            Debug.Log("Has triggered");
            Instantiate(explosion,transform.position,Quaternion.identity);
            StartCoroutine(TurnOnTrigger(0.5f));
        }
    }

    IEnumerator TurnOnTrigger(float explosionTime)
    {
        Destroy(coll.transform.parent.gameObject);
        yield return new WaitForSeconds(explosionTime / 2);
        coll.enabled = true;
        yield return new WaitForSeconds(explosionTime);
        coll.enabled = false;
        
    }
}

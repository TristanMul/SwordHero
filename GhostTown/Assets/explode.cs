using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explode : MonoBehaviour
{ 
    public GameObject explosion;

 

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Walkable")
        {
            Debug.Log("Has triggered");
            Instantiate(explosion,transform.position,Quaternion.identity);
        }
    }
}

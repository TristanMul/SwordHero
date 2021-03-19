using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explode : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion2;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Walkable")
        {
            Debug.Log("Has triggered");
            Instantiate(explosion,transform.position,Quaternion.identity);
        }
    }
}

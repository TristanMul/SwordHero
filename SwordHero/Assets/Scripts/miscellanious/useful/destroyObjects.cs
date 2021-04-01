using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyObjects : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitDelete : MonoBehaviour
{
    private void OnTriggerEnter(Collider obj)
    {
        Debug.Log("Destroy");
        Destroy(obj.gameObject);
    }
}

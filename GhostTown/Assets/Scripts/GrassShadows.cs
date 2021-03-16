using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassShadows : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        GetComponent<SpriteRenderer>().receiveShadows = true;
    }

    /*void LateUpdate()
    {
        Quaternion r1 = Quaternion.LookRotation(transform.position - Camera.main.transform.position, Vector3.up);
        Vector3 euler2 = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(r1.eulerAngles.x, euler2.y, euler2.z);
    }*/
}

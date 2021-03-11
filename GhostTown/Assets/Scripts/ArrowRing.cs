using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRing : MonoBehaviour
{
    public GameObject arrow;
    public GameObject arrowRing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(arrowRing.GetComponent<Transform>().eulerAngles);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(arrow, new Vector3(arrowRing.transform.position.x, arrowRing.transform.position.y, arrowRing.transform.position.z), 
                Quaternion.Euler(arrowRing.transform.eulerAngles.x + 5, 0,0), this.gameObject.GetComponentInParent<Transform>());
        }
    }
}

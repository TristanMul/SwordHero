using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParts : Standards
{
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("DestroyPartss", 1, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DestroyPartss()
    {
        if (transform.childCount > 0)
        {
            Transform child = transform.GetChild(0);
            Destroy(child.gameObject);
            try
            {
                var destroyText = FindGameObjectInChildWithTag(child.gameObject, "textboxes").GetComponent<KillText>();
                Destroy(destroyText.killLabel);
            }
            catch (NullReferenceException e)
            {
                Debug.Log("not working fully");
            }
            
        }
    }
}

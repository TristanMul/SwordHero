using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsDestroyer : Standards
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Ball"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (FindGameObjectInChildWithTag(collision.gameObject, "textboxes") != null) {
                Destroy(FindGameObjectInChildWithTag(collision.gameObject, "textboxes").GetComponent<KillText>().killLabel);
                Destroy(FindGameObjectInChildWithTag(collision.gameObject, "textboxes"));
                Destroy(FindGameObjectInChildWithTag(collision.gameObject, "enemycamera"));
                Destroy(collision.gameObject);
            } 
        }
    }
}

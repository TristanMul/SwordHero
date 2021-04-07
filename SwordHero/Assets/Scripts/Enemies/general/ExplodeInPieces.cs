using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeInPieces : MonoBehaviour
{
    Transform[] parts;
    Vector3 randomForce;
    Transform parentObject;
    Transform oldParent;
    List<GameObject> childObjects;
    // Start is called before the first frame update
    void Start()
    {
        oldParent = transform.parent;
        parts = GetComponentsInChildren<Transform>();
        childObjects = new List<GameObject>();
        randomForce = new Vector3(Random.Range(1, 10), Random.Range(1, 10), Random.Range(1, 10));
        parentObject = transform.parent.GetComponent<Transform>();

        foreach(Transform child in parts)
        {
            if (child != parts[0])
            {
                childObjects.Add(child.gameObject);
                child.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            SetupForDestroy();
            AddForces(parts);
        }
    }

    void SetupForDestroy()
    {
        transform.parent = null;
        oldParent.gameObject.SetActive(false);
    }

    void AddForces(Transform[] obj)
    {
        foreach (GameObject rb in childObjects)
        {
            rb.gameObject.SetActive(true);
            StartCoroutine(AddForce(rb));
        }
    }

    IEnumerator AddForce(GameObject rb)
    {
        float duration = 0.1f;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            rb.GetComponent<Rigidbody>().AddExplosionForce(2, transform.position, 2, 1, ForceMode.Impulse);
            yield return null;
        }
    }
}

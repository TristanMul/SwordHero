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
    EnemyHealth health;
    private float removeTime = 8f;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponentInParent<EnemyHealth>();
        health.OnEnemyDeath += SetupDestruction;
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

    void SetupDestruction() {
        //StartCoroutine(FallApart());
        SetupForDestroy();
        AddForces();
        Invoke("DestroyGameObject", removeTime);
    }

    IEnumerator FallApart() {
        yield return new WaitForSeconds(1f);
        SetupForDestroy();
        AddForces();
    }

    void SetupForDestroy()
    {
        transform.parent = null;
        oldParent.gameObject.SetActive(false);
    }

    void AddForces()
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

    void DestroyGameObject() {
        Destroy(gameObject);
    }

}

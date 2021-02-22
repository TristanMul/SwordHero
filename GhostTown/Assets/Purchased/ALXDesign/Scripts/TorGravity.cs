using System.Collections.Generic;
using UnityEngine;

public class TorGravity : MonoBehaviour {

    public float ForceGN;
    public float BodyKf;

    private HashSet<Rigidbody> affectedBod = new HashSet<Rigidbody>();
    private Rigidbody componRigidbody;

    // Use this for initialization
    void Start()
    {

        componRigidbody = GetComponent<Rigidbody>();
       }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            affectedBod.Add(other.attachedRigidbody);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            affectedBod.Remove(other.attachedRigidbody);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        foreach (Rigidbody body in affectedBod)
        {
            Vector3 directionOutTor = transform.position - body.position;

            float distance = (transform.position - body.position).sqrMagnitude;
            float strength = ForceGN * (body.mass - componRigidbody.mass) / distance;

            body.AddForce(directionOutTor * strength * BodyKf);

        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class TornadoGravity : MonoBehaviour {

    public float ForceG;
    public float BodyKoeff;
    public float OrbitSpeed;


    private HashSet<Rigidbody> affectedBodies = new HashSet<Rigidbody>();
    private Rigidbody componentRigidbody;

    //float Angle = 0f;

    // Use this for initialization
    void Start () {

        componentRigidbody = GetComponent<Rigidbody>();
        }
	
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            affectedBodies.Add(other.attachedRigidbody);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            affectedBodies.Remove(other.attachedRigidbody);
        }
    }

    // Update is called once per frame
    private void FixedUpdate() {
	
        foreach (Rigidbody body in affectedBodies)
        {
            //Angle++;

            Vector3 directionToTor = (transform.position - body.position).normalized;

            float distance = (transform.position - body.position).magnitude;
            float strength = ForceG * body.mass * componentRigidbody.mass / (distance * distance);

            body.AddForce(directionToTor * strength * BodyKoeff);
            //body.AddForce(0f, 1 + Mathf.Sin(Time.time), 0f);
            //body.transform.RotateAroundLocal(Vector3.zero);

            //body.transform.position = new Vector3(Mathf.Cos(Angle) + body.position.x, 0f, Mathf.Sin(Angle) + body.position.z);
            body.transform.RotateAround(Vector3.zero, Vector3.down, 10 * OrbitSpeed * Time.deltaTime);

            //body.localPosition = vector3.forward + vector3.lerp(vector3.forward, -vector3.forward, mathf.repeat(Time.time * скорость, 1) * 0.5)
            //body.AddRelativeForce(Mathf.Cos(Angle) + body.position.x, transform.position.y, Mathf.Sin(Angle) + body.position.z);

            // Angle++;
            //X = cos(Angle) + StartCoord.x;
            //Z = sin(Angle) + StartCoord.z;


        }
    }
}

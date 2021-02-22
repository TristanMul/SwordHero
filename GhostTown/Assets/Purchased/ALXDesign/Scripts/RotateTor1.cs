using UnityEngine;

public class RotateTor1 : MonoBehaviour {

    public float TorRotationSpeed;
   
    void Update()
    {
        Quaternion rotation = Quaternion.AngleAxis(TorRotationSpeed * Time.deltaTime, Vector3.down);
        Quaternion rotat = Quaternion.AngleAxis(TorRotationSpeed * Time.deltaTime, Vector3.down);

        transform.rotation *= rotation;
        transform.rotation *= rotat;
    }
}

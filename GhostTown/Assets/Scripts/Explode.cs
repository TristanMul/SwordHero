using RootMotion.Dynamics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public GameObject body, legL, legR, armL, armR;
    MuscleCollisionBroadcaster legLE, legRE, armLE, armRE;
    public GameObject calfL, calfR, footL, footR;
    public Enemy deleteBehaviour;
    public MuscleRemoveMode removeMuscleMode;
    public PuppetMaster puppetMaster;
    public GameObject rend;
    Material mat;
    public Texture matBoom;
    public GameObject NewHead;
    public GameObject oldHead;
    public Transform headPosition;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        legLE = legL.GetComponent<Rigidbody>().GetComponent<MuscleCollisionBroadcaster>();
        legRE = legR.GetComponent<Rigidbody>().GetComponent<MuscleCollisionBroadcaster>();
        armLE = armL.GetComponent<Rigidbody>().GetComponent<MuscleCollisionBroadcaster>();
        armRE = armR.GetComponent<Rigidbody>().GetComponent<MuscleCollisionBroadcaster>();
        mat = rend.GetComponent<Renderer>().material;
        //StartCoroutine(Kaboom(1f));
    }

    public IEnumerator Kaboom(float duration)
    {
        mat.mainTexture = matBoom;
        oldHead.transform.localScale = new Vector3(0, 0, 0);
        Vector3 rot = headPosition.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y + 180, rot.z);
        NewHead = Instantiate(NewHead,headPosition.position, Quaternion.Euler(rot));
        explosion = Instantiate(explosion, oldHead.transform.position, Quaternion.identity,oldHead.transform);
        //yield return new WaitForSeconds(duration);
        legLE.puppetMaster.RemoveMuscleRecursive(legLE.puppetMaster.muscles[legLE.muscleIndex].joint, true, true, removeMuscleMode);
        legRE.puppetMaster.RemoveMuscleRecursive(legRE.puppetMaster.muscles[legRE.muscleIndex].joint, true, true, removeMuscleMode);
        armLE.puppetMaster.RemoveMuscleRecursive(armLE.puppetMaster.muscles[armLE.muscleIndex].joint, true, true, removeMuscleMode);
        armRE.puppetMaster.RemoveMuscleRecursive(armRE.puppetMaster.muscles[armRE.muscleIndex].joint, true, true, removeMuscleMode);

        footL.transform.parent = calfL.transform;
        footR.transform.parent = calfR.transform;
        Destroy(footL.GetComponent<ConfigurableJoint>());
        Destroy(footR.GetComponent<ConfigurableJoint>());
        Destroy(footL.GetComponent<Rigidbody>());
        Destroy(footR.GetComponent<Rigidbody>());

        puppetMaster.state = PuppetMaster.State.Dead;
        Collider[] colliders = Physics.OverlapSphere(body.transform.position, 5f);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(5, body.transform.position + body.transform.up, 10f,0f,ForceMode.Impulse);
        }
        Destroy(deleteBehaviour);
        yield return null;
    }  
}

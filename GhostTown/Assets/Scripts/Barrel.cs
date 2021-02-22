using RootMotion.Dynamics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : Standards
{
    public GameObject explodeParticle;
    PuppetMaster puppetMaster;
    public GameObject textbox;
    public GameObject barrelRend;

    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public MobileKeyboard keyboard;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Destruct());
        keyboard = GameObject.FindGameObjectWithTag("Player").GetComponent<MobileKeyboard>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        Vector3 targetDir = target.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);

        if (angle < 90 && barrelRend.GetComponent<Renderer>() != null && textbox.GetComponent<KillText>() != null) {
            keyboard.BarrelReset(keyboard.lockedEnemy);
            Destroy(textbox.GetComponent<KillText>().killLabel);
            Destroy(gameObject);
        }
    }

    public IEnumerator Destruct()
    {
        gameObject.tag = "Untagged";
        Destroy(textbox.GetComponent<KillText>().killLabel);
        Destroy(textbox);

        //yield return new WaitForSeconds(0.3f);
        explodeParticle = Instantiate(explodeParticle, gameObject.transform.position, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 5f);
        foreach (Collider hit in colliders) {
            if (hit.tag == "Enemy") {
                StartCoroutine(hit.GetComponent<Explode>().Kaboom(0f));
                Destroy(hit.GetComponent<Enemy>().kT.killLabel);
                Destroy(hit.GetComponent<Enemy>().textBox);
                Destroy(hit.gameObject.GetComponent<CapsuleCollider>());
                Destroy(hit.GetComponent<Enemy>().eCamera);
            }
            if (hit)
            {
                if (hit.attachedRigidbody != null)
                hit.attachedRigidbody.AddExplosionForce(10, gameObject.transform.position, 50, 1f, ForceMode.Impulse);
            }
        }
        Destroy(barrelRend.GetComponent<Renderer>());
        yield return new WaitForSeconds(1f);
        Destroy(explodeParticle);
        Destroy(gameObject);
        yield return null;
    }

    public IEnumerator Death(MuscleCollisionBroadcaster broadcaster)
    {
        broadcaster.GetComponent<PuppetMaster>().state = PuppetMaster.State.Dead;
        yield return new WaitForSeconds(1f);
        broadcaster.GetComponent<PuppetMaster>().state = PuppetMaster.State.Alive;
    }
}

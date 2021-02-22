using RootMotion.Dynamics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : Standards
{

    public float delay = 2f;
    public MuscleRemoveMode removeMuscleMode;
    //public GameObject blast;
    MobileKeyboard keyboard;
    GameManager manager;
    void Awake() {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        keyboard = GameObject.FindGameObjectWithTag("Player").GetComponent<MobileKeyboard>();
        if (keyboard.lockedEnemy != null)
        {
            StartCoroutine(Destruct(CalcTimeDist(keyboard.lockedEnemy, manager.BallSpeed)));
        }
    }
    void Start()
    {
        
            
    }

    private IEnumerator Destruct(float dey)
    {
        yield return new WaitForSeconds(dey);

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 10)
        {
            //StartCoroutine(DestroyBodyPart(0.5f, collision));
            Destroy(gameObject);
        }
    }
    public IEnumerator DestroyBodyPart(float duration, Collision collision)
    {
        yield return new WaitForSeconds(duration);
        var destroyBody = collision.gameObject.GetComponent<MuscleCollisionBroadcaster>();
        destroyBody.puppetMaster.RemoveMuscleRecursive(destroyBody.puppetMaster.muscles[destroyBody.muscleIndex].joint, true, true, removeMuscleMode);
        var joint = destroyBody.GetComponent<ConfigurableJoint>();
        if (joint != null)
        {
            Destroy(joint);
            //destroyBody.GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * 2.0f, destroyBody.transform.position);
        }
        Destroy(gameObject);
    }
}

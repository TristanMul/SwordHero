using UnityEngine;
using System.Collections;
using RootMotion.Dynamics;

namespace RootMotion.Demos
{
    // Delayed destruction of the GameObject.
    public class Destructor : MonoBehaviour
    {

        public float delay = 2f;
        public MuscleRemoveMode removeMuscleMode;
        //public GameObject blast;

        void Start()
        {

            StartCoroutine(Destruct());
        }

        private IEnumerator Destruct()
        {
            yield return new WaitForSeconds(delay);

            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("bodypart"))
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
}

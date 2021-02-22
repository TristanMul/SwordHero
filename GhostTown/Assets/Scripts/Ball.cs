using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.Dynamics;

namespace RootMotion.Demos
{
    public class Ball : MonoBehaviour
    {

        public float rayDis;
        public PuppetMaster puppetMaster;
        public MuscleRemoveMode removeMuscleMode;
        public LayerMask layerMask;
        public float unpin = 10f;
        public float force = 10f;
        public ParticleSystem particles;

        private void Start()
        {
            particles = GameObject.Find("Bone Splinters").GetComponent<ParticleSystem>();
            Invoke("Destroy", 3);
        }

        public void Destroy()
        {
            Destroy(this.gameObject);
        }

        public Collider[] arrColliders;
        public float rad;

        private void Update()
        {
            //Debug.Log(puppetMaster.muscles.Length-1 + "V");

            arrColliders = Physics.OverlapSphere(this.transform.position, 0.13f, layerMask);

            foreach (Collider c in arrColliders)
            {
                if (arrColliders[0].transform.parent.name == "PuppetMaster")
                {
                    puppetMaster = arrColliders[0].transform.parent.GetComponent<PuppetMaster>();
                }

                Debug.Log(c.gameObject.name);
                var broadcaster = arrColliders[0].attachedRigidbody.GetComponent<MuscleCollisionBroadcaster>();

                // If is a muscle...
                if (broadcaster != null)
                {
                    broadcaster.Hit(unpin, transform.forward * force, transform.position);

                    // Remove the muscle and its children
                    if (puppetMaster != null)
                    {
                        if (broadcaster.muscleIndex <= puppetMaster.muscles.Length - 1)
                        {
                            if (puppetMaster.muscles.Length > 0)
                            {
                                broadcaster.puppetMaster.RemoveMuscleRecursive(broadcaster.puppetMaster.muscles[broadcaster.muscleIndex].joint, true, true, removeMuscleMode);
                                Debug.Log(broadcaster.muscleIndex + "V");
                            }
                        }
                    }
                }
                else
                {
                    // Not a muscle (any more)
                    //var joint = hit.collider.attachedRigidbody.GetComponent<ConfigurableJoint>();
                    //if (joint != null) Destroy(joint);

                    // Add force
                    arrColliders[0].attachedRigidbody.AddForceAtPosition(transform.forward * force, transform.position);
                }

                // Particle FX
                particles.transform.position = transform.position;
                particles.transform.rotation = Quaternion.LookRotation(-transform.position);
                particles.Emit(5);
            }
        }
    }
    
       


        //public void FindEnemy()
        //{
            //if (GameObject.FindGameObjectWithTag("Enemy"))
            //{
            //    puppetMaster = GameObject.Find("PuppetMaster").GetComponent<PuppetMaster>();
            //}
        //}
    //void FixedUpdate()
        //{
        //    if (puppetMaster == null)
        //    {
        //        FindEnemy();
        //    }
        //}

        //	RaycastHit hitF, hitB, hitR, hitL, hitU, hitD; ;
        //RaycastHit hit = new RaycastHit();

        //	if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitF, rayDis, layerMask))
        //	{
        //		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitF.distance, Color.yellow);
        //		Debug.Log("Did Hit");

        //		var broadcaster = hitF.collider.attachedRigidbody.GetComponent<MuscleCollisionBroadcaster>();

        //		// If is a muscle...
        //		if (broadcaster != null)
        //		{
        //			broadcaster.Hit(unpin, transform.forward * force, hitF.point);

        //			// Remove the muscle and its children
        //			broadcaster.puppetMaster.RemoveMuscleRecursive(broadcaster.puppetMaster.muscles[broadcaster.muscleIndex].joint, true, true, removeMuscleMode);
        //		}
        //		else
        //		{
        //			// Not a muscle (any more)
        //			//var joint = hit.collider.attachedRigidbody.GetComponent<ConfigurableJoint>();
        //			//if (joint != null) Destroy(joint);

        //			// Add force
        //			hitF.collider.attachedRigidbody.AddForceAtPosition(transform.forward * force, hitF.point);
        //		}

        //		// Particle FX
        //		particles.transform.position = hitF.point;
        //		particles.transform.rotation = Quaternion.LookRotation(-transform.position);
        //		particles.Emit(5);
        //	}
        //	else
        //	{
        //		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayDis, Color.white);
        //		Debug.Log("Did not Hit");
        //	}
        //	///
        //	if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hitB, rayDis, layerMask))
        //	{
        //		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * hitB.distance, Color.yellow);
        //		Debug.Log("Did Hit");


        //		var broadcaster = hitB.collider.attachedRigidbody.GetComponent<MuscleCollisionBroadcaster>();

        //		// If is a muscle...
        //		if (broadcaster != null)
        //		{
        //			broadcaster.Hit(unpin, transform.position * force, transform.position);

        //			// Remove the muscle and its children
        //			broadcaster.puppetMaster.RemoveMuscleRecursive(broadcaster.puppetMaster.muscles[broadcaster.muscleIndex].joint, true, true, removeMuscleMode);
        //		}
        //		else
        //		{
        //			// Not a muscle (any more)
        //			//var joint = hit.collider.attachedRigidbody.GetComponent<ConfigurableJoint>();
        //			//if (joint != null) Destroy(joint);

        //			// Add force
        //			hitB.collider.attachedRigidbody.AddForceAtPosition(transform.position * force, transform.position);
        //		}

        //		// Particle FX
        //		particles.transform.position = hitB.point;
        //		particles.transform.rotation = Quaternion.LookRotation(-transform.position);
        //		particles.Emit(5);
        //	}
        //	else
        //	{
        //		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * rayDis, Color.white);
        //		Debug.Log("Did not Hit");
        //	}
        //	///
        //	if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hitR, rayDis, layerMask))
        //	{
        //		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * hitR.distance, Color.yellow);
        //		Debug.Log("Did Hit");


        //		var broadcaster = hitR.collider.attachedRigidbody.GetComponent<MuscleCollisionBroadcaster>();

        //		// If is a muscle...
        //		if (broadcaster != null)
        //		{
        //			broadcaster.Hit(unpin, transform.position * force, transform.position);

        //			// Remove the muscle and its children
        //			broadcaster.puppetMaster.RemoveMuscleRecursive(broadcaster.puppetMaster.muscles[broadcaster.muscleIndex].joint, true, true, removeMuscleMode);
        //		}
        //		else
        //		{
        //			// Not a muscle (any more)
        //			//var joint = hit.collider.attachedRigidbody.GetComponent<ConfigurableJoint>();
        //			//if (joint != null) Destroy(joint);

        //			// Add force
        //			hitR.collider.attachedRigidbody.AddForceAtPosition(transform.position * force, transform.position);
        //		}

        //		// Particle FX
        //		particles.transform.position = hitR.point;
        //		particles.transform.rotation = Quaternion.LookRotation(-transform.position);
        //		particles.Emit(5);
        //	}
        //	else
        //	{
        //		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * rayDis, Color.white);
        //		Debug.Log("Did not Hit");
        //	}
        //	///
        //	if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hitL, rayDis, layerMask))
        //	{
        //		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * hitL.distance, Color.yellow);
        //		Debug.Log("Did Hit");


        //		var broadcaster = hitL.collider.attachedRigidbody.GetComponent<MuscleCollisionBroadcaster>();

        //		// If is a muscle...
        //		if (broadcaster != null)
        //		{
        //			broadcaster.Hit(unpin, transform.position * force, transform.position);

        //			// Remove the muscle and its children
        //			broadcaster.puppetMaster.RemoveMuscleRecursive(broadcaster.puppetMaster.muscles[broadcaster.muscleIndex].joint, true, true, removeMuscleMode);
        //		}
        //		else
        //		{
        //			// Not a muscle (any more)
        //			//var joint = hit.collider.attachedRigidbody.GetComponent<ConfigurableJoint>();
        //			//if (joint != null) Destroy(joint);

        //			// Add force
        //			hitL.collider.attachedRigidbody.AddForceAtPosition(transform.position * force, transform.position);
        //		}

        //		// Particle FX
        //		particles.transform.position = hitL.point;
        //		particles.transform.rotation = Quaternion.LookRotation(-transform.position);
        //		particles.Emit(5);
        //	}
        //	else
        //	{
        //		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * rayDis, Color.white);
        //		Debug.Log("Did not Hit");
        //	}

        //	////// Up
        //	if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hitU, rayDis, layerMask))
        //	{
        //		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hitU.distance, Color.yellow);
        //		Debug.Log("Did Hit");


        //		var broadcaster = hitU.collider.attachedRigidbody.GetComponent<MuscleCollisionBroadcaster>();

        //		// If is a muscle...
        //		if (broadcaster != null)
        //		{
        //			broadcaster.Hit(unpin, transform.position * force, transform.position);

        //			// Remove the muscle and its children this
        //			broadcaster.puppetMaster.RemoveMuscleRecursive(broadcaster.puppetMaster.muscles[broadcaster.muscleIndex].joint, true, true, removeMuscleMode);
        //		}
        //		else
        //		{
        //			// Not a muscle (any more)
        //			//var joint = hit.collider.attachedRigidbody.GetComponent<ConfigurableJoint>();
        //			//if (joint != null) Destroy(joint);

        //			// Add force
        //			hitU.collider.attachedRigidbody.AddForceAtPosition(transform.position * force, transform.position);
        //		}

        //		// Particle FX
        //		particles.transform.position = hitU.point;
        //		particles.transform.rotation = Quaternion.LookRotation(-transform.position);
        //		particles.Emit(5);
        //	}
        //	else
        //	{
        //		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * rayDis, Color.white);
        //		Debug.Log("Did not Hit");
        //	}

        //	////// Down

        //	if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hitD, rayDis, layerMask))
        //	{
        //		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hitD.distance, Color.yellow);
        //		Debug.Log("Did Hit");


        //		var broadcaster = hitD.collider.attachedRigidbody.GetComponent<MuscleCollisionBroadcaster>();

        //		// If is a muscle...
        //		if (broadcaster != null)
        //		{
        //			broadcaster.Hit(unpin, transform.position * force, transform.position);

        //			// Remove the muscle and its children
        //			broadcaster.puppetMaster.RemoveMuscleRecursive(broadcaster.puppetMaster.muscles[broadcaster.muscleIndex].joint, true, true, removeMuscleMode);
        //		}
        //		else
        //		{
        //			// Not a muscle (any more)
        //			//var joint = hit.collider.attachedRigidbody.GetComponent<ConfigurableJoint>();
        //			//if (joint != null) Destroy(joint);

        //			// Add force
        //			hitD.collider.attachedRigidbody.AddForceAtPosition(transform.position * force, transform.position);
        //		}

        //		// Particle FX
        //		particles.transform.position = hitD.point;
        //		particles.transform.rotation = Quaternion.LookRotation(-transform.position);
        //		particles.Emit(5);
        //	}
        //	else
        //	{
        //		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * rayDis, Color.white);
        //		Debug.Log("Did not Hit");
        //	}
        //}
}
using UnityEngine;
using System.Collections;
using RootMotion.Dynamics;

namespace RootMotion.Demos {
	
	public class SkeletonShooter2 : MonoBehaviour {

		public PuppetMaster puppetMaster;
		public Skeleton2 skeleton;
		public MuscleRemoveMode removeMuscleMode;
		public LayerMask layers;
		public float unpin = 10f;
		public float force = 10f;
		//public ParticleSystem particles;

		// Update is called once per frame
		void Update () {
			if (Input.GetMouseButtonDown(0)) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				
				// Raycast to find a ragdoll collider
				RaycastHit hit = new RaycastHit();
				if (Physics.Raycast(ray, out hit, 100f, layers)) {
					var broadcaster = hit.collider.attachedRigidbody.GetComponent<MuscleCollisionBroadcaster>();

					// If is a muscle...
					if (broadcaster != null) {
                        //broadcaster.Hit(unpin, ray.direction * force, hit.point);

                        // Remove the muscle and its children
                        broadcaster.puppetMaster.RemoveMuscleRecursive(broadcaster.puppetMaster.muscles[broadcaster.muscleIndex].joint, true, true, removeMuscleMode);
                        //broadcaster.puppetMaster.state = PuppetMaster.State.Frozen;
                        //var joint = hit.collider.attachedRigidbody.GetComponent<ConfigurableJoint>();
                        //if (joint != null) Destroy(joint);
                    }
                    else {
						// Not a muscle (any more)
						//var joint = hit.collider.attachedRigidbody.GetComponent<ConfigurableJoint>();
						//if (joint != null) Destroy(joint);

						// Add force
						hit.collider.attachedRigidbody.AddForceAtPosition(ray.direction * force, hit.point);
					}

					// Particle FX
					//particles.transform.position = hit.point;
					//particles.transform.rotation = Quaternion.LookRotation(-ray.direction);
					//particles.Emit(5);
				}
			}

			// Reattach all the missing muscles
			if (Input.GetKeyDown(KeyCode.R)) {
				puppetMaster.Rebuild();
				skeleton.OnRebuild();
			}
		}
	}
}
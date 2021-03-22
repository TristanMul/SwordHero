using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mesa
{
    public class Mob : MonoBehaviour
    {

        public Transform target;
        public float damping = 1;
        public SpriteRenderer sensor;
        public Color detectColor = Color.white;
        Color normalColor;
        public float sensorAlpha = 1;
        public float speed = 1;
        public ParticleSystem boundary;
        public Transform projectile;
        GameObject projectilePool;
        public float bulletSpeed = 1;

        Quaternion returnRot;

        void Start()
        {

            returnRot = transform.rotation;

            if (sensor)
            {
                normalColor = sensor.color;

                Color sensorColor = sensor.color;
                sensorColor.a = 0;
                sensor.color = sensorColor;
            }

            //animation random start
            int randomFrame = Random.Range(0, 70);
            Animator anim = transform.GetComponent<Animator>();
            //animator.Play("AnimationName", 1, (1f / total_frames_in_animation) * desired_frame);
            anim.Play("Mob_Idle", 0, (1f / 70) * randomFrame);
        }

        // Update is called once per frame
        void Update()
        {
            if (target)
            {
                //transform.LookAt(target);

                var lookPos = target.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);

                if (!boundary.gameObject.activeSelf)
                {
                    boundary.gameObject.SetActive(true);
                }

                if (sensor.color.a < sensorAlpha)
                {
                    Color alpha = sensor.color;
                    alpha.a += Time.deltaTime * speed;
                    sensor.color = alpha;
                }

                if (TargetAngle(target) < 45)
                {
                    sensor.color = detectColor;
                    if (!projectilePool)
                    {
                        Shoot();
                    }
                }
                else
                {
                    sensor.color = normalColor;
                }
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, returnRot, Time.deltaTime);

                if (sensor.color.a > 0)
                {
                    Color alpha = sensor.color;
                    alpha.a -= Time.deltaTime * speed;
                    sensor.color = alpha;
                }
                else
                {
                    if (boundary.gameObject.activeSelf)
                    {
                        boundary.gameObject.SetActive(false);
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (GetNameTag(other.transform) == "Man2")
            {
                target = other.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (GetNameTag(other.transform) == "Man2")
            {
                target = null;
            }
        }

        void Shoot()
        {
            Vector3 targetDir = target.transform.position - transform.position;
            Transform bullet = Instantiate(projectile, transform.position + (transform.forward * 1) + (transform.up * 2), transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce((targetDir + transform.up) * bulletSpeed, ForceMode.Force);

            if (!projectilePool)
            {
                projectilePool = bullet.gameObject;
            }
        }

        public float TargetAngle(Transform _target)
        {
            Vector3 targetDir = _target.position - transform.position;
            float angle = Vector3.Angle(targetDir, transform.forward);
            return angle;

        }

        public string GetNameTag(Transform hit)
        {
            string hitName = "";
            
                string targetStr = hit.transform.name;
                targetStr = targetStr.Substring(0, 4);
                //print("targetStr = " + targetStr);
                hitName = targetStr;
            
            return hitName;
        }
    }
}

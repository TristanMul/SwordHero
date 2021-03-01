using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mesa
{

    public class Shooter : MonoBehaviour
    {
        public enum WeaponType
        {
            bullet = 0, kick = 1
        }
        
        public WeaponType attackType = WeaponType.bullet;
        public Transform projectile;
        public float offfset = 0;
        public float power = 1;
        public ForceMode mode = ForceMode.Force;

        Vector3 dbgPos;

        void Start()
        {

        }


        void Update()
        {
            //Debug.DrawLine(transform.position, transform.position + (dbgPos * 100));
            //Debug.DrawRay(transform.position, transform.forward * 5, Color.red);
            Debug.DrawRay(transform.position, transform.forward * offfset, Color.red);

            //touch
            if (Input.GetMouseButtonDown(0) || (Input.touchCount == 1))
            {
                if (attackType == WeaponType.bullet)
                {
                    Shoot();
                }
                else if (attackType == WeaponType.kick)
                {
                    Kick();
                }
            }
        }

        IEnumerator ScaleMe(Transform objTr)
        {
            objTr.localScale *= 1.2f;
            yield return new WaitForSeconds(0.5f);
            objTr.localScale /= 1.2f;
        }



        public void ShootBT()
        {
            GameObject bullet;
            bullet = Instantiate(projectile.gameObject, transform.position + transform.forward * offfset, transform.rotation);
            //Rigidbody rig = bullet.GetComponent<Rigidbody>();

            //Vector3 shootDir = (transform.forward * 5 - transform.position).normalized;
            Vector3 shootDir = transform.forward;
            bullet.GetComponent<Mesa.Bullet>().Setup(shootDir);
        }

        public void Shoot()
        {
            Transform bullet = Instantiate(projectile, transform.position + (transform.forward * 1) + (transform.up * 1), transform.rotation);
            //Vector3 shootDir = transform.forward;
            //bullet.GetComponent<Bullet>().Setup(shootDir);
            bullet.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up) * power, mode);
        }

        public void Kick()
        {
            Animator anim = transform.GetComponent<Animator>();
            anim.SetTrigger("Kick");
        }

        public string GetNameTag()
        {
            string hitName = "";
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                string targetStr = hit.transform.name;
                targetStr = targetStr.Substring(0, 3);
                //print("targetStr = " + targetStr);
                hitName = targetStr;
            }
            return hitName;
        }

        public void ClickHitName()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    StartCoroutine(ScaleMe(hit.transform));
                    Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mesa
{

    public class Bullet : MonoBehaviour
    {
        public bool privateMove = false;
        private Vector3 shootDir;
        public float moveSpeed = 10;
        public float destorySec = 5.0f;
        public ParticleSystem exp;

        public void Setup(Vector3 _shootDir)
        {
            shootDir = _shootDir;
        }

        void Update()
        {

            if (privateMove)
            {
                transform.position += shootDir * moveSpeed * Time.deltaTime;
            }

            shootDir = new Vector3(0, 0, 2);    // block for consol message 

            DestoryCount();
        }

        private void OnCollisionEnter(Collision collision)
        {
            //Destroy(collision.gameObject);
            if (exp)
            {
                Instantiate(exp, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }

        public void DestoryCount()
        {
            if (destorySec > 0)
            {
                destorySec -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}

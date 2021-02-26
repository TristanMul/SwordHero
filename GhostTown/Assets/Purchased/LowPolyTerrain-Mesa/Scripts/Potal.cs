using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mesa
{
    public class Potal : MonoBehaviour
    {

        Potal pt;
        //public List<Transform> spots = new List<Transform>(); 
        public Transform linkSpot;
        public ParticleSystem fx;
        bool goingOn = false;

        void Start()
        {
            if (linkSpot) pt = linkSpot.GetComponent<Potal>();
        }

        private void OnTriggerEnter(Collider other)
        {
            //pt.goingOn = true;
            //print("pt " + pt.gameObject.name );

            if (fx)
            {
                fx.transform.gameObject.SetActive(false);

                var emission = fx.emission;
                ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[emission.burstCount];
                emission.GetBursts(bursts);

                var main = fx.main;
                bursts[0].minCount = bursts[0].maxCount = 100;

                emission.SetBursts(bursts);
                fx.transform.gameObject.SetActive(true);
            }

            if (!goingOn)
            {
                pt.goingOn = true;
                other.transform.position = linkSpot.position;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (goingOn)
            {
                goingOn = false;
            }

            var emission = fx.emission;
            ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[emission.burstCount];
            emission.GetBursts(bursts);

            var main = fx.main;
            bursts[0].minCount = bursts[0].maxCount = 0;

            emission.SetBursts(bursts);
        }
    }
}

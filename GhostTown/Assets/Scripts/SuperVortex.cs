using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperVortex : MonoBehaviour
{
    public GameObject enemy;
    public GameObject vortex;
    public bool killTrigger;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.localScale = new Vector3(vortex.transform.localScale.x *10 + 100, 100, vortex.transform.localScale.z *100 + 100);
    }

    //private void OnTriggerEnter(Collider obj)
    //{
    //    Debug.Log("Destroy");
    //    if (killTrigger)
    //    {
    //        enemy.GetComponent<EnemyFollow>().StartKillGhost();
    //        Debug.Log("kill");
    //    }
    //}
}

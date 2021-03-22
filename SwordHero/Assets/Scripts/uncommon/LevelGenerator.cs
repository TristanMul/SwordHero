using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    //public List<GameObject> platform = new List<GameObject>();
    //public List<float> height = new List<float>();

    //private int rndRange = 0;
    //private float lastPos = 0;
    //private float lastScale = 0;

    //public void RandomGenerator()
    //{
    //    rndRange = Random.Range(0, platform.Count);

    //    for (int i = 0; i < platform.Count; i++)
    //    {
    //        CreateLevelObject(platform[i], height[i], i);
    //    }
    //}

    //public GameObject finishGroundPrefab;
    //public Vector3 pos;
    //public float offset;
    //public GameObject go;

    //public void CreateLevelObject(GameObject obj, float height, int value)
    //{
    //    if (rndRange == value)
    //    {
    //        GameObject go = Instantiate(obj) as GameObject;

    //        offset = lastPos + (lastScale * 0.5f);
    //        offset += (go.transform.localScale.z) * 0.5f;
    //        pos = new Vector3(0, height, offset);

    //        go.transform.position = pos;

    //        lastPos = go.transform.position.z;
    //        lastScale = go.transform.localScale.z;

    //        go.transform.parent = this.transform;

    //        Invoke("FinalGround", 1);
    //    }
    //}

    //public bool final;
    //public void FinalGround()
    //{
    //    if (final == false)
    //    {
    //        //GameObject goo = Instantiate(finishGroundPrefab) as GameObject;
    //        //offset += (goo.transform.localScale.z) * 0.5f;
    //        //goo.transform.position = pos;
    //        //goo.transform.parent = this.transform;

    //        GameObject go = Instantiate(finishGroundPrefab) as GameObject;

    //        offset = lastPos + (lastScale * 0.5f);
    //        offset += (go.transform.localScale.z) * 0.5f;
    //        pos = new Vector3(0, -10f, offset);

    //        go.transform.position = pos;

    //        lastPos = go.transform.position.z;
    //        lastScale = go.transform.localScale.z;

    //        go.transform.parent = this.transform;

    //        final = true;

    //    }
    //}
}

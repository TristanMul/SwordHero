using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCol : Standards
{
    public List<GameObject> test = new List<GameObject>();
    public GameObject canv;
    // Start is called before the first frame update
    void Start()
    {
        canv = GameObject.FindGameObjectWithTag("thecanvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (test.Count > 1)
        {
            for (int i = 0; i < test.Count; i++)
            {
                test[i].transform.localPosition = new Vector3(test[i].transform.position.x, test[i].transform.position.y * (i * 200), test[i].transform.position.z);
            }
        }
    }

    public void FindBorders()
    {
        test.Clear();
        Debug.Log("it got here");
        //GetChildObjects(canv, "border");
        //if (test.Count > 1) {
        //    for (int i = 0; i < test.Count; i++)
        //    {
        //        test[i].transform.localPosition = new Vector3(test[i].transform.position.x, test[i].transform.position.y*i, test[i].transform.position.z);
        //    }
        //}
    }
    public GameObject[] FindSecondClosest(string tag, GameObject pos)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        GameObject secondClosest = null;
        float distance = Mathf.Infinity;
        float position = pos.transform.position.y;
        foreach (GameObject go in gos)
        {
            float diff = go.transform.position.y - position;
            if (diff < distance)
            {
                secondClosest = closest;
                closest = go;
                distance = diff;
            }
        }
        return new GameObject[] { closest, secondClosest };
    }
    public void GetChildObjects(GameObject parent, string _tag)
    {
        Transform t = parent.transform;
        Debug.Log(t.childCount);
        for (int i = 0; i < t.childCount; i++)
        {
            Transform child = t.GetChild(i);
            if (child.tag == _tag)
            {
                test.Add(child.gameObject);
            }
            //if (child.childCount > 0)
            //{
            //    GetChildObjects(parent, _tag);
            //}
        }
    }
}

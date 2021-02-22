using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[System.Serializable]
public class Standards : MonoBehaviour
{    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject FindClosestEnemy(string tag, GameObject pos)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = pos.transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    public GameObject FindClosestEnemyScript()
    {
        Enemy[] gos;
        gos = FindObjectsOfType<Enemy>();
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (Enemy go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go.gameObject;
                distance = curDistance;
            }
        }
        return closest;
    }
    public float CalcTimeDist(GameObject part, float speed)
    {
        //var enemySpeed = gameManager.EnemySpeed;
        var Distance = Vector3.Distance(part.transform.position, gameObject.transform.position);
        var TimeBetweenObjects = Distance / speed;
        //var timeBet = Distance - (enemySpeed * TimeBetweenObjects);
        return TimeBetweenObjects;
    }

    public static GameObject FindGameObjectInChildWithTag(GameObject parent, string tag)
    {
        if (parent != null)
        {
            Transform t = parent.transform;

            for (int i = 0; i < t.childCount; i++)
            {
                if (t.GetChild(i).gameObject.tag == tag)
                {
                    return t.GetChild(i).gameObject;
                }

            }
        }

        return null;
    }

    public static GameObject FindParentWithTag(GameObject childObject, string tag)
    {
        Transform t = childObject.transform;
        while (t.parent != null)
        {
            if (t.parent.tag == tag)
            {
                return t.parent.gameObject;
            }
            t = t.parent.transform;
        }
        return null; // Could not find a parent with given tag.
    }

    public static GameObject FindRandomGameObjectInChild(GameObject parent, string tag)
    {
        if (parent != null)
        {
            Transform t = parent.transform;
            
            Transform[] all = t.GetComponentsInChildren<Transform>();
            all = all.Where(child => child.tag == tag).ToArray();
            int r = UnityEngine.Random.Range(0, all.Length);
            //Debug.Log(all.Length);
            for (int i = 0; i < all.Length; i++)
            {
                if (i == r)
                {
                    var tis = all[i].gameObject;
                    return tis;
                }

            }
        }

        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghostspawner : MonoBehaviour
{
    public GameObject enemy;
    public int mobWidth;
    public int mobLength;
    public int gapSize;

    void Start()
    {
        Grid();
    }
    
    void Grid()
    {
        GameObject refMob = (GameObject)Instantiate(enemy);

        for (int i = 0; i < mobLength; i++)
        {
            for (int j = 0; j < mobWidth; j++)
            {
                GameObject mob = (GameObject)Instantiate(refMob, transform);
                float x = i * gapSize;
                float y = j * -gapSize;
                mob.transform.position = new Vector3(x,55 ,y);
            }
        }

        Destroy(refMob);
        Debug.Log(transform.position);
        float gridW = mobWidth * gapSize;
        float gridH = mobLength * gapSize;
        transform.position = new Vector3(0, 10, 10);
    }
}

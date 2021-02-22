using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemys;
    public TextCol textc;

    void Start()
    {
        //textc = GameObject.Find("GameManager").GetComponent<TextCol>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < enemys.Length; i++)
            {
                if (enemys.Length > 0)
                {
                    enemys[i].SetActive(true);
                }    
            }
            //textc.FindBorders();
        } 
    }
}

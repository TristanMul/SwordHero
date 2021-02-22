using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyer : Standards
{
    public EnemySpawner spawner;
    public bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!done) {
                for (int i = 0; i < spawner.enemys.Length; i++)
                {
                    if (spawner.enemys.Length > 0)
                    {
                        if (spawner.enemys[i] != null) {
                            if (spawner.enemys[i].tag == "Enemy") {
                                //Destroy(FindGameObjectInChildWithTag(spawner.enemys[i], "textboxes").GetComponent<KillText>().killLabel);
                                spawner.enemys[i].SetActive(false);
                                Destroy(spawner.enemys[i]);
                            }
                            if (spawner.enemys[i].tag == "barrel") {
                                Destroy(FindGameObjectInChildWithTag(FindGameObjectInChildWithTag(spawner.enemys[i], "Enemy"),"textboxes").GetComponent<KillText>().killLabel);
                                spawner.enemys[i].SetActive(false);
                                Destroy(spawner.enemys[i]);

                            }
                        }
                    }
                }
                done = true;
            } 
        }
    }
}

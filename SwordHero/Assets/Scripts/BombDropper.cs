using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDropper : MonoBehaviour
{
    public GameObject bomb;
    private bool drop;
    GameObject[] bombs;

    // Start is called before the first frame update
    void Start()
    {
        drop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            drop = true;
        }

        if (drop)
        {
            StartCoroutine(DropBomb(0.5f));
        }
    }

    IEnumerator DropBomb(float timeToWait)
    {
        foreach (Transform _bomb in transform)
        {
            _bomb.GetComponent<Transform>().position = new Vector3(10, 0, 0);
            Instantiate(bomb, _bomb.transform);
            yield return new WaitForSeconds(timeToWait);
        }
        drop = false;
    }
}

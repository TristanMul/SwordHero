using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressToStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Time.timeScale = 1;
            this.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorLerp : MonoBehaviour
{
    public Color white;
    public Color transparent;
    Color colorChanger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        colorChanger = Color.Lerp(white, transparent, Mathf.PingPong(Time.time, 1));
        GetComponent<SpriteRenderer>().color = colorChanger;
    }
}

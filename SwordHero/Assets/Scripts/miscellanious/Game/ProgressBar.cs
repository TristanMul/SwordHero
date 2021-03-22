using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [HideInInspector]
    private Slider slider;
    public float fillSpeed;
    private float targetProgress;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Progress(0.5f);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetBar();
        }

        if(slider.value < targetProgress)
        {
            slider.value += fillSpeed * Time.deltaTime;
        }
    }

    void Progress(float newProgress)
    {
        targetProgress = slider.value + newProgress;
    }

    void ResetBar()
    {
        slider.value = 0.0f;
        Progress(0.0f);
    }
}

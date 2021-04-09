using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFinishPortal : MonoBehaviour
{
    
    void Start()
    {
        GameManager.onAllenemiesDefeated += ActivatePortal;
    }

    private void OnDisable() {
        GameManager.onAllenemiesDefeated -= ActivatePortal;
    }

    private void ActivatePortal(){
        transform.GetChild(0).gameObject.SetActive(true);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private Gradient gradient;

    // Update the healthbar to show current health percentage. 
    public void UpdateHealthBar(float currentHealth, float maxHealth){
        float healthPercentage = currentHealth / maxHealth;

        if(healthPercentage == 1f){
            GetComponent<Canvas>().enabled = false;
        }
        else{
            GetComponent<Canvas>().enabled = true;
            fillImage.fillAmount = healthPercentage;
            fillImage.color = gradient.Evaluate(healthPercentage);
        }
        
    }
}

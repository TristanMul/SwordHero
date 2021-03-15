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

        // Don't show healthbar on full health.
        if(healthPercentage == 1f){
            GetComponent<Canvas>().enabled = false;
        }
        // Show healthbar when not on full health.
        else{
            GetComponent<Canvas>().enabled = true;
            fillImage.fillAmount = healthPercentage;                // Fill healthbar based on health percentage.
            fillImage.color = gradient.Evaluate(healthPercentage);  // Color healthbar based on health percentage.
        }
        
    }
}

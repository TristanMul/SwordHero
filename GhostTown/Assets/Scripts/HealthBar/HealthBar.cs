using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage;

    // Update the healthbar to show current health percentage. 
    public void UpdateHealthBar(float currentHealth, float maxHealth){
        fillImage.fillAmount = currentHealth / maxHealth;
    }

}

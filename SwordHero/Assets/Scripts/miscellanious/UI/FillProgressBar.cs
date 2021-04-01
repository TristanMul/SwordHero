using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillProgressBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    List<GameObject> allEnemies = new List<GameObject>();
    [SerializeField] private Text enemiesKilled;
    int maxEnemies;
    int currentEnemies;
    private void Start()
    {
        allEnemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        maxEnemies = allEnemies.Count;
        enemiesKilled.text = "0/" + maxEnemies + " Enemies killed";
    }
    public void UpdateProgressBar()
    {
        currentEnemies = allEnemies.Count;
        float Progress = 1 - ((float)currentEnemies / (float)maxEnemies);
        enemiesKilled.text = maxEnemies - currentEnemies + "/" + maxEnemies + " Enemies killed";
            fillImage.fillAmount = Progress;                // Fill healthbar based on health percentage.


    }
    public void Remove(GameObject enemy)
    {
        allEnemies.Remove(enemy);
    }
}

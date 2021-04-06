using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateCoins : MonoBehaviour
{
    [SerializeField] private Text amountOfCoins;

    public void UpdateCoinAmount()
    {
        amountOfCoins.text = PlayerPrefs.GetInt("Coins").ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public static int totalLevels = 3;

    private void Start()
    {
        //Disable at launch!!!
        PlayerPrefs.DeleteAll();

        if (!PlayerPrefs.HasKey("level"))
            SceneManager.LoadScene("0");

        if (!PlayerPrefs.HasKey("leveltext"))
            PlayerPrefs.SetInt("leveltext", 0);

        if (!PlayerPrefs.HasKey("coins"))
            PlayerPrefs.SetInt("coins", 0);

        if (PlayerPrefs.GetInt("level") > totalLevels)
            PlayerPrefs.SetInt("level", 1);

        SceneManager.LoadScene(PlayerPrefs.GetInt("level").ToString());
    }
}

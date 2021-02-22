using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void MoreGames()
    {
        Application.OpenURL("Your Url");
    }

    public void RateUs()
    {
        Application.OpenURL("Your Url");
    }

    public void PrivacyPolicy()
    {
        Application.OpenURL("Your Url");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}

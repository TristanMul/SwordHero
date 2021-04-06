using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{

    [SerializeField] private RectTransform levelCompletePanel;
    [SerializeField] private Text UItext;

    private void Start() {
        UpdateLevelCompleteText();
    }

    public void UpdateLevelCompleteText(){
        levelCompletePanel.gameObject.SetActive(true);
        UItext.text = "LEVEL " + PlayerPrefs.GetInt("leveltext").ToString() + " Complete";
    }

    public void LoadNextScene(){
        // Increase level.
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        PlayerPrefs.SetInt("leveltext", PlayerPrefs.GetInt("leveltext") + 1);

        // Loop the levels.
        if (PlayerPrefs.GetInt("level") > Loader.totalLevels)
            PlayerPrefs.SetInt("level", 1);

        // Go to next level.
        SceneManager.LoadScene(PlayerPrefs.GetInt("level").ToString());
    }
}

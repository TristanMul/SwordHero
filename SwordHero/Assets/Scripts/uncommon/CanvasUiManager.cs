using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasUiManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public int currentLevelNo;
    public Text thisLevelNo;

    private void Awake()
    {
        Time.timeScale = 1;
        currentLevelNo = PlayerPrefs.GetInt("LevelNo", 1);
        thisLevelNo.text = "Level " + currentLevelNo.ToString();
    }

    public void Next()
    {
        Time.timeScale = 1;

        PlayerPrefs.SetInt("LevelNo", currentLevelNo + 1);
      
        Debug.Log(currentLevelNo);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RetryGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Home()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

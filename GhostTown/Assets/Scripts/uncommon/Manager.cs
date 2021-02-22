using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Manager : MonoBehaviour
{
    public int levelCount;
    public LevelGenerator levelGenerator = null;
    private static Manager s_Instance;
    public int currentLevelNo;
    public GameObject[] groundsToAdd;

    private void Start()
    {        
        Time.timeScale = 1;

        currentLevelNo = SceneManager.GetActiveScene().buildIndex;

        //Debug.Log(currentLevelNo);


        if (currentLevelNo < 4)
        {
            levelCount = 2;
        }
        else if (currentLevelNo < 7)
        {
            levelCount = 3;
        }
        else if (currentLevelNo < 10)
        {
            levelCount = 4;
         //   levelGenerator.platform.Add(groundsToAdd[0]);
        }
        else if (currentLevelNo < 13)
        {
            levelCount = 5;
        }
        else if (currentLevelNo < 16)
        {
            levelCount = 6;
        }
        else
        {
            levelCount = 8;
        }

        //for (int i = 0; i < levelCount; i++)
        //{
        //    levelGenerator.RandomGenerator();
        //} 
    }

    public void Update()
    {
        //if(Input.GetKeyDown(KeyCode.P))
        //{
        //    PlayerPrefs.DeleteAll();
        //}
    }

    //public void UpdateDistanceCount()
    //{
    //    levelGenerator.RandomGenerator();
    //}

    public void Next()
    {
        //Time.timeScale = 1;

        //currentLevelNo = PlayerPrefs.GetInt("LevelNo", currentLevelNo);

        //PlayerPrefs.SetInt("LevelNo", currentLevelNo + 1);
        //currentLevelNo = currentLevelNo + 1;
        //Debug.Log(currentLevelNo);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RetryGame()
    {
    //    Time.timeScale = 1;
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayGame()
    {
      //  Time.timeScale = 1;
    }

    public void PrivacyPolicy()
    {
     //   Application.OpenURL("https://sites.google.com/view/thamadako/beranda?authuser=0");
    }
}

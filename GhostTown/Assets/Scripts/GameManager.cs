using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private string sceneName;
    public float CameraSpeed;
    public float AnimationSpeed;
    public float PlayerSpeed;
    public int FontSize;
    [HideInInspector] public GameObject _player;
    [HideInInspector]
    public GameObject _enemy;
    public GameObject pressToStart;
    public GameObject gameOverUI;
    public GameObject finishLine;
    public int _speed;
    public bool playerAlive;
    public float vortexHangTime;
    public float vortexBuildup;
    public bool isTouchingFinish;
    public bool gameReset;
    bool gameOver = false;
    GameObject[] enemies;
    Scene currScene;

    private void Awake()
    {
        currScene = SceneManager.GetActiveScene();
        sceneName = currScene.name;

        gameOverUI.SetActive(false);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (gameOver)
        {
            if (Input.GetMouseButtonUp(0))
            {
                SceneManager.LoadScene(currScene.name);
                gameOver = false;
                gameReset = true;
            }
        }

        if (!playerAlive)
        {
            StartCoroutine(GameOver());
            if (Input.GetMouseButtonUp(0))
            {
                SceneManager.LoadScene(currScene.name);
            }
            //StartCoroutine(ReloadSameScene());
            
        }

        if (gameReset)
        {
            if (Input.GetMouseButtonUp(0))
            {
                SceneManager.LoadScene(currScene.name);
            }
            StartCoroutine(StartScene());
        }
    }

    public IEnumerator StartScene()
    {
        pressToStart.SetActive(true);
        gameOverUI.SetActive(false);
        //Time.timeScale = 0;
        gameReset = true;
        yield return null;
    }

    public IEnumerator GameOver()
    {
        gameOverUI.SetActive(true);
        pressToStart.SetActive(false);
        /*Time.timeScale = 0;
        yield return new WaitForSeconds(1);
        Time.timeScale = 1;*/
        yield return null;
    }

    public IEnumerator PlayerFinished()
    {
        //Play animation
        yield return null;
    }
}
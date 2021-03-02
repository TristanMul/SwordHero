using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private string sceneName;
    public float EnemySpeed;
    public float BallSpeed;
    public float CameraSpeed;
    public float AnimationSpeed;
    public float PlayerSpeed;
    public int FontSize;
    [HideInInspector]
    public GameObject _player;
    [HideInInspector]
    public GameObject _enemy;
    public GameObject pressToStart;
    public GameObject gameOverUI;
    public GameObject finishedUI;
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
        finishedUI.SetActive(false);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        Debug.Log(sceneName);
        _player = GameObject.FindGameObjectWithTag("Player");
        _enemy = GameObject.FindGameObjectWithTag("Enemy");

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
            if (Input.GetMouseButtonUp(0))
            {
                SceneManager.LoadScene(currScene.name);
            }
            //StartCoroutine(ReloadSameScene());
            StartCoroutine(GameOver());
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

    //public IEnumerator ReloadSameScene()
    //{
    //    gameOverUI.SetActive(true);
    //    yield return new WaitForSeconds(2);
    //    gameOver = true;
    //    Time.timeScale = 0;
    //}

    public IEnumerator StartScene()
    {
        pressToStart.SetActive(true);
        gameOverUI.SetActive(false);
        Time.timeScale = 0;
        gameReset = true;
        yield return null;
    }

    public IEnumerator GameOver()
    {
        gameOverUI.SetActive(true);
        pressToStart.SetActive(false);
        Time.timeScale = 0;
        yield return new WaitForSeconds(1);
        Time.timeScale = 1;
        yield return null;
    }

    public IEnumerator PlayerFinished()
    {
        //Play animation
        yield return null;
    }
}
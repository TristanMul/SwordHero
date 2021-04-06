using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public GameObject _enemy;
    [HideInInspector] public GameObject finishLine;
    [HideInInspector] public GameObject _player;

    public FillProgressBar progressBar;
    public static GameManager instance = null;
    private string sceneName;
    public float CameraSpeed;
    public float AnimationSpeed;
    public float PlayerSpeed;
    public int FontSize;
    public GameObject pressToStart;
    public GameObject gameOverUI;
    public int _speed;
    public bool playerAlive;
    public float vortexHangTime;
    public float vortexBuildup;
    public bool isTouchingFinish;
    public bool gameReset;
    bool gameOver = false;
    GameObject[] enemies;
    Scene currScene;

    void Awake()
    {
        finishLine = GameObject.FindWithTag("Finish");
        currScene = SceneManager.GetActiveScene();
        sceneName = currScene.name;
        progressBar = GameObject.Find("ProgressBar").GetComponent<FillProgressBar>();

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

        if (Input.GetKeyDown(KeyCode.Space))//test for slowtime
        {
            StartCoroutine(SlowTime(1f, .5f));
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


    /// <summary>
    /// Slows time -Lennart
    /// </summary>
    /// <param name="duration">The realtime duration of the time slow</param>
    /// <param name="magnitude">The timescale applied</param>
    /// <returns></returns>
    IEnumerator SlowTime(float duration, float magnitude)
    {
        Time.timeScale = magnitude;
        Time.fixedDeltaTime = magnitude * 0.02f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    public void TimeSlow(float duration, float magnitude)
    {
        StartCoroutine(SlowTime(duration, magnitude));
    }
}
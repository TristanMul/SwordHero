using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
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
    public GameObject gameOverUI;
    public int _speed;
    public bool playerAlive;
    public float vortexHangTime;
    public float vortexBuildup;
    bool gameOver;
    Scene currScene;

    private void Awake()
    {
        currScene = SceneManager.GetActiveScene();
        gameOverUI.SetActive(false);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (gameOver)
        {
            if (Input.GetMouseButtonUp(0))
            {
                SceneManager.LoadScene(currScene.name);
            }
        }

        //if (_player.GetComponentInChildren<Animator>().enabled == false)
        //{
        //    StartCoroutine(ReloadSameScene());
        //}
    }

    public IEnumerator ReloadSameScene()
    {
        gameOverUI.SetActive(true);
        yield return new WaitForSeconds(2);
        gameOver = true;
        Time.timeScale = 0;
    }
}
using RootMotion.Dynamics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Standards
{
    public GameObject body, enemy;
    float speed;
    float ogspeed;
    public int lives;
    [HideInInspector]
    public GameManager gameManager;
    [HideInInspector]
    public Transform target;
    public float distance;
    [HideInInspector]
    public RootMotion.Demos.Dying2 diy;
    public GameObject textBox;
    public KillText kT;
    public GameObject eCamera;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        diy = GetComponent<RootMotion.Demos.Dying2>();
        ogspeed = speed;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        speed = gameManager.EnemySpeed;
        this.GetComponent<Animator>().speed = gameManager.AnimationSpeed;
    }

    void Update()
    {
        if (body == null)
        {
            Destroy(enemy);
        }

        Vector3 targetPostition = new Vector3(target.position.x,this.transform.position.y,target.position.z);
        this.transform.LookAt(targetPostition);

        this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Destroyer"))
        {
            Destroy(enemy);
        }
        
    }

    public void IndexEnd()
    {
        if (lives <= 0)
        {
            StartCoroutine(Explode(CalcTimeDist(target.gameObject, gameManager.BallSpeed), gameObject));
            gameObject.tag = "Untagged";
            Destroy(kT.killLabel);
            Destroy(textBox);
            Destroy(gameObject.GetComponent<CapsuleCollider>());
        }
        else
        {
            StartCoroutine(Fall(3f));
            lives--;
            FindGameObjectInChildWithTag(gameObject, "textboxes").GetComponent<KillText>().ChooseWord();
        }
    }

    public IEnumerator Stop(float duration)
    {
        speed = 0f;
        this.GetComponent<Animator>().CrossFadeInFixedTime("Idle", 0.25f, -1, 0);
        this.GetComponent<Animator>().speed = gameManager.AnimationSpeed;
        yield return new WaitForSeconds(duration);
        speed = ogspeed;
        this.GetComponent<Animator>().CrossFadeInFixedTime("Walk", 0.25f, -1, 0);
        this.GetComponent<Animator>().speed = gameManager.AnimationSpeed;
    }
    public IEnumerator Fall(float duration)
    {
        speed = 0f;
        //this.GetComponent<Animator>().CrossFadeInFixedTime("DieBackwards", 0f, -1, 0);
        diy.Death();
        this.GetComponent<Animator>().speed = gameManager.AnimationSpeed*2;
        yield return new WaitForSeconds(duration);
        speed = ogspeed;
        diy.Revive();
        //this.GetComponent<Animator>().CrossFadeInFixedTime("Walk", 0f, -1, 0);
        this.GetComponent<Animator>().speed = gameManager.AnimationSpeed;
    }
    public IEnumerator Explode(float duration, GameObject closest)
    {
        var L = gameObject.GetComponent<Explode>();
        StartCoroutine(L.Kaboom(0f));
        StartCoroutine(CleanUp(3f, FindParentWithTag(gameObject, "destroyenemy")));
        yield break;
    }
    public IEnumerator CleanUp(float time, GameObject enemy)
    {
        yield return new WaitForSeconds(time);
        Destroy(enemy);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed;
    float ogspeed;
    public bool keyB;
    public Animation jumpAnim;
    public bool go;
    public Rigidbody rb;
    public GameManager gameManager;
    public MobileKeyboard keyboard;
    public float upForce, forwardForce;
    public ParticleSystem finishParticle1, finishParticle2;

    void Start()
    {
        keyB = true;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        keyboard = gameObject.GetComponent<MobileKeyboard>();
        speed = gameManager.PlayerSpeed;
        ogspeed = speed;
    }

    private void Update()
    {
        if (go)
        transform.position += new Vector3(0, 0, 1 * speed* Time.deltaTime);
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jump"))
        {
            rb.isKinematic = false;
            rb.velocity = new Vector3(0, 8, 0);
            //StartCoroutine(Wait(1.0f));
            //Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            this.GetComponent<AudioSource>().Play();

            rb.isKinematic = true;
            speed = 0;
            finishParticle1.Play();
            finishParticle2.Play();
            Invoke("Win", 2);
        }
        

        if (other.gameObject.CompareTag("JumpEnd"))
        {
            rb.isKinematic = true;
            this.transform.position = new Vector3(transform.position.x, 1.58f, transform.position.z);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Retry");
            KillText[] texts = FindObjectsOfType<KillText>();
            for (int i = 0; i < texts.Length; i++)
            {
                Destroy(texts[i].killLabel);
                Destroy(texts[i]);
            }
            keyB = false;
            keyboard.keyboard.active = false;
            Time.timeScale = 0;
            GameObject.FindObjectOfType<Canvas>().transform.Find("RetryPanel").gameObject.SetActive(true);
        }
    }

    public void Win()
    {
        KillText[] texts = FindObjectsOfType<KillText>();
        for (int i = 0; i < texts.Length; i++)
        {
            Destroy(texts[i].killLabel);
            Destroy(texts[i]);
        }
        keyB = false;
        keyboard.keyboard.active = false;
        GameObject.FindObjectOfType<Canvas>().transform.Find("WinPanel").gameObject.SetActive(true);
    }
    public IEnumerator JumpWait(float duration)
    {
        speed = 0;
        yield return new WaitForSeconds(duration);
        speed = ogspeed;
        rb.isKinematic = false;
        rb.velocity = new Vector3(0, 8, 0);
    }
    public IEnumerator Wait(float duration)
    {
        speed = 0;
        yield return new WaitForSeconds(duration);
        speed = ogspeed;
    }
}

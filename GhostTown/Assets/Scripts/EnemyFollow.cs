using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    GameManager gameManager;
    [HideInInspector]
    public GameObject target;
    public Transform enemy;
    public int speed;
    public float range;
    Vector3 killScale = new Vector3(0.1f, 0.1f, 0.1f);

    void Awake()
    {
         target = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        enemy = this.GetComponent<Transform>();
        StartCoroutine(LerpObject(killScale, enemy.localScale, enemy.position, enemy.position, 1.5f, false));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target.transform != null)
        {
            if ((Mathf.Abs(target.transform.position.z - enemy.position.z) < range) &&
                Mathf.Abs(target.transform.position.x - enemy.position.x) < range)
            {
                enemy.transform.LookAt(target.transform);
                enemy.position = Vector3.MoveTowards(enemy.position, target.transform.position, speed * Time.deltaTime);
            }
        }
        else
        {
            speed = 0;
        }
    }

    public IEnumerator LerpObject(Vector3 a, Vector3 b, Vector3 x, Vector3 y, float time, bool destroy)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * speed;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);
            transform.position = Vector3.Lerp(x, y, i);
            transform.GetComponent<Collider>().enabled = false;
            yield return null;
        }
        if (destroy) {
            target.GetComponent<VacuumPoof>().PlayPoofEffect();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(KillEnemy(other));
    }


    //On touch player
    IEnumerator KillEnemy(Collider other)
    {
        if (other.tag == "SuperVortex")
        {
            foreach (Collider c in GetComponents<Collider>())
            {
                c.enabled = false;
            }
            Debug.Log("collider disabled");
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(LerpObject(enemy.localScale, killScale, enemy.position, target.transform.position, 1.5f, true));
        }

        if (other.tag == "Player")
        {
            Debug.Log("kill me");
            StartCoroutine(target.GetComponent<PlayerMovement>().OnDeath());
        }
    }
}

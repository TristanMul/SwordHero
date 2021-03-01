using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    GameManager gameManager;
    [HideInInspector]
    public GameObject target;
    [HideInInspector]
    public GameObject cylinder;
    public Transform enemy;
    public int speed;
    public float range;
    private Material currentMat;
    Vector3 killScale = new Vector3(0.1f, 0.1f, 0.1f);

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        cylinder = GameObject.FindGameObjectWithTag("Cylinder");
        //StartCoroutine(LerpAlpha(1));
    }

    // Start is called before the first frame update
    void Start()
    {
        enemy = this.GetComponent<Transform>();
        currentMat = gameObject.GetComponentInChildren<Renderer>().material;
        var ghostRenderer = GetComponentInChildren<Renderer>().material.color;
        StartCoroutine(LerpAlpha(2f));
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

    public IEnumerator LerpObject(Vector3 scaleA, Vector3 scaleB, Vector3 posA, Vector3 posB, float time, bool destroy)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * speed;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(scaleA, scaleB, i);
            transform.position = Vector3.Lerp(posA, posB, i);
            transform.GetComponent<Collider>().enabled = false;
            yield return null;
        }
        if (destroy) {
            // Play the poof effect on the vacuum of the player and destroy this object.
            target.GetComponent<VacuumPoof>().PlayPoofEffect(GetComponent<TypeOfGhost>().color);
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
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(LerpObject(enemy.localScale, killScale, enemy.position, cylinder.transform.position, 1f, true));
            //Spawn Poof Effects Here
        }

        if (other.tag == "Player")
        {
            StartCoroutine(target.GetComponent<PlayerMovement>().OnDeath());
        }
    }

    public IEnumerator LerpAlpha(float time)
    {
        Color oldColor = currentMat.color;
        float i = 0.0f;
        float rate = (1.0f / time) * speed;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            
            Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, i);
            currentMat.SetColor("_Color", newColor);
            yield return null;
        }
    }
}

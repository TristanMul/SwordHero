using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathAnimation : MonoBehaviour
{
    private TextMeshProUGUI coinsText;
    public GameObject canvas;
    public float canvasMoveSpeed;
    private float textDisappearTime = .5f;

    /// <summary>
    /// Sets up the coins to be displayed
    /// </summary>
    /// <param name="amountOfCoins">The amount of coins gained</param>
    public void Setup(int amountOfCoins)
    {
        coinsText = canvas.GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(MoveObject());
        coinsText.text = "+" + amountOfCoins;
    }
    private void Start()
    {
        coinsText = canvas.GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(MoveObject());

    }
    // Update is called once per frame
    void Update()
    {
        Quaternion r1 = Quaternion.LookRotation(canvas.transform.position - Camera.main.transform.position, Vector3.up);
        Vector3 euler2 = canvas.transform.eulerAngles;
        canvas.transform.rotation = Quaternion.Euler(r1.eulerAngles.x, euler2.y, euler2.z);
        
        UpdateText();
    }


    void UpdateText()
    {
        Vector3 newLocation = canvas.transform.position;//Updates the location of the text
        newLocation.y += canvasMoveSpeed * coinsText.color.a;
        canvas.transform.position = newLocation;


        /*Color textColor = coinsText.material.color;
        textColor.a -= Time.deltaTime / textDisappearTime;
        coinsText.material.color = textColor;*/
        //if(coinsText.material.color.a < .1f) { Destroy(gameObject); }
    }

    IEnumerator MoveObject()
    {
        float elapsed = 0.0f;
        float duration = 10f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Color newColor = coinsText.color;
            newColor.a = Mathf.Lerp(newColor.a, 0f, elapsed / duration);
            coinsText.color = newColor;
            coinsText.material.color = newColor;
            yield return null;
        }
        Debug.Log("destroy");
        Destroy(gameObject);
    }
}

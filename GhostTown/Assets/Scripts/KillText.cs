using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillText : Standards
{
    [HideInInspector]
    public GameObject killLabel;
    public GameObject textPrefab;
    Canvas canv;
    public bool closest;
    public string findText;
    string previousText;
    GameObject stringList;
    GameManager gameManager;
    [HideInInspector]
    public Text killText;
    Image border;
    public Sprite yellowBorder;
    public Sprite redBorder;
    // Start is called before the first frame update
    void Start()
    {
        stringList = GameObject.FindGameObjectWithTag("levelmanager");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        var max = stringList.GetComponent<StringList>().killTextList.Count;
        var random = Random.Range(0, max);
        findText = stringList.GetComponent<StringList>().killTextList[random];
        stringList.GetComponent<StringList>().killTextList.Remove(findText);
        closest = false;

        GameObject tempObj = GameObject.FindGameObjectWithTag("thecanvas");
        if (tempObj != null) { canv = tempObj.GetComponent<Canvas>(); }
        killLabel = Instantiate(textPrefab, Vector3.zero, transform.rotation);        
        killLabel.transform.SetParent(canv.transform, false);
        var panel = FindGameObjectInChildWithTag(tempObj, "Box");
        Vector3 namePose = Camera.main.WorldToScreenPoint(this.transform.position);
        //killLabel.transform.position = namePose;
        HeightCheck();
        killLabel.transform.SetParent(panel.transform, false);
        border = FindGameObjectInChildWithTag(killLabel, "afterborder").GetComponent<Image>();

        var bg = FindGameObjectInChildWithTag(killLabel, "afterborder");
        killText = FindGameObjectInChildWithTag(bg,"killtext").GetComponent<Text>();
        killText.text = findText;
        killText.fontSize = gameManager.FontSize;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 namePose = Camera.main.WorldToScreenPoint(this.transform.position);
        killLabel.transform.position = new Vector3(namePose.x, killLabel.transform.position.y, namePose.z);
        //killLabel.transform.position = namePose;
    }
    public void HeightCheck()
    {
        Vector3 namePose = Camera.main.WorldToScreenPoint(this.transform.position);
        if (FindParentWithTag(FindParentWithTag(gameObject, "Enemy"), "barrel"))
        { killLabel.transform.position = new Vector3(namePose.x, Screen.height * 0.6f, namePose.z); }
        if (FindParentWithTag(FindParentWithTag(gameObject, "Enemy"), "destroyenemy"))
        { killLabel.transform.position = new Vector3(namePose.x, Screen.height * 0.7f, namePose.z); }
    }
    public void ClosestTrue()
    {
        closest = true;
    }
    public void ClosestFalse()
    {
        closest = false;
    }
    public void ZeroAlpha()
    {
        killText.canvasRenderer.SetAlpha(0.2f);
    }
    public void FullAlpha()
    {
        killText.canvasRenderer.SetAlpha(1f);
    }
    public void RedBorder()
    {
        border.sprite = redBorder;
    }
    public void YellowBorder()
    {
        border.sprite = yellowBorder;
    }

    public void ChooseWord()
    {
        var max = stringList.GetComponent<StringList>().killTextList.Count;
        var random = Random.Range(0, max);
        findText = stringList.GetComponent<StringList>().killTextList[random];
        stringList.GetComponent<StringList>().killTextList.Remove(findText);
        killText.text = findText;
    }
}

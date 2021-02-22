using RootMotion.Dynamics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class MobileKeyboard : Standards
{
    public TouchScreenKeyboard keyboard;
    public GameManager gameManager;
    public GameObject particle;
    public GameObject player;
    [HideInInspector]
    public bool lockOn;
    [HideInInspector]
    public GameObject lockedEnemy;
    public string textColor;
    public string emptyColor;

    int letterIndex;
    int colorIndex;
    string checkIt;
    string colorchangeLength;
    string colorchangeLength2;
    string previousKillLetter;
    string lockedText;
    GameObject fire;
    GameObject lockedObjectText;
    
    KillText killText;
    List<KillText> matchedEnemies = new List<KillText>();
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        letterIndex = 0;
        colorIndex = 0;
        fire = GameObject.FindGameObjectWithTag("gunpoint");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        colorchangeLength = "<color=#c0c0c0ff>";
        colorchangeLength2 = "</color>";
    }

    // Update is called once per frame
    void Update()
    {
        if (!TouchScreenKeyboard.visible && player.GetComponent<Player>().keyB)
        {
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.URL, false, false, false);
            TouchScreenKeyboard.hideInput = true;
        }
        if (keyboard.text.Length > 0 && Application.platform == RuntimePlatform.IPhonePlayer)
        {
            checkIt = keyboard.text;
            IOSNewMatch(checkIt);
            IOSEnemyMatched(checkIt);
            keyboard.text = "";
        }
        PcAndroidNewMatch();
        PcAndroidEnemyMatched();
    }

    public void FindTextBoxes(bool locke)
    {
        GameObject[] boxes;
        boxes = GameObject.FindGameObjectsWithTag("textboxes");
        foreach (GameObject go in boxes)
        {
            if (locke)
            {
                go.GetComponent<KillText>().ZeroAlpha();
                go.GetComponent<KillText>().YellowBorder();
            }
            else { go.GetComponent<KillText>().FullAlpha(); go.GetComponent<KillText>().YellowBorder(); }
        }
    }

    public void IOSNewMatch(string checkString)
    {
        if (!lockOn)
        {
            checkString = checkString.Substring(0, 1);
            foreach (char c in checkString)
            {
                KillText[] texts = FindObjectsOfType<KillText>();
                for (int i = 0; i < texts.Length; i++)
                {
                    char s = texts[i].findText[0];
                    if (s == c)
                    {
                        matchedEnemies.Add(texts[i]);
                    }
                }
                if (matchedEnemies.Count > 0)
                {
                    KillText distMatch = null;
                    float distance = Mathf.Infinity;
                    Vector3 position = transform.position;
                    foreach (KillText go in matchedEnemies)
                    {
                        Vector3 diff = go.transform.position - position;
                        float curDistance = diff.sqrMagnitude;
                        if (curDistance < distance)
                        {
                            distMatch = go;
                            distance = curDistance;
                        }
                    }
                    FindTextBoxes(true);
                    distMatch.FullAlpha();
                    distMatch.RedBorder();
                    killText = distMatch;
                    lockedText = distMatch.findText.Replace(" ", string.Empty);
                    lockedObjectText = distMatch.gameObject;
                    lockedEnemy = FindParentWithTag(lockedObjectText, "Enemy");
                    lockOn = true;
                    matchedEnemies.Clear();
                    break;
                }
            }
        }
    }

    public void IOSEnemyMatched(string checkString)
    {
        GameObject bodyPart = FindRandomGameObjectInChild(lockedEnemy, "bodypart");
        if (lockOn)
        {
            checkString = checkString.Substring(0, 1);
            foreach (char c in checkString)
            {
                if (letterIndex < lockedText.Length && c == lockedText[letterIndex])
                {
                    textColor = FindGameObjectInChildWithTag(lockedEnemy, "textboxes").GetComponent<KillText>().killText.text;
                    textColor = textColor.Insert(colorIndex, "<color=#000000ff>");
                    colorIndex += colorchangeLength.Length;
                    colorIndex++;
                    if (colorIndex < textColor.Length && ' ' == textColor[colorIndex]) { colorIndex++; }
                    textColor = textColor.Insert(colorIndex, "</color>");
                    colorIndex += colorchangeLength2.Length;

                    letterIndex++;
                    FindGameObjectInChildWithTag(lockedEnemy, "textboxes").GetComponent<KillText>().killText.text = textColor;
                    fire.GetComponent<GunPoint>().Fire(bodyPart);
                    StartCoroutine(PPParticle(CalcTimeDist(bodyPart, gameManager.BallSpeed), bodyPart));
                }

                if (letterIndex >= lockedText.Length)
                {
                    if (FindParentWithTag(lockedEnemy, "destroyenemy"))
                    {
                        lockedEnemy.GetComponent<Enemy>().IndexEnd();
                    }
                    if (FindParentWithTag(lockedEnemy, "barrel"))
                    {
                        StartCoroutine(FindParentWithTag(lockedEnemy, "barrel").GetComponent<Barrel>().Destruct());
                    }
                    ResetLockOn();
                    FindTextBoxes(false);
                    StartCoroutine(WaitOnDeath());
                }
            }
        }
    }

    public void PcAndroidNewMatch()
    {
        if (!lockOn)
        {
            foreach (char c in Input.inputString)
            {
                KillText[] texts = FindObjectsOfType<KillText>();
                for (int i = 0; i < texts.Length; i++)
                {
                    char s = texts[i].findText[0];
                    if (s == c)
                    {
                        matchedEnemies.Add(texts[i]);
                    }
                }
                if (matchedEnemies.Count > 0)
                {
                    KillText distMatch = null;
                    float distance = Mathf.Infinity;
                    Vector3 position = transform.position;
                    foreach (KillText go in matchedEnemies)
                    {
                        Vector3 diff = go.transform.position - position;
                        float curDistance = diff.sqrMagnitude;
                        if (curDistance < distance)
                        {
                            distMatch = go;
                            distance = curDistance;
                        }
                    }
                    FindTextBoxes(true);
                    distMatch.FullAlpha();
                    distMatch.RedBorder();
                    killText = distMatch;
                    lockedText = distMatch.findText;
                    lockedObjectText = distMatch.gameObject;
                    lockedEnemy = FindParentWithTag(lockedObjectText, "Enemy");
                    lockOn = true;
                    matchedEnemies.Clear();
                    break;
                }
            }
        }   
    }

    public void PcAndroidEnemyMatched()
    {
        GameObject bodyPart = FindRandomGameObjectInChild(lockedEnemy, "bodypart");
        if (lockOn)
        {
            foreach (char c in Input.inputString)
            {
                if (letterIndex < lockedText.Length && c == lockedText[letterIndex])
                {
                    textColor = FindGameObjectInChildWithTag(lockedEnemy, "textboxes").GetComponent<KillText>().killText.text;
                    textColor = textColor.Insert(colorIndex, "<color=#000000ff>");
                    colorIndex += colorchangeLength.Length;
                    colorIndex++;
                    textColor = textColor.Insert(colorIndex, "</color>");
                    colorIndex += colorchangeLength2.Length;

                    letterIndex++;
                    if (letterIndex < lockedText.Length && ' ' == lockedText[letterIndex]) { letterIndex++; colorIndex++; }
                    FindGameObjectInChildWithTag(lockedEnemy, "textboxes").GetComponent<KillText>().killText.text = textColor;
                    fire.GetComponent<GunPoint>().Fire(bodyPart);
                    StartCoroutine(PPParticle(CalcTimeDist(bodyPart, gameManager.BallSpeed), bodyPart));
                }

                if (letterIndex >= lockedText.Length)
                {
                    if (FindParentWithTag(lockedEnemy, "destroyenemy"))
                    {
                        lockedEnemy.GetComponent<Enemy>().IndexEnd();
                    }
                    if (FindParentWithTag(lockedEnemy, "barrel"))
                    {
                        StartCoroutine(FindParentWithTag(lockedEnemy, "barrel").GetComponent<Barrel>().Destruct());
                    }
                    ResetLockOn();
                    FindTextBoxes(false);
                    StartCoroutine(WaitOnDeath());
                }
            }
        }  
    }

    public static GameObject FindPuppetPartGameObjectInChild(GameObject parent, string name)
    {
        if (parent != null)
        {
            Transform t = parent.transform;

            Transform[] all = t.GetComponentsInChildren<Transform>();
            all = all.Where(child => child.name == name).ToArray();
            if (all.Length == 1)
            {
                var part = all[0].gameObject;
                return part;
            }
            else { Debug.Log("name not found" + name); }
        }

        return null;
    }
    public void ResetLockOn()
    {
        letterIndex = 0;
        colorIndex = 0;
        lockOn = false;
        lockedEnemy = null;
        lockedObjectText = null;
        lockedText = null;
    }
    public void BarrelReset(GameObject check)
    {
        if (lockedEnemy != null)
        {
            if (FindParentWithTag(lockedEnemy, "barrel") != null)
            {
                letterIndex = 0;
                colorIndex = 0;
                lockOn = false;
                lockedEnemy = null;
                lockedObjectText = null;
                lockedText = null;
                FindTextBoxes(false);
            }
        }
    }

    public IEnumerator PPParticle(float time, GameObject part)
    {
        yield return new WaitForSeconds(time);
        if (part != null) {
            var spark = Instantiate(particle, part.transform.position + part.transform.forward * -0.5f, Quaternion.identity);
            yield return new WaitForSeconds(2f);
            Destroy(spark);
        }
        
    }
    public IEnumerator WaitOnDeath()
    {
        if (FindClosestEnemyScript() != null) { StartCoroutine(player.GetComponent<Player>().Wait(1.0f));}
        yield break;
    }
}

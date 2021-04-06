using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCurve : MonoBehaviour
{
    [SerializeField] private Transform[] routes;
    [SerializeField] private float speedModifier = 0.5f;
    
    private int routeToGo = 0;
    private float tParam = 0f;
    private Vector2 objectPosition;
    private bool coroutineAllowed = true;

    private void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(FollowRoute(routeToGo));
        }
    }

    private IEnumerator FollowRoute(int routeNumber)
    {
        coroutineAllowed = false;

        Vector2 p0 = routes[routeNumber].GetChild(0).position;
        Vector2 p1 = routes[routeNumber].GetChild(1).position;
        Vector2 p2 = routes[routeNumber].GetChild(2).position;
        Vector2 p3 = routes[routeNumber].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            objectPosition =  Mathf.Pow(1 - tParam, 3) * p0 +
            3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
            3 * (1 - tParam) * Mathf.Pow(1 - tParam, 2) * p2 +
            Mathf.Pow(tParam, 3) * p3;

            transform.position = objectPosition;
            yield return new WaitForEndOfFrame();
        }
        tParam = 0f;
        routeToGo++;
        
        if(routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
        }
        coroutineAllowed = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbility : MonoBehaviour
{
   
    [SerializeField] private float increaseSize;
    private float sizeIncreased = 0;
    Vector3 circleResetSize;
    [SerializeField] private float triggerSize;
    private bool changedColor = false;
    // Start is called before the first frame update

    private void Awake()
    {
        sizeIncreased += transform.localScale.x;
        circleResetSize = transform.localScale;
    }
    private void FixedUpdate()
    {
        ChangeCirclesize();
    }
    public void ChangeCirclesize()
    {
        if (sizeIncreased <= triggerSize)
        {
            transform.localScale += new Vector3(increaseSize, increaseSize, 0);
            sizeIncreased += increaseSize;

        }
        else if (!changedColor)
        {
            Debug.Log("Special power ready");
            //circle.GetComponent<SpriteRenderer>().color = translucentYellow;
            changedColor = true;
        }

    }
    public void ResetCircleSize()
    {
        //circle.GetComponent<SpriteRenderer>().color = translucentWhite;
        transform.localScale = circleResetSize;
        sizeIncreased = transform.localScale.x;
        changedColor = false;
    }
}

using System.Collections;
using UnityEngine;

public class SetTextureOffset : MonoBehaviour {

    public Vector2 offsetValue;
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        StartCoroutine(MoveTheTexture());
    }

    IEnumerator MoveTheTexture()
    {
        while (true)
        {
            yield return new WaitForSeconds(5.0F);
            offsetValue = rend.material.GetTextureOffset("_MainTex");
            offsetValue.y = offsetValue.y + 1.0F / 3.0F;
            rend.material.SetTextureOffset("_MainTex", new Vector2(offsetValue.x, offsetValue.y));
        }
    }
}

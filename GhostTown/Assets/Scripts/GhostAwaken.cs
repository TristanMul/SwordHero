using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAwaken : MonoBehaviour
{
    public Material materialAwake;
    private Renderer ghostRenderer;
    private Color ghostColor;
    private EnemyFollow enemyFollow;
    public bool isAwake;

    void Start()
    {
        ghostRenderer = transform.Find("Ghost").GetComponent<Renderer>();
        ghostColor = GetComponent<TypeOfGhost>().color;
        enemyFollow = GetComponent<EnemyFollow>();
        isAwake = false;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.T)){
            WakeUpGhost();
        }
    }

    public void WakeUpGhost(){
        isAwake = true;
        ChangeMaterial();
        enemyFollow.enabled = true;

    }

    private void ChangeMaterial(){
        // if(ghostRenderer.material != materialAwake){
        //     ghostRenderer.material = materialAwake;
        // }
        // ghostRenderer.material.color = Color.Lerp(ghostRenderer.material.color, ghostColor, 1f);
        ghostRenderer.material.color = ghostColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeOfGhost : MonoBehaviour
{

    // Temporary solution to getting the type of ghost
    // until proper inheritence is in place.
    public GhostType ghostType;
    public enum GhostType{
        Normal,
        Fast,
        Big,
    }

    public Color color;

    private void Start() {
        color = SetColor();
    }

    // Set color to match the type of ghost that is caught.
    // This is used for the poof effect on the vacuum of the player.
    private Color SetColor(){
        switch(ghostType){
            case GhostType.Normal:
                return new Color(0f, 0.97f, 1.0f);
            case GhostType.Fast:
                return Color.red;
            case GhostType.Big:
                return Color.magenta;
            default:
                return Color.white;
        }
    }

    

}

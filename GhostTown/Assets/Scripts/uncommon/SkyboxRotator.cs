using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    public float RotationPerSecond = 1;
    public bool _rotate;
    public Material[] skyboxes;
    public Material ground;
    public Texture[] groundSprites;

    private void Awake()
    {
        ground.mainTexture = groundSprites[Random.Range(0, groundSprites.Length)];

        RenderSettings.skybox = skyboxes[Random.Range(0, skyboxes.Length)];
    }

    protected void Update()
    {
        if (_rotate) RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotationPerSecond);
    }

    public void ToggleSkyboxRotation()
    {
        _rotate = !_rotate;
    }
}
using UnityEngine;


[ExecuteInEditMode, RequireComponent(typeof(Camera))]
public class RenderDepth : MonoBehaviour
{
    public bool renderDepth = true;

    void OnEnable()
    {
        SetCameraDepth();
    }

    void OnValidate()
    {
        SetCameraDepth();
    }

    void SetCameraDepth()
    {
        var cam = GetComponent<Camera>();
        if (renderDepth)
            cam.depthTextureMode |= DepthTextureMode.Depth;
        else
            cam.depthTextureMode &= ~DepthTextureMode.Depth;
    }
}

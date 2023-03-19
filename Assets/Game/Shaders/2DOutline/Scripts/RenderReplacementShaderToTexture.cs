using UnityEngine;
using UnityEngine.Rendering;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class RenderReplacementShaderToTexture : MonoBehaviour
{
    [SerializeField]
    Shader replacementShader;

    [SerializeField]
    RenderTextureFormat renderTextureFormat = RenderTextureFormat.ARGB32;

    [SerializeField]
    FilterMode filterMode = FilterMode.Point;

    [SerializeField]
    int renderTextureDepth = 24;

    [SerializeField]
    CameraClearFlags cameraClearFlags = CameraClearFlags.Color;

    [SerializeField]
    Color background = Color.black;

    [SerializeField]
    string targetTexture = "_RenderTexture";

    private RenderTexture renderTexture;
    private new Camera camera;

    private void Start()
    {
        foreach (Transform t in transform)
        {
            DestroyImmediate(t.gameObject);
        }

        Camera thisCamera = GetComponent<Camera>();

        // Create a render texture matching the main camera's current dimensions.
        renderTexture = new RenderTexture(thisCamera.pixelWidth, thisCamera.pixelHeight, renderTextureDepth, renderTextureFormat);
        renderTexture.filterMode = filterMode;
        // Surface the render texture as a global variable, available to all shaders.
        Shader.SetGlobalTexture(targetTexture, renderTexture);

        // Setup a copy of the camera to render the scene using the normals shader.
        GameObject copy = new GameObject("Camera" + targetTexture);
        camera = copy.AddComponent<Camera>();
        camera.CopyFrom(thisCamera);
        camera.transform.SetParent(transform);
        camera.targetTexture = renderTexture;
        camera.SetReplacementShader(replacementShader, "RenderType");
        camera.depth = thisCamera.depth - 1;
        camera.clearFlags = cameraClearFlags;
        camera.backgroundColor = background;
        //camera.cullingMask &= ~(1 << LayerMask.NameToLayer("Default"));
        for (int i = 0; i <= 10; i++)
        {
            camera.cullingMask = camera.cullingMask ^ (1 << i);
        }
        //camera.cullingMask |= 1 << LayerMask.NameToLayer("Player");
        //camera.cullingMask |= 1 << LayerMask.NameToLayer("Enemy");
        camera.cullingMask |= 1 << LayerMask.NameToLayer("Global");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{   
    private RenderTexture renderTextureInstance;
    private Camera cam;
    
    private void Start()
    {
        renderTextureInstance = RenderTexture.GetTemporary(512, 512, 16, RenderTextureFormat.ARGB32);

         // Configura a Render Texture
        renderTextureInstance.wrapMode = TextureWrapMode.Clamp;
        renderTextureInstance.filterMode = FilterMode.Bilinear;

        this.gameObject.GetComponent<Renderer>().material.mainTexture = renderTextureInstance;

        cam = GetComponentInChildren<Camera>();
        cam.targetTexture = renderTextureInstance;
    }


    void OnDestroy()
    {
        RenderTexture.ReleaseTemporary(renderTextureInstance);
    }
}

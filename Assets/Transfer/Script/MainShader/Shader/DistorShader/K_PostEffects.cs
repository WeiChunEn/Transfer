using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class K_PostEffects : MonoBehaviour
{
    public Material mat;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        
        Graphics.Blit(source, destination, mat);
    }
}

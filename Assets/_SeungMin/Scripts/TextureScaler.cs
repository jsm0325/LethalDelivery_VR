using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScaler : MonoBehaviour
{
    public Vector2 textureScale = new Vector2(1, 1);

    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            float scaleX = transform.localScale.x * textureScale.x;
            float scaleY = transform.localScale.y * textureScale.y;
            renderer.material.mainTextureScale = new Vector2(scaleX, scaleY);
        }
    }
}

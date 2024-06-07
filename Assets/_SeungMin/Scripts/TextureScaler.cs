using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScaler : MonoBehaviour
{
    public Vector2 baseScale = new Vector2(1, 1); // ±âº» Tiling °ª
    private Renderer _renderer;
    private Vector3 _previousScale;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _previousScale = transform.localScale;
        UpdateTextureScale();
    }

    void Update()
    {
        if (transform.localScale != _previousScale)
        {
            UpdateTextureScale();
            _previousScale = transform.localScale;
        }
    }

    void UpdateTextureScale()
    {
        Vector2 newScale = new Vector2(
            baseScale.x * transform.localScale.x,
            baseScale.y * transform.localScale.y
        );
        _renderer.material.mainTextureScale = newScale;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    // ID is used to prevent the MaterialProperty to try to optimize the rendering as it breaks the shader
    private static int Material_ID = 0;
    // Contains the info needed for the shader, loaded from the level's json
    public Shape Shape;

    // Reference to the renderer
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        StartCoroutine(SetShader());
    }

    // Set the shape map of the recepter shader
    IEnumerator SetShader()
    {
        yield return new WaitForFixedUpdate();
        if (Shape != null)
        {
            MaterialPropertyBlock properties = new MaterialPropertyBlock();
            _renderer.GetPropertyBlock(properties);
            properties.SetFloat("_ID", Material_ID++);
            properties.SetTexture("_ShapeMask", Shape.Mask.texture);
            properties.SetVector("_BordersWidth", Vector4.one * (Shape.BorderWidth / 2));
            properties.SetFloat("_BorderXOffset", Shape.BorderOffset.x);
            properties.SetFloat("_BorderYOffset", Shape.BorderOffset.y);
            properties.SetFloat("_KeepScale", Shape.KeepScale);
            _renderer.SetPropertyBlock(properties);
        }
    }
}

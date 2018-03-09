using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BlockDatas class.
/// Used to manipulate the values of the ZooCube/Block shader through the use of the Animal and Shape ScriptableObjects.
/// This is mostly a test class used to manipulate the EditablePieces while not in play mode.
/// </summary>
[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
[System.Serializable]
public class BlockDatas : MonoBehaviour
{
    /// <summary>
    /// SpriteRenderer getter. The SpriteRenderer is stored for optimization. Uses a getter to be usable in EditMode
    /// </summary>
    private SpriteRenderer _sprite_renderer = null;
    private SpriteRenderer SpriteRenderer
    {
        get
        {
            if (_sprite_renderer == null)
                _sprite_renderer = GetComponent<SpriteRenderer>();
            return _sprite_renderer;
        }
        set { }
    }

    /// <summary>
    /// Animal data. When set, the shader's properties are updated.
    /// </summary>
    [SerializeField]
    private Animal _animal;
    public Animal Animal
    {
        get { return _animal; }
        set
        {
            _animal = value;
            ApplyAnimalToShader();
        }
    }

    /// <summary>
    /// Shape data. When set, the shader's properties are updated.
    /// </summary>
    [SerializeField]
    private Shape _shape;
    public Shape Shape
    {
        get { return _shape; }
        set
        {
            _shape = value;
            ApplyShapeToShader();
        }
    }

    void Start()
    {
        ApplyPropertiesToShader();
    }

    /// <summary>
    /// Apply the values stored in _shape to the shader's properties.
    /// </summary>
    private void ApplyShapeToShader()
    {
        if (_shape != null)
        {
            MaterialPropertyBlock properties = new MaterialPropertyBlock();
            SpriteRenderer.GetPropertyBlock(properties);
            properties.SetFloat("_FaceScale", _shape.FaceScale);
            properties.SetTexture("_ShapeMask", _shape.Mask.texture);
            properties.SetFloat("_BorderWidth", _shape.BorderWidth);
            properties.SetFloat("_BorderXOffset", _shape.BorderOffset.x);
            properties.SetFloat("_BorderYOffset", _shape.BorderOffset.y);
            properties.SetFloat("_FaceXOffset", _shape.FaceOffset.x);
            properties.SetFloat("_FaceYOffset", _shape.FaceOffset.y);
            SpriteRenderer.SetPropertyBlock(properties);
        }
    }

    /// <summary>
    /// Apply the values stored in _animal to the shader's properties.
    /// </summary>
    private void ApplyAnimalToShader()
    {
        if (_animal != null)
        {
            MaterialPropertyBlock properties = new MaterialPropertyBlock();
            SpriteRenderer.GetPropertyBlock(properties);
            properties.SetTexture("_Face", _animal.Face.texture);
            properties.SetColor("_BackgroundColor", _animal.Color);
            properties.SetColor("_BorderColor", _animal.BorderColor);
            SpriteRenderer.SetPropertyBlock(properties);
        }
    }

    /// <summary>
    /// Apply both the animal and shape datas at the same time.
    /// </summary>
    private void ApplyPropertiesToShader()
    {
        ApplyAnimalToShader();
        ApplyShapeToShader();
    }
}

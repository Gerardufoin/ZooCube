using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
[System.Serializable]
public class BlockDatas : MonoBehaviour
{
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

    [SerializeField]
    private Texture _shape_mask;
    public Texture ShapeMask
    {
        get { return _shape_mask; }
        set
        {
            _shape_mask = value;
            MaterialPropertyBlock properties = new MaterialPropertyBlock();
            SpriteRenderer.GetPropertyBlock(properties);
            if (_shape_mask != null)
                properties.SetTexture("_ShapeMask", _shape_mask);
            SpriteRenderer.SetPropertyBlock(properties);
        }
    }

    [SerializeField]
    private float _image_scale = 1.0f;
    public float ImageScale
    {
        get { return _image_scale; }
        set
        {
            _image_scale = value;
            MaterialPropertyBlock properties = new MaterialPropertyBlock();
            SpriteRenderer.GetPropertyBlock(properties);
            properties.SetFloat("_ImageScale", _image_scale);
            SpriteRenderer.SetPropertyBlock(properties);
        }
    }

    [SerializeField]
    private Color _background_color;
    public Color BackgroundColor
    {
        get { return _background_color; }
        set
        {
            _background_color = value;
            MaterialPropertyBlock properties = new MaterialPropertyBlock();
            SpriteRenderer.GetPropertyBlock(properties);
            properties.SetColor("_BackgroundColor", _background_color);
            SpriteRenderer.SetPropertyBlock(properties);
        }
    }

    [SerializeField]
    private Color _border_color;
    public Color BorderColor
    {
        get { return _border_color; }
        set
        {
            _border_color = value;
            MaterialPropertyBlock properties = new MaterialPropertyBlock();
            SpriteRenderer.GetPropertyBlock(properties);
            properties.SetColor("_BorderColor", _border_color);
            SpriteRenderer.SetPropertyBlock(properties);
        }
    }

    [SerializeField]
    private float _border_width = 0.1f;
    public float BorderWidth
    {
        get { return _border_width; }
        set
        {
            _border_width = value;
            MaterialPropertyBlock properties = new MaterialPropertyBlock();
            SpriteRenderer.GetPropertyBlock(properties);
            properties.SetFloat("_BorderWidth", _border_width);
            SpriteRenderer.SetPropertyBlock(properties);
        }
    }

    [SerializeField]
    private float _border_x_offset;
    public float BorderXOffset
    {
        get { return _border_x_offset; }
        set
        {
            _border_x_offset = value;
            MaterialPropertyBlock properties = new MaterialPropertyBlock();
            SpriteRenderer.GetPropertyBlock(properties);
            properties.SetFloat("_BorderXOffset", _border_x_offset);
            SpriteRenderer.SetPropertyBlock(properties);
        }
    }

    [SerializeField]
    private float _border_y_offset;
    public float BorderYOffset
    {
        get { return _border_y_offset; }
        set
        {
            _border_y_offset = value;
            MaterialPropertyBlock properties = new MaterialPropertyBlock();
            SpriteRenderer.GetPropertyBlock(properties);
            properties.SetFloat("_BorderYOffset", _border_y_offset);
            SpriteRenderer.SetPropertyBlock(properties);
        }
    }

    [SerializeField]
    private float _image_x_offset;
    public float ImageXOffset
    {
        get { return _image_x_offset; }
        set
        {
            _image_x_offset = value;
            MaterialPropertyBlock properties = new MaterialPropertyBlock();
            SpriteRenderer.GetPropertyBlock(properties);
            properties.SetFloat("_ImageXOffset", _image_x_offset);
            SpriteRenderer.SetPropertyBlock(properties);
        }
    }

    [SerializeField]
    private float _image_y_offset;
    public float ImageYOffset
    {
        get { return _image_y_offset; }
        set
        {
            _image_y_offset = value;
            MaterialPropertyBlock properties = new MaterialPropertyBlock();
            SpriteRenderer.GetPropertyBlock(properties);
            properties.SetFloat("_ImageYOffset", _image_y_offset);
            SpriteRenderer.SetPropertyBlock(properties);
        }
    }

    void Start()
    {
        ApplyPropertiesToShader();
    }

    private void ApplyPropertiesToShader()
    {
        MaterialPropertyBlock properties = new MaterialPropertyBlock();
        SpriteRenderer.GetPropertyBlock(properties);
        properties.SetFloat("_ImageScale", _image_scale);
        if (_shape_mask != null)
            properties.SetTexture("_ShapeMask", _shape_mask);
        properties.SetColor("_BackgroundColor", _background_color);
        properties.SetColor("_BorderColor", _border_color);
        properties.SetFloat("_BorderWidth", _border_width);
        properties.SetFloat("_BorderXOffset", _border_x_offset);
        properties.SetFloat("_BorderYOffset", _border_y_offset);
        properties.SetFloat("_ImageXOffset", _image_x_offset);
        properties.SetFloat("_ImageYOffset", _image_y_offset);
        SpriteRenderer.SetPropertyBlock(properties);
    }
}

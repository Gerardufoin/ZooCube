using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Reflection;
using System.Collections;

[CustomEditor(typeof(BlockDatas))]
[CanEditMultipleObjects]
public class BlockDatasEditor : Editor
{
    private Texture _shape_mask;
    private float _image_scale;
    private Color _background_color;
    private Color _border_color;
    private float _border_width;
    private float _border_x_offset;
    private float _border_y_offset;
    private float _image_x_offset;
    private float _image_y_offset;

    private BlockDatas renderer;

    void OnEnable()
    {
        renderer = target as BlockDatas;
        _shape_mask = renderer.ShapeMask;
        _image_scale = renderer.ImageScale;
        _background_color = renderer.BackgroundColor;
        _border_color = renderer.BorderColor;
        _border_width = renderer.BorderWidth;
        _border_x_offset = renderer.BorderXOffset;
        _border_y_offset = renderer.BorderYOffset;
        _image_x_offset = renderer.ImageXOffset;
        _image_y_offset = renderer.ImageYOffset;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        _shape_mask = EditorGUILayout.ObjectField("ShapeMask", _shape_mask, typeof(Texture), false) as Texture;
        if (EditorGUI.EndChangeCheck())
        {
            renderer.ShapeMask = _shape_mask;
        }

        EditorGUI.BeginChangeCheck();
        _image_scale = EditorGUILayout.Slider("Image Scale", _image_scale, 0.0f, 2.0f);
        if (EditorGUI.EndChangeCheck())
        {
            renderer.ImageScale = _image_scale;
        }

        EditorGUI.BeginChangeCheck();
        _background_color = EditorGUILayout.ColorField("Background Color", _background_color);
        if (EditorGUI.EndChangeCheck())
        {
            renderer.BackgroundColor = _background_color;
        }

        EditorGUI.BeginChangeCheck();
        _border_color = EditorGUILayout.ColorField("Border Color", _border_color);
        if (EditorGUI.EndChangeCheck())
        {
            renderer.BorderColor = _border_color;
        }

        EditorGUI.BeginChangeCheck();
        _border_width = EditorGUILayout.Slider("Border Width", _border_width, 0.0f, 1.0f);
        if (EditorGUI.EndChangeCheck())
        {
            renderer.BorderWidth = _border_width;
        }

        EditorGUI.BeginChangeCheck();
        _border_x_offset = EditorGUILayout.Slider("Border X Offset", _border_x_offset, -0.1f, 0.1f);
        if (EditorGUI.EndChangeCheck())
        {
            renderer.BorderXOffset = _border_x_offset;
        }

        EditorGUI.BeginChangeCheck();
        _border_y_offset = EditorGUILayout.Slider("Border Y Offset", _border_y_offset, -0.1f, 0.1f);
        if (EditorGUI.EndChangeCheck())
        {
            renderer.BorderYOffset = _border_y_offset;
        }

        EditorGUI.BeginChangeCheck();
        _image_x_offset = EditorGUILayout.Slider("Image X Offset", _image_x_offset, -1.0f, 1.0f);
        if (EditorGUI.EndChangeCheck())
        {
            renderer.ImageXOffset = _image_x_offset;
        }

        EditorGUI.BeginChangeCheck();
        _image_y_offset = EditorGUILayout.Slider("Image Y Offset", _image_y_offset, -1.0f, 1.0f);
        if (EditorGUI.EndChangeCheck())
        {
            renderer.ImageYOffset = _image_y_offset;
        }
    }
}
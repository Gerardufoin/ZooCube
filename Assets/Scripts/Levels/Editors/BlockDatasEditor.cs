using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Reflection;
using System.Collections;

/// <summary>
/// Editor allowing to edit the Animal and Shape value of a BlockDatas through the inspector.
/// </summary>
[CustomEditor(typeof(BlockDatas))]
[CanEditMultipleObjects]
public class BlockDatasEditor : Editor
{
    private Animal _animal;
    private Shape _shape;

    private BlockDatas renderer;

    void OnEnable()
    {
        renderer = target as BlockDatas;
        _animal = renderer.Animal;
        _shape = renderer.Shape;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        _animal = EditorGUILayout.ObjectField("Animal", _animal, typeof(Animal), false) as Animal;
        if (EditorGUI.EndChangeCheck())
        {
            renderer.Animal = _animal;
        }

        EditorGUI.BeginChangeCheck();
        _shape = EditorGUILayout.ObjectField("Shape", _shape, typeof(Shape), false) as Shape;
        if (EditorGUI.EndChangeCheck())
        {
            renderer.Shape = _shape;
        }
    }
}
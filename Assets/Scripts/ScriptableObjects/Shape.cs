using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shape", menuName = "ZooCube/Shape")]
public class Shape : ScriptableObject
{
    public GameDatas.ShapeType Type;
    public Sprite Mask;
    public float FaceScale = 1.0f;
    public Vector2 FaceOffset;
    public float BorderWidth = 0.1f;
    public Vector2 BorderOffset;
}

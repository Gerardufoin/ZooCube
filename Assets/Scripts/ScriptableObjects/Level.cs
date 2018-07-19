using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_LevelType
{
    SIMPLE,
    MIRROR
}

[CreateAssetMenu(fileName = "Level", menuName = "ZooCube/Level")]
public class Level : ScriptableObject
{
    public int Number;
    public E_LevelType Type;
    [TextArea]
    public string JsonData;
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_LevelType
{
    SIMPLE,
    HINT
}

[CreateAssetMenu(fileName = "Level", menuName = "ZooCube/Level")]
public class Level : ScriptableObject
{
    public int Number;
    public E_LevelType Type;
    public bool MirrorHint;
    [Range(0, 3)]
    public int Tutorial;
    [TextArea]
    public string JsonData;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ZooCube/Level")]
public class Level : ScriptableObject
{
    public int Number;
    [TextArea]
    public string JsonData;
}

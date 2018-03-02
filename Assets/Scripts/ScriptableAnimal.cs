using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Animal", menuName = "ZooCube/Animal")]
public class ScriptableAnimal : ScriptableObject
{
    public GameDatas.AnimalType Type;
    public Sprite Face;
    public Color Color = Color.white;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Animal", menuName = "ZooCube/Animal")]
public class Animal : ScriptableObject
{
    public GameDatas.E_AnimalType Type;
    public Sprite Face;
    public Color Color = Color.white;
    public Color BorderColor = Color.white;
}

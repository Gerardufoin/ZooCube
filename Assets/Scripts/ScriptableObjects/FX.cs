using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FX", menuName = "ZooCube/FX")]
public class FX : ScriptableObject
{
    public SoundManager.E_FX FXType;
    public AudioClip Clip;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_TranslationKey
{
    PLAY,
    OPTIONS,
    CREDITS,
    QUIT
}

[CreateAssetMenu(fileName = "Lang", menuName = "ZooCube/Lang")]
public class Language : ScriptableObject
{
    public E_Language Name;
    public List<string> Translation;
}

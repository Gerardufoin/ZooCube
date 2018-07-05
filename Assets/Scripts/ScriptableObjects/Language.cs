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

[System.Serializable]
public class Localization
{
    public E_TranslationKey Key;
    public string Value;
}

[CreateAssetMenu(fileName = "Lang", menuName = "ZooCube/Lang")]
public class Language : ScriptableObject
{
    public E_Language Name;
    public Localization[] Translation;
}

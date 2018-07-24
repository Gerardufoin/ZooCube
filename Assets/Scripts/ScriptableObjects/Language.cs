using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_TranslationKey
{
    PLAY,
    OPTIONS,
    CREDITS,
    QUIT,
    BACK,
    PROGRAMMING,
    THANKS,
    VOLUME,
    GENERAL,
    MUSIC,
    SOUND,
    RESOLUTION,
    FULLSCREEN,
    LANGUAGE,
    NEW_PROFILE,
    ENTER_USERNAME,
    CREATE,
    SELECT_PROFILE,
    CHOOSE_LEVEL,
    LEVEL_CLEARED,
    NEXT_LEVEL,
    LEVEL_SELECT,
    MAIN_MENU,
    LOADING,
    WARNING,
    DELETE_CONFIRMATION_1,
    DELETE_CONFIRMATION_2,
    CONFIRM,
    CANCEL,
    DELETE
}

[CreateAssetMenu(fileName = "Lang", menuName = "ZooCube/Lang")]
public class Language : ScriptableObject
{
    public E_Language Name;
    [SerializeField]
    public List<string> Translation;
}

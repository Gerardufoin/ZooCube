using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Editor for the Language scriptable object. List the E_TranslationKey enum with appropriate fields for an easier access.
/// </summary>
[CustomEditor(typeof(Language))]
public class LanguageEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Language t = (Language)target;

        EditorGUI.BeginChangeCheck();
        E_Language language = (E_Language)EditorGUILayout.EnumPopup("Language", t.Name);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changed the language of the translation");
            t.Name = language;
        }

        // List the enum with the Translation equivalent
        foreach (E_TranslationKey e in System.Enum.GetValues(typeof(E_TranslationKey)))
        {
            int idx = (int)e;
            if (idx >= t.Translation.Count)
            {
                t.Translation.Add("");
            }

            EditorGUI.BeginChangeCheck();
            string text = EditorGUILayout.TextField(e.ToString(), t.Translation[idx]);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed translation for " + e.ToString());
                t.Translation[idx] = text;
            }
        }
    }
}

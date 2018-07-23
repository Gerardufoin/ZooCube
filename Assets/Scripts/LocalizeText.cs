using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizeText : MonoBehaviour
{
    // Translation key of the text
    public E_TranslationKey TranslationKey;
    // Capitalize the text
    public bool Capitalize;

    // Reference to GameDatas
    private GameDatas _gameDatas;
    private GameDatas Datas
    {
        get
        {
            if (!_gameDatas)
            {
                _gameDatas = FindObjectOfType<GameDatas>();
            }
            return _gameDatas;
        }
    }

    // Reference to the TextMeshPro text
    private TextMeshProUGUI _text;
    private TextMeshProUGUI Text
    {
        get
        {
            if (!_text)
            {
                _text = GetComponent<TextMeshProUGUI>();
            }
            return _text;
        }
    }

    // Has the start function been called yet
    private bool _started;

    private void Start()
    {
        Localize();
        _started = true;
    }

    /// <summary>
    /// When a component is enabled, we set the translation (as it does not change while the objet is inactive
    /// </summary>
    private void OnEnable()
    {
        if (_started)
        {
            Localize();
        }
    }

    /// <summary>
    /// Set the translation of the text component based on the current translation of GameDatas
    /// </summary>
    public void Localize()
    {
        string txt = Datas.GetLocalization(TranslationKey);
        Text.text = (Capitalize ? txt.ToUpper() : txt).Replace("\\n", "\n");
    }
}

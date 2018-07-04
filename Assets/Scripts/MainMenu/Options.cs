using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum E_Language
{
    ENGLISH,
    FRENCH
}

public class Options : MonoBehaviour
{
    private const string RESOLUTION_KEY = "Prefered_Resolution";
    private const string IS_FULLSCREEN_KEY = "Prefered_Fullscreen";
    private const string LOCAL_KEY = "Prefered_Localization";

    // Reference to the general volume slider
    public Slider m_generalVolumeSlider;
    // Reference to the music volume slider
    public Slider m_musicVolumeSlider;
    // Reference to the sound volume slider
    public Slider m_soundVolumeSlider;
    // Reference to the resolution dropdown
    public TMP_Dropdown m_resolutionDropdown;
    // Reference to the fullscreen toggle
    public Toggle m_fullscreenToggle;
    // Reference to the language toggles
    public ToggleGroup m_languageToggles;

    // Reference to the content of the options panel
    private GameObject _optionsPanel;
    // Visibility of the options panel
    private bool _visibility;
    // Current resolution idx
    private int _resolutionIdx;
    // Current resolution
    private Vector2Int _resolution = new Vector2Int();
    // Is currently in fullscreen
    private bool _fullscreen;
    // Current language
    private E_Language _language;

    // Reference to the sound manager
    private SoundManager _soundManager;

	private void Start ()
    {
        _optionsPanel = transform.GetChild(0).gameObject;
        _soundManager = FindObjectOfType<SoundManager>();

        GetUserConfig();

        for (int i = 0; i < m_languageToggles.transform.childCount; ++i)
        {
            InitLanguageToggle(m_languageToggles.transform.GetChild(i).GetComponent<Toggle>(), i);
        }

        HidePanel();
	}
	
    /// <summary>
    /// Initialize a language toggle
    /// </summary>
    /// <param name="t">Toggle to initialize</param>
    /// <param name="val">Value of the toggle</param>
    private void InitLanguageToggle(Toggle t, int val)
    {
        t.isOn = (val == (int)_language);
        t.onValueChanged.AddListener(delegate
        {
            if (t.isOn)
            {
                ChangeLanguage(val);
            }
        });
    }

	private void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePanel();
        }
	}

    /// <summary>
    /// Init the options panel with the values stored in PlayerPrefs
    /// </summary>
    private void GetUserConfig()
    {
        m_generalVolumeSlider.value = _soundManager.GetMainVolume();
        m_musicVolumeSlider.value = _soundManager.GetBGMVolume();
        m_soundVolumeSlider.value = _soundManager.GetFXVolume();

        _resolutionIdx = PlayerPrefs.GetInt(RESOLUTION_KEY, 0);
        m_resolutionDropdown.value = _resolutionIdx;
        ConvertResolution(m_resolutionDropdown.options[_resolutionIdx].text);
        _fullscreen = PlayerPrefs.GetInt(IS_FULLSCREEN_KEY, 0) != 0;
        m_fullscreenToggle.isOn = _fullscreen;
        UpdateResolution();

        _language = (E_Language)PlayerPrefs.GetInt(LOCAL_KEY, (int)E_Language.ENGLISH);
    }

    /// <summary>
    /// Hide the options panel
    /// </summary>
    public void HidePanel()
    {
        _visibility = false;
        _optionsPanel.SetActive(false);
    }

    /// <summary>
    /// Display the options panel
    /// </summary>
    public void ShowPanel()
    {
        _visibility = true;
        _optionsPanel.SetActive(true);
    }

    /// <summary>
    /// Toggle the options panel's display
    /// </summary>
    public void TogglePanel()
    {
        if (_visibility)
        {
            HidePanel();
        }
        else
        {
            ShowPanel();
        }
    }

    /// <summary>
    /// Callback for the resolution dropdown. Set the resolution of the screen
    /// </summary>
    /// <param name="resolution">Index of the selected value of the dropdown</param>
    public void ChangeResolution(int idx)
    {
        PlayerPrefs.SetInt(RESOLUTION_KEY, idx);
        ConvertResolution(m_resolutionDropdown.options[idx].text);
        UpdateResolution();
    }

    /// <summary>
    /// Callback for the fullscreen toggle. Set the fullscreen option
    /// </summary>
    /// <param name="value">Is the game fullscreen</param>
    public void ChangeFullscreen(bool value)
    {
        _fullscreen = value;
        PlayerPrefs.SetInt(IS_FULLSCREEN_KEY, (value ? 1 : 0));
        UpdateResolution();
    }

    /// <summary>
    /// Convert a resolution from a string format ("1920x1080") to store it into the _resolution parameter
    /// </summary>
    /// <param name="val">The string to convert</param>
    private void ConvertResolution(string val)
    {
        string[] res = val.Split('x');
        if (res.Length != 2) return;

        _resolution.x = int.Parse(res[0]);
        _resolution.y = int.Parse(res[1]);
    }

    /// <summary>
    /// Apply the selected resolution and the fullscreen option to the screen
    /// </summary>
    private void UpdateResolution()
    {
        Screen.SetResolution(_resolution.x, _resolution.y, _fullscreen);
    }

    /// <summary>
    /// Change the language of the game. Callback for the language toggles
    /// </summary>
    /// <param name="lang">An E_Language value passed as int</param>
    public void ChangeLanguage(int lang)
    {
        _language = (E_Language)lang;
        PlayerPrefs.SetInt(LOCAL_KEY, lang);
        Debug.Log("Language set to " + _language);
    }
}

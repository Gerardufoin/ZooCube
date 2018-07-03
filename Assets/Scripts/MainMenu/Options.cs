using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    // Reference to the general volume slider
    public Slider m_generalVolumeSlider;
    // Reference to the music volume slider
    public Slider m_musicVolumeSlider;
    // Reference to the sound volume slider
    public Slider m_soundVolumeSlider;

    // Reference to the content of the options panel
    private GameObject _optionsPanel;
    // Visibility of the options panel
    private bool _visibility;

    // Reference to the sound manager
    private SoundManager _soundManager;

	private void Start ()
    {
        _optionsPanel = transform.GetChild(0).gameObject;
        _soundManager = FindObjectOfType<SoundManager>();

        m_generalVolumeSlider.value = _soundManager.GetMainVolume();
        m_musicVolumeSlider.value = _soundManager.GetBGMVolume();
        m_soundVolumeSlider.value = _soundManager.GetFXVolume();

        HidePanel();
	}
	
	private void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePanel();
        }
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
}

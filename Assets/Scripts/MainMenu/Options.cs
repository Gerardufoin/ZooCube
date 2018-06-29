using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    // Reference to the content of the options panel
    private GameObject _optionsPanel;
    // Visibility of the options panel
    private bool _visibility;

    // Reference to the sound manager
    private SoundManager _soundManager;

	private void Start ()
    {
        _optionsPanel = transform.GetChild(0).gameObject;
        HidePanel();
	}
	
	private void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePanel();
        }
	}

    public void HidePanel()
    {
        _visibility = false;
        _optionsPanel.SetActive(false);
    }

    public void ShowPanel()
    {
        _visibility = true;
        _optionsPanel.SetActive(true);
    }

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

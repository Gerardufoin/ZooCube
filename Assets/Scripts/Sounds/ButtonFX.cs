using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// ButtonFX class. Assigns the FXs of a button at the start of the scene.
/// </summary>
[RequireComponent(typeof(Button))]
public class ButtonFX : EventTrigger
{
    // Reference to the SoundManager
    private SoundManager _soundManager;

	void Start()
    {
        _soundManager = FindObjectOfType<SoundManager>();
    }

    /// <summary>
    /// Override of the PointerEnter event. Plays the hover FX.
    /// </summary>
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        _soundManager.PlayFX(SoundManager.E_FX.BUTTON_HOVER);
    }

    /// <summary>
    /// Override of the PointerDown event. Plays the click FX.
    /// </summary>
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        _soundManager.PlayFX(SoundManager.E_FX.BUTTON_CLICK);
    }
}

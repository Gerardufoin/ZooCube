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
    // Reference to the FX AudioSource
    private AudioSource _fxSource;

    // Reference to hover fx
    private AudioClip _hoverFX;
    // Reference to click fx
    private AudioClip _clickFX;

	void Start ()
    {
        SoundManager sManager = FindObjectOfType<SoundManager>();
        _fxSource = sManager.GetComponent<AudioSource>();
        _hoverFX = sManager.GetFX(SoundManager.E_FX.BUTTON_HOVER);
        _clickFX = sManager.GetFX(SoundManager.E_FX.BUTTON_CLICK);
    }

    /// <summary>
    /// Override of the PointerEnter event. Plays the hover FX.
    /// </summary>
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        _fxSource.PlayOneShot(_hoverFX);
    }

    /// <summary>
    /// Override of the PointerDown event. Plays the click FX.
    /// </summary>
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        _fxSource.PlayOneShot(_clickFX);
    }
}

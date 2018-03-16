using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SoundManager class. Manages the BGM and FX (volume, tracks, etc).
/// The object where the SoundManager is placed will automatically becomes the object that will play the FXs.
/// The BGM AudioSource is expected to be on the Main Camera.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
#region PlayerPrefs keys
    private const string BGM_VOLUME_KEY = "BGM_Volume";
    private const string BGM_MUTE_KEY = "BGM_Mute";
    private const string FX_VOLUME_KEY = "FX_Volume";
    private const string FX_MUTE_KEY = "FX_Mute";
#endregion

    // Type of FXs
    public enum E_FX
    {
        NONE = 0,
        BUTTON_HOVER,
        BUTTON_CLICK,
        CURTAIN,
        THEATER
    }

    // List of FXs
    [SerializeField]
    private Dictionary<E_FX, AudioClip> m_fxList = new Dictionary<E_FX, AudioClip>();

    // Reference to the BGM source (camera)
    private AudioSource _bgmSource;
    // Reference to the FX source (this)
    private AudioSource _fxSource;

	void Start ()
    {
        _bgmSource = Camera.main.GetComponent<AudioSource>();
        _fxSource = GetComponent<AudioSource>();
        _bgmSource.volume = PlayerPrefs.HasKey(BGM_VOLUME_KEY) ? PlayerPrefs.GetFloat(BGM_VOLUME_KEY) : 1f;
        _bgmSource.mute = PlayerPrefs.HasKey(BGM_MUTE_KEY) ? (PlayerPrefs.GetInt(BGM_MUTE_KEY) != 0) : false;
        _fxSource.volume = PlayerPrefs.HasKey(FX_VOLUME_KEY) ? PlayerPrefs.GetFloat(FX_VOLUME_KEY) : 1f;
        _fxSource.mute = PlayerPrefs.HasKey(FX_MUTE_KEY) ? (PlayerPrefs.GetInt(FX_MUTE_KEY) != 0) : false;
    }

    /// <summary>
    /// Mutes or unmutes the BGM
    /// </summary>
    /// <param name="state">Mute the BGM if true, unmute otherwise</param>
    public void MuteBGM(bool state)
    {
        _bgmSource.mute = state;
        PlayerPrefs.SetInt(BGM_MUTE_KEY, state ? 1 : 0);
    }

    /// <summary>
    /// Mutes or unmutes the FXs
    /// </summary>
    /// <param name="state">Mute the FXs if true, unmute otherwise</param>
    public void MuteFX(bool state)
    {
        _fxSource.mute = state;
        PlayerPrefs.SetInt(FX_MUTE_KEY, state ? 1 : 0);
    }

    /// <summary>
    /// Changes the volume of the BGM
    /// </summary>
    /// <param name="volume">Set the volume of the BGM. The volume is clamped between 0 and 1</param>
    public void SetBGMVolume(float volume)
    {
        volume = Mathf.Clamp01(volume);
        _bgmSource.volume = volume;
        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, volume);
    }

    /// <summary>
    /// Changes the volume of the FXs
    /// </summary>
    /// <param name="volume">Set the volume of the FXs. The volume is clamped between 0 and 1</param>
    public void SetFXVolume(float volume)
    {
        volume = Mathf.Clamp01(volume);
        _fxSource.volume = volume;
        PlayerPrefs.SetFloat(FX_VOLUME_KEY, volume);
    }

    /// <summary>
    /// Returns a FX AudioClip from the list.
    /// </summary>
    /// <param name="fx">Type of the clip to return</param>
    /// <returns></returns>
    public AudioClip GetFX(E_FX fx)
    {
        return null;
    }
}

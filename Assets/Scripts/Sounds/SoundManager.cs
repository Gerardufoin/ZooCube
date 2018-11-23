using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

/// <summary>
/// SoundManager class. Manages the BGM and FX (volume, tracks, etc).
/// The object where the SoundManager is placed will automatically becomes the object that will play some general FXs.
/// The BGM AudioSource is expected to be on the Main Camera.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
#region PlayerPrefs keys
    private const string MAIN_VOLUME_KEY = "MAIN_Volume";
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
        BUTTON_CLICK
    }

    [System.Serializable]
    public struct S_FX
    {
        public E_FX Type;
        public AudioClip Clip;
    }

    public AudioMixer m_audioMixer;

    // Reference to the "mute fx" toggle
    [SerializeField]
    private ToggleIcon m_muteFXToggle;
    // Reference to the "mute BGM" toggle
    [SerializeField]
    private ToggleIcon m_muteBGMToggle;
    // List of FXs
    [SerializeField]
    private List<S_FX> m_fxList = new List<S_FX>();

    // Reference to the FX source (this)
    private AudioSource _fxSource;

    void Start ()
    {
        _fxSource = GetComponent<AudioSource>();

        SetMainVolume();
        SetBGMVolume();
        SetFXVolume();
        m_muteBGMToggle.SetState(!IsMuted(BGM_MUTE_KEY));
        m_muteFXToggle.SetState(!IsMuted(FX_MUTE_KEY));
    }

    /// <summary>
    /// Set the selected group volume in the mixer m_audioMixer
    /// </summary>
    /// <param name="key">Mixer's group key</param>
    /// <param name="volume">Volume to set</param>
    /// <param name="muted">Is the volume muted</param>
    private void SetMixerVolume(string key, float volume, bool muted)
    {
        // Mixer volume is not linear and follow the logarithm Log([0.01f-1f]) * 20
        float trueVolume = Mathf.Log(Mathf.Clamp((muted ? 0f : volume), 0.01f, 1f)) * 20;
        m_audioMixer.SetFloat(key, trueVolume);
    }

    /// <summary>
    /// Set the volume of the 'Master' Mixer's group
    /// </summary>
    private void SetMainVolume()
    {
        SetMixerVolume("Master", GetMainVolume(), false);
    }

    /// <summary>
    /// Set the volume of the 'BGM' Mixer's group
    /// </summary>
    private void SetBGMVolume()
    {
        SetMixerVolume("BGM", GetBGMVolume(), IsMuted(BGM_MUTE_KEY));
    }

    /// <summary>
    /// Set the volume of the 'FX' Mixer's group
    /// </summary>
    private void SetFXVolume()
    {
        SetMixerVolume("SFX", GetFXVolume(), IsMuted(FX_MUTE_KEY));
    }

    /// <summary>
    /// Check if the selected PlayerPrefabs' key is muted
    /// </summary>
    /// <param name="key">PlayerPrefabs' key containing the mute value</param>
    /// <returns>True if the key contains a muted volume, false otherwise</returns>
    private bool IsMuted(string key)
    {
        return PlayerPrefs.GetInt(key, 0) != 0;
    }

    /// <summary>
    /// Mutes or unmutes the BGM
    /// </summary>
    /// <param name="state">Mute the BGM if true, unmute otherwise</param>
    public void MuteBGM(bool state)
    {
        PlayerPrefs.SetInt(BGM_MUTE_KEY, state ? 1 : 0);
        m_muteBGMToggle.SetState(!state);
        SetBGMVolume();
    }

    /// <summary>
    /// Toggles the mute state of the BGM
    /// </summary>
    public void ToggleBGM()
    {
        MuteBGM(!IsMuted(BGM_MUTE_KEY));
    }

    /// <summary>
    /// Mutes or unmutes the FXs
    /// </summary>
    /// <param name="state">Mute the FXs if true, unmute otherwise</param>
    public void MuteFX(bool state)
    {
        PlayerPrefs.SetInt(FX_MUTE_KEY, state ? 1 : 0);
        m_muteFXToggle.SetState(!state);
        SetFXVolume();
    }

    /// <summary>
    /// Toggles the mute state of the FXs
    /// </summary>
    public void ToggleFX()
    {
        MuteFX(!IsMuted(FX_MUTE_KEY));
    }

    /// <summary>
    /// Return the main volume set in the player pref. If no volume is set, 1 is returned.
    /// </summary>
    /// <returns>Main volume</returns>
    public float GetMainVolume()
    {
        return PlayerPrefs.GetFloat(MAIN_VOLUME_KEY, 1f);
    }

    /// <summary>
    /// Changes the main volume
    /// </summary>
    /// <param name="volume">Set the main volume. The volume is clamped between 0 and 1</param>
    public void SetMainVolume(float volume)
    {
        PlayerPrefs.SetFloat(MAIN_VOLUME_KEY, volume);
        SetMainVolume();
    }

    /// <summary>
    /// Return the BGM volume set in the player pref. If no volume is set, 1 is returned.
    /// </summary>
    /// <returns>BGM volume</returns>
    public float GetBGMVolume()
    {
        return PlayerPrefs.GetFloat(BGM_VOLUME_KEY, 1f);
    }

    /// <summary>
    /// Changes the volume of the BGM
    /// </summary>
    /// <param name="volume">Set the volume of the BGM. The volume is clamped between 0 and 1</param>
    public void SetBGMVolume(float volume)
    {
        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, volume);
        SetBGMVolume();
    }

    /// <summary>
    /// Return the FX volume set in the player pref. If no volume is set, 1 is returned.
    /// </summary>
    /// <returns>FX volume</returns>
    public float GetFXVolume()
    {
        return PlayerPrefs.GetFloat(FX_VOLUME_KEY, 1f);
    }

    /// <summary>
    /// Changes the volume of the FXs
    /// </summary>
    /// <param name="volume">Set the volume of the FXs. The volume is clamped between 0 and 1</param>
    public void SetFXVolume(float volume)
    {
        PlayerPrefs.SetFloat(FX_VOLUME_KEY, volume);
        SetFXVolume();
    }

    /// <summary>
    /// Play an FX from the list
    /// </summary>
    /// <param name="fx">Type of the clip to play</param>
    public void PlayFX(E_FX fx)
    {
        foreach (S_FX elem in m_fxList)
        {
            if (elem.Type == fx)
            {
                _fxSource.PlayOneShot(elem.Clip);
                return;
            }
        }
    }
}

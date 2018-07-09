using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// SoundManager class. Manages the BGM and FX (volume, tracks, etc).
/// The object where the SoundManager is placed will automatically becomes the object that will play the FXs.
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
        BUTTON_CLICK,
        CURTAIN,
        THEATER
    }

    // Reference to the "mute fx" toggle
    [SerializeField]
    private ToggleIcon m_muteFXToggle;
    // Reference to the "mute BGM" toggle
    [SerializeField]
    private ToggleIcon m_muteBGMToggle;
    // List of FXs
    [SerializeField]
    private List<FX> m_fxList = new List<FX>();

    // Reference to the BGM source (camera)
    private AudioSource _bgmSource;
    // Reference to the FX source (this)
    private AudioSource _fxSource;

    // Main volume. BGM and FX volumes are multiplied with this.
    private float _mainVolume;

	void Start ()
    {
        _fxSource = GetComponent<AudioSource>();
        _mainVolume = GetMainVolume();

        InitSceneVolumes();
        m_muteBGMToggle.SetState(!_bgmSource.mute);
        m_muteFXToggle.SetState(!_fxSource.mute);

        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) =>
        {
            InitSceneVolumes();
        };
    }

    /// <summary>
    /// Initialize the volume on the audiosources of the scene
    /// </summary>
    private void InitSceneVolumes()
    {
        _bgmSource = Camera.main.GetComponent<AudioSource>();
        _bgmSource.volume = GetBGMVolume();
        _bgmSource.mute = PlayerPrefs.HasKey(BGM_MUTE_KEY) ? (PlayerPrefs.GetInt(BGM_MUTE_KEY) != 0) : false;
        _fxSource.volume = GetFXVolume();
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
        m_muteBGMToggle.SetState(!state);
    }

    /// <summary>
    /// Toggles the mute state of the BGM
    /// </summary>
    public void ToggleBGM()
    {
        MuteBGM(!_bgmSource.mute);
    }

    /// <summary>
    /// Mutes or unmutes the FXs
    /// </summary>
    /// <param name="state">Mute the FXs if true, unmute otherwise</param>
    public void MuteFX(bool state)
    {
        _fxSource.mute = state;
        PlayerPrefs.SetInt(FX_MUTE_KEY, state ? 1 : 0);
        m_muteFXToggle.SetState(!_fxSource.mute);
    }

    /// <summary>
    /// Toggles the mute state of the FXs
    /// </summary>
    public void ToggleFX()
    {
        MuteFX(!_fxSource.mute);
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
        _mainVolume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat(MAIN_VOLUME_KEY, _mainVolume);
        SetBGMVolume(GetBGMVolume());
        SetFXVolume(GetFXVolume());
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
        volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, volume);
        if (_bgmSource) _bgmSource.volume = volume * _mainVolume;
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
        volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat(FX_VOLUME_KEY, volume);
        if (_fxSource) _fxSource.volume = volume * _mainVolume;
    }

    /// <summary>
    /// Returns a FX AudioClip from the list.
    /// </summary>
    /// <param name="fx">Type of the clip to return</param>
    /// <returns></returns>
    public AudioClip GetFX(E_FX fx)
    {
        foreach (FX data in m_fxList)
        {
            if (data.FXType == fx)
            {
                return data.Clip;
            }
        }
        return null;
    }
}

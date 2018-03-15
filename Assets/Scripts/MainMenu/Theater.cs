using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Theater class. Manage the theater UI across the scenes.
/// The theater UI serves as transition screen and add an overlay depending on the scenes.
/// As the main UI shared across scenes, the Theater class is a singleton and unique.
/// </summary>
[RequireComponent(typeof(Animator))]
public class Theater : MonoBehaviour
{
    // Reference to the loading text
    [SerializeField]
    private GameObject _loadingText;

    // Reference to the animator
    private Animator _curtains;

    // Is the theater currently opened ?
    private bool _isOpen = true;
    public bool IsOpen
    {
        get { return _isOpen; }
    }

    // Callback triggered by the closed animation once the main curtain is closed
    public delegate void Callback();
    public Callback CurtainCloseActions;

    // Instance of the theater UI. There can only be one.
    private static Theater instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.parent.gameObject);
        }
        else if (instance != this)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    /// <summary>
    /// Initialisations.
    /// </summary>
    private void Start()
    {
        _curtains = GetComponent<Animator>();
        // Once a new scene is loaded, it is necessary for the canvas to get a reference to the new main camera.
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => {
            transform.parent.GetComponent<Canvas>().worldCamera = Camera.main;
        };
    }

    /// <summary>
    /// Display the "Loading" text
    /// </summary>
    public void ShowLoading()
    {
        _loadingText.SetActive(true);
    }

    /// <summary>
    /// Hide the "Loading" text
    /// </summary>
    public void HideLoading()
    {
        _loadingText.SetActive(false);
    }

    /// <summary>
    /// Pull in the theater and close the curtain.
    /// </summary>
    public void CloseCurtains()
    {
        _curtains.SetBool("Theater", true);
        _curtains.SetBool("Curtain", true);
    }

    /// <summary>
    /// Open the curtain.
    /// </summary>
    /// <param name="removeTheater">If set to true, slide out the theater from the view.</param>
    public void OpenCurtains(bool removeTheater = false)
    {
        _isOpen = true;
        _curtains.SetBool("Curtain", false);
        if (removeTheater) _curtains.SetBool("Theater", false);
    }

    /// <summary>
    /// Callback used in the close animation to trigger all registered close callbacks.
    /// </summary>
    public void CurtainClosed()
    {
        _isOpen = false;
        if (CurtainCloseActions != null)
        {
            CurtainCloseActions();
            CurtainCloseActions = null;
        }
    }
}

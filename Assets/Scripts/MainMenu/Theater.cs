using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Theater : MonoBehaviour
{
    // Reference to the loading text
    [SerializeField]
    private GameObject _loadingText;

    // Reference to the animator
    private Animator _curtains;

    private bool _isOpen = true;
    public bool IsOpen
    {
        get { return _isOpen; }
    }

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

    private void Start()
    {
        _curtains = GetComponent<Animator>();
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => {
            transform.parent.GetComponent<Canvas>().worldCamera = Camera.main;
        };
    }

    public void ShowLoading()
    {
        _loadingText.SetActive(true);
    }

    public void HideLoading()
    {
        _loadingText.SetActive(false);
    }

    public void CloseCurtains(bool slideIn = true)
    {
        if (slideIn) _curtains.SetTrigger("SlideIn");
        _curtains.SetTrigger("Close");
    }

    public void OpenCurtains(bool slideOut = true)
    {
        _isOpen = true;
        if (slideOut) _curtains.SetTrigger("SlideOut");
        _curtains.SetTrigger("Open");
    }

    // Animation callback
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Reference to the MainUI
    [SerializeField]
    private GameObject m_mainUI;
    // Reference to the credits UI
    [SerializeField]
    private GameObject m_credits;
    // Reference to the level selection UI
    [SerializeField]
    private GameObject m_levelSelection;

    // Reference to the theater script
    private Theater _theater;

    // If true, pressing escape bring the player back to the main menu
    private bool _enableEscMenu;

    private void Start()
    {
        _theater = GameObject.FindGameObjectWithTag("Theater").GetComponent<Theater>();
        if (!_theater.IsOpen)
        {
            _theater.OpenCurtains();
        }
    }

    private void Update()
    {
        if (_enableEscMenu && Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnMenu();
        }
    }

    public void Play()
    {
        _theater.CurtainCloseActions += () => {
            SceneManager.LoadSceneAsync("Levels");
            _theater.ShowLoading();
        };
        _theater.CloseCurtains();
    }

    public void Editor()
    {
        _theater.CurtainCloseActions += () => {
            SceneManager.LoadSceneAsync("LevelEditor");
            _theater.ShowLoading();
        };
        _theater.CloseCurtains();
    }

    public void Options()
    {
        Debug.Log("OPTIONS");
    }

    public void LevelSelect()
    {
        _theater.CloseCurtains();
        _theater.CurtainCloseActions += () => {
            m_mainUI.SetActive(false);
            m_levelSelection.SetActive(true);
            _theater.OpenCurtains(false);
            _enableEscMenu = true;
        };
    }

    public void Credits()
    {
        _theater.CloseCurtains();
        _theater.CurtainCloseActions += () => {
            m_mainUI.SetActive(false);
            m_credits.SetActive(true);
            _theater.OpenCurtains(false);
            _enableEscMenu = true;
        };
    }

    public void ReturnMenu()
    {
        _enableEscMenu = false;
        _theater.CloseCurtains(false);
        _theater.CurtainCloseActions += () => {
            m_credits.SetActive(false);
            m_levelSelection.SetActive(false);
            m_mainUI.SetActive(true);
            _theater.OpenCurtains();
        };
    }

    public void Quit()
    {
        Debug.Log("QUIT");
    }
}

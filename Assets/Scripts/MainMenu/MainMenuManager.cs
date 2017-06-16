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

    // Reference to the theater script
    private Theater _theater;
    // Reference to the theater animator
    private Animator _theaterAnim;

    // If true, pressing escape bring the player back to the main menu
    private bool _enableEscMenu;

    private void Start()
    {
        _theater = GameObject.FindGameObjectWithTag("Theater").GetComponent<Theater>();
        _theaterAnim = _theater.GetComponent<Animator>();
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
        _theaterAnim.SetTrigger("SlideIn");
        _theaterAnim.SetTrigger("Close");
    }

    public void Editor()
    {
        _theater.CurtainCloseActions += () => {
            SceneManager.LoadSceneAsync("LevelEditor");
            _theater.ShowLoading();
        };
        _theaterAnim.SetTrigger("SlideIn");
        _theaterAnim.SetTrigger("Close");
    }

    public void Options()
    {
        Debug.Log("OPTIONS");
    }

    public void Credits()
    {
        _theaterAnim.SetTrigger("SlideIn");
        _theaterAnim.SetTrigger("Close");
        _theater.CurtainCloseActions += () => {
            m_mainUI.SetActive(false);
            m_credits.SetActive(true);
            _theaterAnim.SetTrigger("Open");
            _enableEscMenu = true;
        };
    }

    public void ReturnMenu()
    {
        _enableEscMenu = false;
        _theaterAnim.SetTrigger("Close");
        _theater.CurtainCloseActions += () => {
            m_credits.SetActive(false);
            m_mainUI.SetActive(true);
            _theaterAnim.SetTrigger("Open");
            _theaterAnim.SetTrigger("SlideOut");
        };
    }

    public void Quit()
    {
        Debug.Log("QUIT");
    }
}

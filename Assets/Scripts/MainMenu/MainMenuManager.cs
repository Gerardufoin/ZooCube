using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Reference to the theater animator
    private Animator _theater;

    private void Start()
    {
        _theater = GameObject.FindGameObjectWithTag("Theater").GetComponent<Animator>();
    }

    public void Play()
    {
        Debug.Log("PLAY");
        _theater.SetTrigger("SlideIn");
        _theater.SetTrigger("Close");
        SceneManager.LoadSceneAsync("Levels");
    }

    public void Editor()
    {
        Debug.Log("EDITOR");
        _theater.SetTrigger("SlideIn");
        _theater.SetTrigger("Close");
    }

    public void Options()
    {
        Debug.Log("OPTIONS");
    }

    public void Credits()
    {
        Debug.Log("CREDITS");
        _theater.SetTrigger("SlideIn");
        _theater.SetTrigger("Close");
        _theater.SetTrigger("Open");
    }

    public void Quit()
    {
        Debug.Log("QUIT");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UINavigationButtons : MonoBehaviour
{
    [SerializeField]
    private GameObject m_mainMenuButtons;
    [SerializeField]
    private GameObject m_levelsButtons;

    /// <summary>
    /// On enable, swap to the appropriate navigation buttons based on the scene
    /// </summary>
    private void OnEnable()
    {
        bool mainMenu = (SceneManager.GetActiveScene().buildIndex == 0);
        m_mainMenuButtons.SetActive(mainMenu);
        m_levelsButtons.SetActive(!mainMenu);
    }

    /// <summary>
    /// Go back to the level select from a level by calling the GameManager ToLevelSelect method
    /// </summary>
    public void LevelToSelection()
    {
        FindObjectOfType<GameManager>().ToLevelSelect();
    }

    /// <summary>
    /// Go back to the main menu from a level by calling the GameManager ExitToMenu method
    /// </summary>
    public void LevelToMainMenu()
    {
        FindObjectOfType<GameManager>().ExitToMenu();
    }
}

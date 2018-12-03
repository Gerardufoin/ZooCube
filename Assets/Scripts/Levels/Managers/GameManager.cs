using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// GameManager class. Main manager of a level. Contains the callbacks to transition between scenes and dictates when the game start or end.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Reference to the victory screen in the canvas
    [SerializeField]
    private Animator m_victoryScreen;
    // Reference to the star particle system
    [SerializeField]
    private ParticleSystem m_starParticles;

    // Reference to the current level
    private Level _currentLevel;
    // Reference to the current user
    private GameDatas.UserDatas _currentUser;

    // Reference to the theater UI
    private Theater _theater;
    // Reference to the RecepeterManager script
    private RecepterManager _recepterManager;

    // Number of placed pieces by the player
    private int _placedPieces;

    /// <summary>
    /// Disable the victory screen at the very begining
    /// </summary>
    private void Awake()
    {
        m_victoryScreen.gameObject.SetActive(false);
    }

    /// <summary>
    /// Opening of the curtains.
    /// </summary>
    void Start ()
    {
        _currentLevel = GameDatas.Instance.CurrentLevel;
        _currentUser = GameDatas.Instance.Users[GameDatas.Instance.CurrentUserIdx];
        GameObject theater = GameObject.FindGameObjectWithTag("Theater");
        if (theater != null)
        {
            _theater = theater.GetComponent<Theater>();
            _theater.OpenCurtains();
        }
        _recepterManager = FindObjectOfType<RecepterManager>();
	}

    /// <summary>
    /// Called when the player place a piece. If all the pieces are placed, we call the victory screen and close the curtains
    /// </summary>
    public void PlacePiece()
    {
        _placedPieces++;
        if (_placedPieces >= _recepterManager.RecepterCount)
        {
            m_starParticles.Play();
            m_victoryScreen.gameObject.SetActive(true);
            m_victoryScreen.GetComponent<AudioSource>().Play();
            m_victoryScreen.SetTrigger("Appear");
            if (_currentUser.OfficialLevelsProgression < _currentLevel.Number)
            {
                GameDatas.Instance.Users[GameDatas.Instance.CurrentUserIdx].OfficialLevelsProgression = _currentLevel.Number;
                GameDatas.Instance.SaveUsers();
            }
            if (_theater) _theater.CloseCurtains();
        }
    }

    /// <summary>
    /// Button callback to return to the main menu.
    /// </summary>
    public void ExitToMenu()
    {
        if (!_theater) return;
        m_victoryScreen.gameObject.SetActive(false);
        if (_theater.IsOpen)
        {
            _theater.CurtainCloseActions += () => {
                SceneManager.LoadSceneAsync("MainMenu");
                _theater.ShowLoading();
            };
            _theater.CloseCurtains();
        }
        else
        {
            SceneManager.LoadSceneAsync("MainMenu");
            _theater.ShowLoading();
        }
    }

    /// <summary>
    /// Button callback to load the next level.
    /// </summary>
    public void NextLevel()
    {
        if (!_theater) return;

        GameDatas.Instance.CurrentLevel = GameDatas.Instance.GetLevel(GameDatas.Instance.CurrentLevel.Number + 1);
        if (_theater.IsOpen)
        {
            _theater.CurtainCloseActions += () => {
                SceneManager.LoadSceneAsync("Levels");
                _theater.ShowLoading();
            };
            _theater.CloseCurtains();
        }
        else
        {
            SceneManager.LoadSceneAsync("Levels");
            _theater.ShowLoading();
        }
    }

    /// <summary>
    /// Button callback to return to the level select.
    /// </summary>
    public void ToLevelSelect()
    {
        MainMenuManager.SkipToLevelSelect = true;
        ExitToMenu();
    }
}

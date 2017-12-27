using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // Reference to the victory screen in the canvas
    [SerializeField]
    private Animator m_victoryScreen;
    // Reference to the star particle system
    [SerializeField]
    private ParticleSystem m_starParticles;

    // Reference to the theater UI
    private Theater _theater;
    // Reference to the RecepeterManager script
    private RecepterManager _recepterManager;

    // Number of placed pieces by the player
    private int _placedPieces;

    // We disable the victory screen
    private void Awake()
    {
        m_victoryScreen.gameObject.SetActive(false);
    }

    void Start ()
    {
        GameObject theater = GameObject.FindGameObjectWithTag("Theater");
        if (theater != null)
        {
            _theater = theater.GetComponent<Theater>();
            _theater.OpenCurtains(false);
        }
        _recepterManager = FindObjectOfType<RecepterManager>();
	}

    // Called when the player place a piece. If all the pieces are placed, we call the victory screen and close the curtains
    public void PlacePiece()
    {
        _placedPieces++;
        if (_placedPieces >= _recepterManager.RecepterCount)
        {
            m_starParticles.Play();
            m_victoryScreen.gameObject.SetActive(true);
            m_victoryScreen.SetTrigger("Appear");
            if (_theater) _theater.CloseCurtains(false);
            Debug.Log("FINISHED :D");
        }
    }

    public void ExitToMenu()
    {
        if (!_theater) return;
        if (_theater.IsOpen)
        {
            _theater.CurtainCloseActions += () => {
                SceneManager.LoadSceneAsync("MainMenu");
                _theater.ShowLoading();
            };
            _theater.CloseCurtains(false);
        }
        else
        {
            SceneManager.LoadSceneAsync("MainMenu");
            _theater.ShowLoading();
        }
    }
}

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // Reference to the victory screen in the canvas
    [SerializeField]
    private Animator m_victoryScreen;
    // Reference to the star particle system
    [SerializeField]
    private ParticleSystem m_starParticles;

    // Reference on the animator of the maingui
    private Animator _curtains;
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
            _curtains = theater.GetComponent<Animator>();
            _curtains.SetTrigger("Open");
        }
        _recepterManager = FindObjectOfType<RecepterManager>();
        // Opening of the curtains
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
            if (_curtains) _curtains.SetTrigger("Close");
            Debug.Log("FINISHED :D");
        }
    }
}

using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Animator m_curtains;

    private RecepterManager _recepterManager;

    private int _placedPieces;

	// Use this for initialization
	void Start ()
    {
        _recepterManager = FindObjectOfType<RecepterManager>();
        m_curtains.SetTrigger("Open");
	}

    public void PlacePiece()
    {
        _placedPieces++;
        if (_placedPieces >= _recepterManager.RecepterCount)
        {
            m_curtains.SetTrigger("Close");
            Debug.Log("FINISHED :D");
        }
    }
}

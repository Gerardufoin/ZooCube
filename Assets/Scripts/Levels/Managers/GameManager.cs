using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_curtains;

    private RecepterManager _recepterManager;

    private Vector3 _curtainsDest;
    private float _startTimer = 0.0f;

    private int _placedPieces;

	// Use this for initialization
	void Start ()
    {
        _recepterManager = FindObjectOfType<RecepterManager>();
        _curtainsDest = m_curtains.transform.position;
        _curtainsDest.y = 11.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (_startTimer < 1.0f)
        {
            _startTimer += Time.deltaTime;
            return ;
        }

	    if (m_curtains.transform.position != _curtainsDest)
        {
            m_curtains.transform.position = Vector3.MoveTowards(m_curtains.transform.position, _curtainsDest, 8.0f * Time.deltaTime);
        }
	}

    public void PlacePiece()
    {
        _placedPieces++;
        if (_placedPieces >= _recepterManager.RecepterCount)
        {
            Debug.Log("FINISHED :D");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_MouseActions
{
    IDLING = 0,
    CREATING,
    MOVING,
    SELECTING
}

public class PiecesManager : MonoBehaviour
{
    #region Properties

    #region Editor
    [SerializeField]
    private GameObject m_areaSelection;
    #endregion

    #region SelectionMesh
    private Mesh _selectionMesh;

    private Rect _selectionBounds = new Rect();
    private Vector3[] _vertices = new Vector3[4];
    private Vector2[] _uvs = new Vector2[4];
    private int[] _tris = new int[6];
    #endregion

    private E_MouseActions _currentAction;

    private delegate void D_CurrentAction();
    private Dictionary<E_MouseActions, D_CurrentAction> _actionUpdate = new Dictionary<E_MouseActions, D_CurrentAction>();

    private List<EditablePiece> _selectedPieces = new List<EditablePiece>();

    private Vector3 _selectionStart;

    private int _editableLayer = 1 << 9;

    #endregion

    // Use this for initialization
    void Start ()
    {
        #region SelectionMesh
        _selectionMesh = new Mesh();

        _vertices[0] = Vector3.zero;
        _vertices[1] = Vector3.zero;
        _vertices[2] = Vector3.zero;
        _vertices[3] = Vector3.zero;

        _uvs[0] = new Vector2(0, 1);
        _uvs[1] = new Vector2(1, 1);
        _uvs[2] = new Vector2(1, 0);
        _uvs[3] = new Vector2(0, 0);

        _tris[0] = 0;
        _tris[1] = 1;
        _tris[2] = 2;
        _tris[3] = 2;
        _tris[4] = 3;
        _tris[5] = 0;
        #endregion

        m_areaSelection.GetComponent<MeshFilter>().mesh = _selectionMesh;
        m_areaSelection.GetComponent<MeshRenderer>().sortingLayerName = "Forground";

        _actionUpdate.Add(E_MouseActions.IDLING, new D_CurrentAction(IdlingUpdate));
        _actionUpdate.Add(E_MouseActions.CREATING, new D_CurrentAction(CreatingUpdate));
        _actionUpdate.Add(E_MouseActions.MOVING, new D_CurrentAction(MovingUpdate));
        _actionUpdate.Add(E_MouseActions.SELECTING, new D_CurrentAction(SelectingUpdate));
    }

    // Update is called once per frame
    void Update ()
    {
        _actionUpdate[_currentAction]();
	}

    private void IdlingUpdate()
    {
        // Selection
        if (Input.GetMouseButtonDown(0))
        {
            _selectionStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D result = Physics2D.OverlapPoint(_selectionStart, _editableLayer);
            if (result != null)
            {
                _selectedPieces.Add(result.GetComponent<EditablePiece>());
                _currentAction = E_MouseActions.MOVING;
            }
            else
            {
                // Prepare selection
                _currentAction = E_MouseActions.SELECTING;
                m_areaSelection.gameObject.SetActive(true);
                UpdateSelectionMesh();
            }
        }
    }

    private void CreatingUpdate()
    {
    }

    private void MovingUpdate()
    {
        Debug.Log("MOVING: " + _selectedPieces.Count);
        _selectedPieces.Clear();
        _currentAction = E_MouseActions.IDLING;
    }

    private void SelectingUpdate()
    {
        if (Input.GetMouseButtonUp(0))
        {
            m_areaSelection.gameObject.SetActive(false);

            Collider2D[] results = Physics2D.OverlapAreaAll(_selectionStart, Camera.main.ScreenToWorldPoint(Input.mousePosition), _editableLayer);
            for (uint i = 0; i < results.Length; ++i)
            {
                _selectedPieces.Add(results[i].GetComponent<EditablePiece>());
            }
            _currentAction = E_MouseActions.MOVING;
            if (_selectedPieces.Count == 0)
            {
                _currentAction = E_MouseActions.IDLING;
            }
            return;
        }
        UpdateSelectionMesh();
    }

    private void UpdateSelectionMesh()
    {
        _selectionMesh.Clear();

        Vector3 selectionEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _selectionBounds.x = Mathf.Min(_selectionStart.x, selectionEnd.x);
        _selectionBounds.y = Mathf.Max(_selectionStart.y, selectionEnd.y);
        _selectionBounds.width = Mathf.Abs(_selectionStart.x - selectionEnd.x);
        _selectionBounds.height = Mathf.Abs(_selectionStart.y - selectionEnd.y);

        _vertices[0].x = _selectionBounds.x;
        _vertices[0].y = _selectionBounds.y;
        _vertices[1].x = _selectionBounds.x + _selectionBounds.width;
        _vertices[1].y = _selectionBounds.y;
        _vertices[2].x = _selectionBounds.x + _selectionBounds.width;
        _vertices[2].y = _selectionBounds.y - _selectionBounds.height;
        _vertices[3].x = _selectionBounds.x;
        _vertices[3].y = _selectionBounds.y - _selectionBounds.height;

        _selectionMesh.vertices = _vertices;
        _selectionMesh.triangles = _tris;
        _selectionMesh.uv = _uvs;
    }
}

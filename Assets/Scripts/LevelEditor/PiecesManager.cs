﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    private GameObject _selectionMove;

    private int _editableLayer = 1 << 9;

    #endregion

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

        _selectionMove = new GameObject("SelectionMoveParent");
        _selectionMove.transform.position = Vector3.zero;

        _actionUpdate.Add(E_MouseActions.IDLING, new D_CurrentAction(IdlingUpdate));
        _actionUpdate.Add(E_MouseActions.CREATING, new D_CurrentAction(CreatingUpdate));
        _actionUpdate.Add(E_MouseActions.MOVING, new D_CurrentAction(MovingUpdate));
        _actionUpdate.Add(E_MouseActions.SELECTING, new D_CurrentAction(SelectingUpdate));
    }

    void Update ()
    {
        _actionUpdate[_currentAction]();
	}

    private bool GetMouseButtonDown(int button)
    {
        return (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(button));
    }

    private bool GetMouseButtonUp(int button)
    {
        return (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonUp(button));
    }

    private void IdlingUpdate()
    {
        // Selection
        if (GetMouseButtonDown(0))
        {
            _selectionStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D result = Physics2D.OverlapPoint(_selectionStart, _editableLayer);
            if (result != null)
            {
                // Multi selection check
                bool selected = false;
                for (int i = 0; i < _selectedPieces.Count; ++i)
                {
                    if (_selectedPieces[i].gameObject == result.gameObject)
                    {
                        selected = true;
                    }
                }
                if (!selected)
                {
                    ClearSelection();
                    AddPieceToSelection(result.gameObject);
                }
                _selectionMove.transform.position = _selectionStart;
                for (int i = 0; i < _selectedPieces.Count; ++i)
                {
                    _selectedPieces[i].transform.parent = _selectionMove.transform;
                }
                _currentAction = E_MouseActions.MOVING;
            }
            else
            {
                // Prepare selection
                _currentAction = E_MouseActions.SELECTING;
                ClearSelection();
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
        _selectionMove.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (GetMouseButtonUp(0))
        {
            for (int i = 0; i < _selectedPieces.Count; ++i)
            {
                _selectedPieces[i].transform.parent = null;
            }
            _currentAction = E_MouseActions.IDLING;
        }
    }

    private void SelectingUpdate()
    {
        if (GetMouseButtonUp(0))
        {
            m_areaSelection.gameObject.SetActive(false);

            Collider2D[] results = Physics2D.OverlapAreaAll(_selectionStart, Camera.main.ScreenToWorldPoint(Input.mousePosition), _editableLayer);
            for (uint i = 0; i < results.Length; ++i)
            {
                AddPieceToSelection(results[i].gameObject);
            }
            // Going back to idling when the selection is done. Have to click again to start moving.
            _currentAction = E_MouseActions.IDLING;
            return;
        }
        UpdateSelectionMesh();
    }

    private void AddPieceToSelection(GameObject piece)
    {
        piece.GetComponent<SpriteRenderer>().sortingOrder = 2;
        _selectedPieces.Add(piece.GetComponent<EditablePiece>());
    }

    private void ClearSelection()
    {
        for (int i = 0; i < _selectedPieces.Count; ++i)
        {
            _selectedPieces[i].GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        _selectedPieces.Clear();
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

    public void ApplyPropertiesOnSelection(MaterialPropertyBlock properties)
    {
        for (int i = 0; i < _selectedPieces.Count; ++i)
        {
            _selectedPieces[i].SetShaderProperties(properties);
        }
    }
}

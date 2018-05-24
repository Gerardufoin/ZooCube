using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Available actions using the mouse.
/// </summary>
public enum E_MouseActions
{
    IDLING = 0,
    MOVING,
    SELECTING,
    SCALING
}

/// <summary>
/// Available directions of the scaling. Used as flags, can be combined.
/// </summary>
public enum E_ScaleDirections
{
    NONE    = 0,
    LEFT    = 1,
    RIGHT   = 2,
    UP      = 4,
    DOWN    = 8
}

/// <summary>
/// PiecesManager class. The manager class used to allow the user to manipulate EditablePieces in the level editor.
/// Unique in a scene.
/// </summary>
public class PiecesManager : MonoBehaviour
{
    #region Properties

    #region Editor
    [SerializeField]
    private GameObject m_areaSelection;
    [SerializeField]
    private LineRenderer m_selectionBorders;
    [SerializeField]
    private EditablePiece m_editablePiece;

    [SerializeField]
    private Texture2D m_cursorVertical;
    [SerializeField]
    private Texture2D m_cursorHorizontal;
    [SerializeField]
    private Texture2D m_cursorDiagonalLeft;
    [SerializeField]
    private Texture2D m_cursorDiagonalRight;
    [SerializeField]
    private Vector2 m_cursorOffset;
    #endregion

    #region SelectionMesh
    private Mesh _selectionMesh;

    private Rect _meshBounds = new Rect();
    private Vector3[] _vertices = new Vector3[4];
    private Vector2[] _uvs = new Vector2[4];
    private int[] _tris = new int[6];
    #endregion

    private E_MouseActions _currentAction;
    private E_ScaleDirections _scaleDirection;

    private delegate void D_CurrentAction();
    private Dictionary<E_MouseActions, D_CurrentAction> _actionUpdate = new Dictionary<E_MouseActions, D_CurrentAction>();

    private List<EditablePiece> _selectedPieces = new List<EditablePiece>();

    private Vector3 _selectionStart;
    private Vector3 _lastMousePosition;
    private GameObject _selectionContainer;
    private Rect _selectionBounds;

    private bool _releaseDown = false;

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

        _selectionContainer = new GameObject("SelectionContainer");
        _selectionContainer.transform.position = Vector3.zero;
        _selectionBounds = new Rect();

        _actionUpdate.Add(E_MouseActions.IDLING, new D_CurrentAction(IdlingUpdate));
        _actionUpdate.Add(E_MouseActions.MOVING, new D_CurrentAction(MovingUpdate));
        _actionUpdate.Add(E_MouseActions.SELECTING, new D_CurrentAction(SelectingUpdate));
        _actionUpdate.Add(E_MouseActions.SCALING, new D_CurrentAction(ScalingUpdate));
    }

    void Update ()
    {
        _actionUpdate[_currentAction]();
        _lastMousePosition = GetMouseCoordinates();
	}

    private bool GetMouseButtonDown(int button)
    {
        return (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(button));
    }

    private bool GetMouseButtonUp(int button)
    {
        // Commented Part: Enable mouse release on GUI so the selection area can end
        return (/*!EventSystem.current.IsPointerOverGameObject() && */Input.GetMouseButtonUp(button));
    }

    private Vector3 GetMouseCoordinates()
    {
        Vector3 coord = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        coord.z = 0;
        return (coord);
    }

    /// <summary>
    /// Position _selectionContainer at the right place based on _scaleDirection
    /// If _scaleDirection is none, _selectionContainer has to be place under the cursor. Otherwise, it has to be placed at the opposite bound of the scaling direction.
    /// Eg: If we are scaling left, _selectionContainer has to be place at the center-right bound of the selection
    /// </summary>
    private void SelectionPosition()
    {
        _selectionContainer.transform.position = _selectionStart;
        if (_scaleDirection != E_ScaleDirections.NONE)
        {
            if ((_scaleDirection & E_ScaleDirections.LEFT) != 0)
                _selectionContainer.transform.position = new Vector3(_selectionBounds.xMax, _selectionContainer.transform.position.y, _selectionContainer.transform.position.z);
            else if ((_scaleDirection & E_ScaleDirections.RIGHT) != 0)
                _selectionContainer.transform.position = new Vector3(_selectionBounds.xMin, _selectionContainer.transform.position.y, _selectionContainer.transform.position.z);

            if ((_scaleDirection & E_ScaleDirections.DOWN) != 0)
                _selectionContainer.transform.position = new Vector3(_selectionContainer.transform.position.x, _selectionBounds.yMax, _selectionContainer.transform.position.z);
            else if ((_scaleDirection & E_ScaleDirections.UP) != 0)
                _selectionContainer.transform.position = new Vector3(_selectionContainer.transform.position.x, _selectionBounds.yMin, _selectionContainer.transform.position.z);
        }
    }

    /// <summary>
    /// Prepare the selection for the move or scale action
    /// </summary>
    private void PrepareSelection()
    {
        SelectionPosition();
        for (int i = 0; i < _selectedPieces.Count; ++i)
        {
            _selectedPieces[i].transform.parent = _selectionContainer.transform;
        }
        _currentAction = (_scaleDirection != E_ScaleDirections.NONE ? E_MouseActions.SCALING : E_MouseActions.MOVING);
    }

    private void IdlingUpdate()
    {
        UpdateCursor();
        // Selection
        if (GetMouseButtonDown(0))
        {
            _selectionStart = GetMouseCoordinates();
            Collider2D result;
            if (_selectionBounds.Contains(_selectionStart))
            {
                PrepareSelection();
            }
            else if ((result = Physics2D.OverlapPoint(_selectionStart, _editableLayer)) != null)
            {
                ClearSelection();
                AddPieceToSelection(result.gameObject);
                PrepareSelection();
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

    private void MovingUpdate()
    {
        Vector2 mouse = GetMouseCoordinates();
        _selectionBounds.position += (mouse - (Vector2)_selectionContainer.transform.position);
        _selectionContainer.transform.position = mouse;
        UpdateSelectionBorders();
        if ((_releaseDown ? GetMouseButtonDown(0) : GetMouseButtonUp(0)))
        {
            _releaseDown = false;
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

            Collider2D[] results = Physics2D.OverlapAreaAll(_selectionStart, GetMouseCoordinates(), _editableLayer);
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

    private void ScalingUpdate()
    {
        Vector2 mouse = GetMouseCoordinates();
        Vector2 bounds = new Vector2(_selectionBounds.width, _selectionBounds.height);

        if ((_scaleDirection & E_ScaleDirections.LEFT) != 0)
            _selectionBounds.xMin += (mouse.x - _lastMousePosition.x);
        else if ((_scaleDirection & E_ScaleDirections.RIGHT) != 0)
            _selectionBounds.xMax += (mouse.x - _lastMousePosition.x);
        if ((_scaleDirection & E_ScaleDirections.UP) != 0)
            _selectionBounds.yMax += (mouse.y - _lastMousePosition.y);
        else if ((_scaleDirection & E_ScaleDirections.DOWN) != 0)
            _selectionBounds.yMin += (mouse.y - _lastMousePosition.y);

        float scaleX = _selectionContainer.transform.localScale.x + _selectionContainer.transform.localScale.x * ((_selectionBounds.width - bounds.x) / bounds.x);
        float scaleY = _selectionContainer.transform.localScale.y + _selectionContainer.transform.localScale.y * ((_selectionBounds.height - bounds.y) / bounds.y);
        _selectionContainer.transform.localScale = new Vector3(scaleX, scaleY, 1);
        UpdateSelectionBorders();

        if (GetMouseButtonUp(0))
        {
            for (int i = 0; i < _selectedPieces.Count; ++i)
            {
                _selectedPieces[i].transform.parent = null;
            }
            _selectionContainer.transform.localScale = Vector3.one;
            _currentAction = E_MouseActions.IDLING;
        }
    }

    private void UpdateSelectionBorders()
    {
        m_selectionBorders.SetPosition(0, new Vector3(_selectionBounds.xMin, _selectionBounds.yMin, 0));
        m_selectionBorders.SetPosition(1, new Vector3(_selectionBounds.xMin, _selectionBounds.yMax, 0));
        m_selectionBorders.SetPosition(2, new Vector3(_selectionBounds.xMax, _selectionBounds.yMax, 0));
        m_selectionBorders.SetPosition(3, new Vector3(_selectionBounds.xMax, _selectionBounds.yMin, 0));
    }

    private void AddPieceToSelection(GameObject piece)
    {
        piece.GetComponent<SpriteRenderer>().sortingOrder = 2;
        _selectedPieces.Add(piece.GetComponent<EditablePiece>());
        Collider2D collider = piece.GetComponent<Collider2D>();
        if (collider)
        {
            if (_selectedPieces.Count > 1)
            {
                _selectionBounds.xMin = Mathf.Min(_selectionBounds.xMin, collider.bounds.min.x);
                _selectionBounds.xMax = Mathf.Max(_selectionBounds.xMax, collider.bounds.max.x);
                _selectionBounds.yMin = Mathf.Min(_selectionBounds.yMin, collider.bounds.min.y);
                _selectionBounds.yMax = Mathf.Max(_selectionBounds.yMax, collider.bounds.max.y);
            }
            else
            {
                _selectionBounds.xMin = collider.bounds.min.x;
                _selectionBounds.xMax = collider.bounds.max.x;
                _selectionBounds.yMin = collider.bounds.min.y;
                _selectionBounds.yMax = collider.bounds.max.y;
                m_selectionBorders.gameObject.SetActive(true);
            }
            UpdateSelectionBorders();
        }
    }

    private void ClearSelection()
    {
        _selectionBounds = new Rect();
        m_selectionBorders.gameObject.SetActive(false);
        for (int i = 0; i < _selectedPieces.Count; ++i)
        {
            _selectedPieces[i].GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        _selectedPieces.Clear();
    }

    private void UpdateSelectionMesh()
    {
        _selectionMesh.Clear();

        Vector3 selectionEnd = GetMouseCoordinates();

        _meshBounds.x = Mathf.Min(_selectionStart.x, selectionEnd.x);
        _meshBounds.y = Mathf.Max(_selectionStart.y, selectionEnd.y);
        _meshBounds.width = Mathf.Abs(_selectionStart.x - selectionEnd.x);
        _meshBounds.height = Mathf.Abs(_selectionStart.y - selectionEnd.y);

        _vertices[0].x = _meshBounds.x;
        _vertices[0].y = _meshBounds.y;
        _vertices[1].x = _meshBounds.x + _meshBounds.width;
        _vertices[1].y = _meshBounds.y;
        _vertices[2].x = _meshBounds.x + _meshBounds.width;
        _vertices[2].y = _meshBounds.y - _meshBounds.height;
        _vertices[3].x = _meshBounds.x;
        _vertices[3].y = _meshBounds.y - _meshBounds.height;

        _selectionMesh.vertices = _vertices;
        _selectionMesh.triangles = _tris;
        _selectionMesh.uv = _uvs;
    }

    private void UpdateCursor()
    {
        Vector2 mousePosition = GetMouseCoordinates();
        Collider2D result = Physics2D.OverlapPoint(mousePosition, _editableLayer);
        if (result)
        {
            bool up = mousePosition.y >= result.bounds.max.y - 0.1f && mousePosition.y <= result.bounds.max.y;
            bool down = mousePosition.y <= result.bounds.min.y + 0.1f && mousePosition.y >= result.bounds.min.y;
            bool left = mousePosition.x <= result.bounds.min.x + 0.1f && mousePosition.x >= result.bounds.min.x;
            bool right = mousePosition.x >= result.bounds.max.x - 0.1f && mousePosition.x <= result.bounds.max.x;
            if ((up && left) || (down && right))
                Cursor.SetCursor(m_cursorDiagonalLeft, m_cursorOffset, CursorMode.Auto);
            else if ((down && left) || (up && right))
                Cursor.SetCursor(m_cursorDiagonalRight, m_cursorOffset, CursorMode.Auto);
            else if (up || down)
                Cursor.SetCursor(m_cursorVertical, m_cursorOffset, CursorMode.Auto);
            else if (left || right)
                Cursor.SetCursor(m_cursorHorizontal, m_cursorOffset, CursorMode.Auto);
            else
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            _scaleDirection = (up ? E_ScaleDirections.UP : 0) | (down ? E_ScaleDirections.DOWN : 0) | (left ? E_ScaleDirections.LEFT : 0) | (right ? E_ScaleDirections.RIGHT : 0);
        }
        else if (_scaleDirection != E_ScaleDirections.NONE)
        {
            _scaleDirection = E_ScaleDirections.NONE;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    public void ApplyFaceOnSelection(Animal animal)
    {
        for (int i = 0; i < _selectedPieces.Count; ++i)
        {
            _selectedPieces[i].Animal = animal;
        }
    }

    public void ApplyShapeOnSelection(Shape shape)
    {
        for (int i = 0; i < _selectedPieces.Count; ++i)
        {
            _selectedPieces[i].Shape = shape;
        }
    }

    public void CreatePiece(Animal face, Shape shape)
    {
        ClearSelection();
        _selectionStart = GetMouseCoordinates();
        _selectionContainer.transform.position = _selectionStart;

        GameObject piece = Instantiate(m_editablePiece.gameObject, _selectionStart, m_editablePiece.transform.rotation);
        piece.transform.localScale = Vector3.one * 0.5f;
        piece.GetComponent<EditablePiece>().PresetProperties(face, shape);

        AddPieceToSelection(piece);
        piece.transform.parent = _selectionContainer.transform;
        _releaseDown = true;
        _currentAction = E_MouseActions.MOVING;
    }
}

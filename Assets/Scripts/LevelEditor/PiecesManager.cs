using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_areaSelection;

    private List<EditablePiece> _selectedPieces = new List<EditablePiece>();

    private bool _selecting = false;
    private Vector3 _selectionStart;

    private Mesh _selectionMesh;

	// Use this for initialization
	void Start ()
    {
        _selectionMesh = new Mesh();

        m_areaSelection.GetComponent<MeshFilter>().mesh = _selectionMesh;
        m_areaSelection.GetComponent<MeshRenderer>().sortingLayerName = "Forground";
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetMouseButtonDown(0))
        {
            _selecting = true;
            _selectionStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            _selecting = false;
        }
        UpdateSelectionMesh();
	}

    private void UpdateSelectionMesh()
    {
        if (!_selecting)
        {
            m_areaSelection.gameObject.SetActive(false);
            return;
        }

        m_areaSelection.gameObject.SetActive(true);

        _selectionMesh.Clear();

        Vector3 selectionEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Rect selection = new Rect(Mathf.Min(_selectionStart.x, selectionEnd.x), Mathf.Max(_selectionStart.y, selectionEnd.y),
                                  Mathf.Abs(_selectionStart.x - selectionEnd.x), Mathf.Abs(_selectionStart.y - selectionEnd.y));

        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(selection.x, selection.y, 0);
        vertices[1] = new Vector3(selection.x + selection.width, selection.y, 0);
        vertices[2] = new Vector3(selection.x + selection.width, selection.y - selection.height, 0);
        vertices[3] = new Vector3(selection.x, selection.y - selection.height, 0);

        Vector2[] uvs = new Vector2[vertices.Length];
        uvs[0] = new Vector2(0, 1);
        uvs[1] = new Vector2(1, 1);
        uvs[2] = new Vector2(1, 0);
        uvs[3] = new Vector2(0, 0);

        int[] tris = new int[6];
        tris[0] = 0;
        tris[1] = 1;
        tris[2] = 2;
        tris[3] = 2;
        tris[4] = 3;
        tris[5] = 0;

        _selectionMesh.vertices = vertices;
        _selectionMesh.triangles = tris;
        _selectionMesh.uv = uvs;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum E_PieceFace
{
    MONKEY = 0,
    RABBIT,
    PENGUIN
}

public enum E_PieceShape
{
    SQUARE = 0,
    CIRCLE,
    TRIANGLE
}

public class PieceCreationManager : MonoBehaviour
{
    #region Structures
    public struct S_FaceInfos
    {
        public Texture Face;
        public Color BackgroundColor;
        public Color BorderColor;

        public S_FaceInfos(Texture face, Color backC, Color bordC)
        {
            Face = face;
            BackgroundColor = backC;
            BorderColor = bordC;
        }
    }

    public struct S_ShapeInfos
    {
        public Texture Shape;
        public float ImageScale;
        public float ImageXOffset;
        public float ImageYOffset;
        public float BorderXOffset;
        public float BorderYOffset;

        public S_ShapeInfos(Texture shape, float imgS, float ixo, float iyo, float bxo, float byo)
        {
            Shape = shape;
            ImageScale = imgS;
            ImageXOffset = ixo;
            ImageYOffset = iyo;
            BorderXOffset = bxo;
            BorderYOffset = byo;
        }
    }
    #endregion

    [SerializeField]
    private ToggleGroup m_facesToggles;
    [SerializeField]
    private ToggleGroup m_shapesToggles;

    [SerializeField]
    private List<Color> m_backgroundColors = new List<Color>();
    [SerializeField]
    private List<Color> m_borderColors = new List<Color>();

    private PiecesManager _piecesManager;

    private Dictionary<E_PieceFace, S_FaceInfos> _faces = new Dictionary<E_PieceFace, S_FaceInfos>();
    private Dictionary<E_PieceShape, S_ShapeInfos> _shapes = new Dictionary<E_PieceShape, S_ShapeInfos>();

    private E_PieceFace _currentFace = E_PieceFace.MONKEY;
    private E_PieceShape _currentShape = E_PieceShape.SQUARE;

    private void Start()
    {
        _piecesManager = FindObjectOfType<PiecesManager>();

        // Faces informations
        _faces.Add(E_PieceFace.MONKEY, new S_FaceInfos(Resources.Load<Texture2D>("Faces/Monkey_Face"), m_backgroundColors[(int)E_PieceFace.MONKEY], m_borderColors[(int)E_PieceFace.MONKEY]));
        _faces.Add(E_PieceFace.RABBIT, new S_FaceInfos(Resources.Load<Texture2D>("Faces/Rabbit_Face"), m_backgroundColors[(int)E_PieceFace.RABBIT], m_borderColors[(int)E_PieceFace.RABBIT]));
        _faces.Add(E_PieceFace.PENGUIN, new S_FaceInfos(Resources.Load<Texture2D>("Faces/Penguin_Face"), m_backgroundColors[(int)E_PieceFace.PENGUIN], m_borderColors[(int)E_PieceFace.PENGUIN]));

        // Shapes informations
        _shapes.Add(E_PieceShape.SQUARE, new S_ShapeInfos(Resources.Load<Texture2D>("Maps/White_BG/Square_Map"), 1f, 0, 0, 0, 0));
        _shapes.Add(E_PieceShape.CIRCLE, new S_ShapeInfos(Resources.Load<Texture2D>("Maps/White_BG/Circle_Map"), 1f, 0, 0, 0, 0));
        _shapes.Add(E_PieceShape.TRIANGLE, new S_ShapeInfos(Resources.Load<Texture2D>("Maps/White_BG/Triangle_Map"), 0.6f, 0, -0.35f, 0, 0.02f));
    }

    public void ChangePieceFace(int e)
    {
        if (!m_facesToggles.transform.GetChild(e).GetComponent<Toggle>().isOn) return;

        _currentFace = (E_PieceFace)e;
        _piecesManager.ApplyPropertiesOnSelection(CreateMaterialPropertiesBlock());
    }

    public void ChangePieceShape(int e)
    {
        if (!m_shapesToggles.transform.GetChild(e).GetComponent<Toggle>().isOn) return;

        _currentShape = (E_PieceShape)e;
        _piecesManager.ApplyPropertiesOnSelection(CreateMaterialPropertiesBlock());
    }

    private MaterialPropertyBlock CreateMaterialPropertiesBlock()
    {
        MaterialPropertyBlock properties = new MaterialPropertyBlock();

        properties.SetTexture("_MainTex", _faces[_currentFace].Face);
        properties.SetColor("_BackgroundColor", _faces[_currentFace].BackgroundColor);
        properties.SetColor("_BorderColor", _faces[_currentFace].BorderColor);
        properties.SetFloat("_BorderWidth", 0.1f);
        properties.SetTexture("_ShapeMask", _shapes[_currentShape].Shape);
        properties.SetFloat("_ImageScale", _shapes[_currentShape].ImageScale);
        properties.SetFloat("_ImageXOffset", _shapes[_currentShape].ImageXOffset);
        properties.SetFloat("_ImageYOffset", _shapes[_currentShape].ImageYOffset);
        properties.SetFloat("_BorderXOffset", _shapes[_currentShape].BorderXOffset);
        properties.SetFloat("_BorderYOffset", _shapes[_currentShape].BorderYOffset);

        return (properties);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    #region Json Structure
    [System.Serializable]
    public struct PieceInfos
    {
        public Texture Face;
        public Texture Shape;
        public Color BackgroundColor;
        public Color BorderColor;
        public Vector2 Position;
        public Vector2 Scale;
        public float ImageScale;
        public float ImageXOffset;
        public float ImageYOffset;
        public float BorderWidth;
        public float BorderXOffset;
        public float BorderYOffset;
    }

    [System.Serializable]
    public class LevelInfos
    {
        public List<PieceInfos> Pieces = new List<PieceInfos>();
    }
    #endregion

    [SerializeField]
    private Recepter m_recepterPrefab;
    [SerializeField]
    private MovablePiece m_piecePrefab;
    [SerializeField]
    private BoxCollider2D m_playzone;
    [SerializeField]
    private List<Transform> m_spawns = new List<Transform>();

    private Vector3 _recepterScale = new Vector3(0.0f, 0.0f, 1.0f);
    private Vector3 _pieceScale = new Vector3(0.0f, 0.0f, 0.1f);

    private Vector3 _zonePosition;
    private Vector3 _zoneSize;

    // Use this for initialization
    void Start ()
    {
        // Shuffle !
        for (int i = 0; i < m_spawns.Count; ++i)
        {
            int j = Random.Range(0, i);
            if (j != i)
            {
                Transform tmp = m_spawns[i];
                m_spawns[i] = m_spawns[j];
                m_spawns[j] = tmp;
            }
        }
        _zonePosition = m_playzone.bounds.min;
        _zoneSize = m_playzone.bounds.size;
        this.LoadLevel("{\"Pieces\":[{\"Face\":{\"instanceID\":10696},\"Shape\":{\"instanceID\":10304},\"BackgroundColor\":{\"r\":0.6313725709915161,\"g\":0.40000003576278689,\"b\":0.22352942824363709,\"a\":1.0},\"BorderColor\":{\"r\":0.38431376218795779,\"g\":0.22352942824363709,\"b\":0.10588236153125763,\"a\":1.0},\"Position\":{\"x\":0.5,\"y\":0.5},\"Scale\":{\"x\":0.3,\"y\":0.3},\"ImageScale\":0.0,\"ImageXOffset\":0.0,\"ImageYOffset\":0.0,\"BorderWidth\":0.0,\"BorderXOffset\":0.0,\"BorderYOffset\":0.0}]}");
    }

    public void LoadLevel(string json)
    {
        LevelInfos infos = JsonUtility.FromJson<LevelInfos>(json);

        for (int i = 0; i < infos.Pieces.Count && i < m_spawns.Count; ++i)
        {
            Debug.Log(infos.Pieces[i].Position.x);
            Vector3 position = new Vector3(_zonePosition.x + _zoneSize.x * infos.Pieces[i].Position.x, _zonePosition.y + _zoneSize.y * infos.Pieces[i].Position.y, 0f);

            Recepter recepter = Instantiate(m_recepterPrefab);
            recepter.transform.localScale = _recepterScale + (Vector3)infos.Pieces[i].Scale;
            recepter.transform.position = position;
            SetRecepterShader(recepter, infos.Pieces[i]);

            MovablePiece piece = Instantiate(m_piecePrefab);
            piece.transform.localScale = _pieceScale + (Vector3)infos.Pieces[i].Scale;
            piece.transform.position = m_spawns[i].position;
            SetPieceShader(piece, infos.Pieces[i]);
        }
    }

    private void SetRecepterShader(Recepter recepter, PieceInfos infos)
    {
        Renderer renderer = recepter.GetComponent<Renderer>();

        MaterialPropertyBlock properties = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(properties);
        //properties.SetTexture("_MainTex", infos.Shape);
        renderer.SetPropertyBlock(properties);
    }

    private void SetPieceShader(MovablePiece piece, PieceInfos infos)
    {
        Renderer renderer = piece.GetComponent<Renderer>();

        MaterialPropertyBlock properties = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(properties);
        //properties.SetTexture("_MainTex", infos.Face);
        //properties.SetTexture("_ShapeMask", infos.Shape);
        properties.SetColor("_BackgroundColor", infos.BackgroundColor);
        properties.SetColor("_BorderColor", infos.BorderColor);
        properties.SetFloat("_ImageScale", infos.ImageScale);
        properties.SetFloat("_BorderWidth", infos.BorderWidth);
        properties.SetFloat("_BorderXOffset", infos.BorderXOffset);
        properties.SetFloat("_BorderYOffset", infos.BorderYOffset);
        properties.SetFloat("_ImageXOffset", infos.ImageXOffset);
        properties.SetFloat("_ImageYOffset", infos.ImageYOffset);
        renderer.SetPropertyBlock(properties);
    }
}

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

    private Vector3 _recepterScale = new Vector3(0.0f, 0.0f, 1.0f);
    private Vector3 _pieceScale = new Vector3(0.0f, 0.0f, 0.1f);

    // Use this for initialization
    void Start ()
    {
        this.LoadLevel("{\"Pieces\":[{\"Face\":{\"instanceID\":10696},\"Shape\":{\"instanceID\":10304},\"BackgroundColor\":{\"r\":0.6313725709915161,\"g\":0.40000003576278689,\"b\":0.22352942824363709,\"a\":1.0},\"BorderColor\":{\"r\":0.38431376218795779,\"g\":0.22352942824363709,\"b\":0.10588236153125763,\"a\":1.0},\"Position\":{\"x\":0.0,\"y\":0.0},\"Scale\":{\"x\":0.3,\"y\":0.3},\"ImageScale\":0.0,\"ImageXOffset\":0.0,\"ImageYOffset\":0.0,\"BorderWidth\":0.0,\"BorderXOffset\":0.0,\"BorderYOffset\":0.0}]}");
    }

    public void LoadLevel(string json)
    {
        LevelInfos infos = JsonUtility.FromJson<LevelInfos>(json);

        for (int i = 0; i < infos.Pieces.Count; ++i)
        {
            Recepter recepter = Instantiate(m_recepterPrefab);
            recepter.transform.localScale = _recepterScale + (Vector3)infos.Pieces[i].Scale;
            recepter.transform.Translate(new Vector3(-1, 1));
            MovablePiece piece = Instantiate(m_piecePrefab);
            piece.transform.localScale = _pieceScale + (Vector3)infos.Pieces[i].Scale;
            piece.transform.Translate(new Vector3(1, 1));
        }
    }
}

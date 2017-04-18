using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Json Structure
// Structure containing all the needed info for a specific piece
[System.Serializable]
public struct PieceInfos
{
    public string FacePath;
    public string ShapePath;
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

// Class containing all the infos to save/load for a level
[System.Serializable]
public class LevelInfos
{
    public List<PieceInfos> Pieces = new List<PieceInfos>();
}
#endregion

// Load the pieces of a specific level
public class LevelLoader : MonoBehaviour
{
    // Used to display list of list in the editor
    [System.Serializable]
    public class SpawnWrapper
    {
        public List<Transform> Spawns = new List<Transform>();
    }

    // Recepter prefab to instantiate at runtime
    [SerializeField]
    private Recepter m_recepterPrefab;
    // MovablePiece prefab to instantiate at runtime
    [SerializeField]
    private MovablePiece m_piecePrefab;
    // Reference to the playzone
    [SerializeField]
    private BoxCollider2D m_playzone;
    // Piece spawners separated in different lists to vary positions
    [SerializeField]
    private List<SpawnWrapper> m_spawns = new List<SpawnWrapper>();

    // Default scale of a piece/recepter
    private Vector3 _defaultScale = new Vector3(0.0f, 0.0f, 1.0f);

    // Shortcut to the position of the playzone
    private Vector3 _zonePosition;
    // Shortcut to the size of the playzone
    private Vector3 _zoneSize;

    // Amount of spawners in all the different lists
    private int _spawnsSize;

    // Use this for initialization
    void Start ()
    {
        // Shuffle !
        for (int i = 0; i < m_spawns.Count; ++i)
        {
            _spawnsSize += m_spawns[i].Spawns.Count;
            for (int j = 0; j < m_spawns[i].Spawns.Count; ++j)
            {
                int r = Random.Range(0, j);
                if (r != j)
                {
                    Transform tmp = m_spawns[i].Spawns[j];
                    m_spawns[i].Spawns[j] = m_spawns[i].Spawns[r];
                    m_spawns[i].Spawns[r] = tmp;
                }
            }
        }
        _zonePosition = m_playzone.bounds.min;
        _zoneSize = m_playzone.bounds.size;
        this.LoadLevel(System.IO.File.ReadAllText(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/test.json"));
    }

    public void LoadLevel(string json)
    {
        LevelInfos infos = JsonUtility.FromJson<LevelInfos>(json);

        int colIdx = 0;
        int rowIdx = 0;

        for (int i = 0; i < infos.Pieces.Count && i < _spawnsSize; )
        {
            if (rowIdx < m_spawns[colIdx].Spawns.Count)
            {
                Vector3 position = new Vector3(_zonePosition.x + _zoneSize.x * infos.Pieces[i].Position.x, _zonePosition.y + _zoneSize.y * infos.Pieces[i].Position.y, -1f);
                Vector3 scale = _defaultScale + (Vector3)infos.Pieces[i].Scale;
                scale.x *= _zoneSize.x;
                scale.y *= _zoneSize.y;

                Recepter recepter = Instantiate(m_recepterPrefab);
                recepter.Id = i;
                recepter.transform.localScale = scale;
                recepter.transform.position = position;
                recepter.PieceInfos = infos.Pieces[i];

                MovablePiece piece = Instantiate(m_piecePrefab);
                piece.Id = i;
                piece.transform.localScale = scale;
                piece.transform.position = m_spawns[colIdx].Spawns[rowIdx].position;
                piece.PieceInfos = infos.Pieces[i];
                ++i;
            }
            if (++colIdx > m_spawns.Count)
            {
                colIdx = 0;
                rowIdx++;
            }
        }
    }
}

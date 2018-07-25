using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// LevelLoader class. Load the pieces of the selected level.
/// </summary>
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
    // Mirror hint prefab to instantiate at runtime
    [SerializeField]
    private Hint m_hintPrefab;

    // Reference to the simple playzone
    [SerializeField]
    private BoxCollider2D m_playZone;
    // Reference to the mirror playzone
    [SerializeField]
    private BoxCollider2D m_mirrorPlayZone;
    // Reference to the mirror hint zone
    [SerializeField]
    private BoxCollider2D m_mirrorHintZone;
    // Piece spawners separated in different lists to vary positions
    [SerializeField]
    private List<SpawnWrapper> m_spawns = new List<SpawnWrapper>();

    [SerializeField]
    private Transform m_tutorials;

    // Default scale of a piece/recepter
    private Vector3 _defaultScale = new Vector3(0.0f, 0.0f, 1.0f);

    // Shortcut to the position of the playzone
    private Vector3 _zonePosition;
    // Shortcut to the size of the playzone
    private Vector3 _zoneSize;
    // Shortcut to the position of the hint zone
    private Vector3 _hintPosition;
    // Shortcut to the size of the hint zone
    private Vector3 _hintSize;

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
        if (GameDatas.Instance.CurrentLevel.Type == E_LevelType.HINT)
        {
            m_mirrorPlayZone.gameObject.SetActive(true);
            m_playZone.gameObject.SetActive(false);
            _zonePosition = m_mirrorPlayZone.bounds.min;
            _zoneSize = m_mirrorPlayZone.bounds.size;
            _hintPosition = m_mirrorHintZone.bounds.min;
            _hintSize = m_mirrorHintZone.bounds.size;
        }
        else
        {
            m_playZone.gameObject.SetActive(true);
            m_mirrorPlayZone.gameObject.SetActive(false);
            _zonePosition = m_playZone.bounds.min;
            _zoneSize = m_playZone.bounds.size;
        }
        foreach (Transform child in m_tutorials)
        {
            child.gameObject.SetActive(child.GetSiblingIndex() + 1 == GameDatas.Instance.CurrentLevel.Tutorial);
        }
        this.LoadLevel(GameDatas.Instance.CurrentLevel.JsonData);
    }

    public void LoadLevel(string json)
    {
        GameDatas.LevelDatas infos = JsonUtility.FromJson<GameDatas.LevelDatas>(json);

        int colIdx = 0;
        int rowIdx = 0;

        for (int i = 0; i < infos.Pieces.Count && i < _spawnsSize; )
        {
            if (rowIdx < m_spawns[colIdx].Spawns.Count)
            {
                Animal animal = GameDatas.Instance.GetAnimalData(infos.Pieces[i].Animal);
                Shape shape = GameDatas.Instance.GetShapeData(infos.Pieces[i].Shape);
                Vector3 position = new Vector3(_zonePosition.x + _zoneSize.x * infos.Pieces[i].Position.x, _zonePosition.y + _zoneSize.y * infos.Pieces[i].Position.y, -1f);
                Vector3 scale = _defaultScale + (Vector3)infos.Pieces[i].Scale;
                scale.x *= _zoneSize.x;
                scale.y *= _zoneSize.y;

                Recepter recepter = Instantiate(m_recepterPrefab);
                recepter.Id = i;
                recepter.transform.localScale = scale;
                recepter.transform.position = position;
                recepter.Shape = shape;
                recepter.Borders = infos.Pieces[i].RecepterBorders;

                MovablePiece piece = Instantiate(m_piecePrefab);
                piece.Id = i;
                piece.transform.localScale = scale;
                piece.transform.position = m_spawns[colIdx].Spawns[rowIdx].position;
                piece.Animal = animal;
                piece.Shape = shape;

                if (GameDatas.Instance.CurrentLevel.Type == E_LevelType.HINT)
                {
                    Hint hint = Instantiate(m_hintPrefab);
                    hint.transform.localScale = scale;
                    if (GameDatas.Instance.CurrentLevel.MirrorHint)
                    {
                        hint.transform.position = new Vector3(m_mirrorHintZone.bounds.max.x - _hintSize.x * infos.Pieces[i].Position.x, _hintPosition.y + _hintSize.y * infos.Pieces[i].Position.y, -1f);
                    }
                    else
                    {
                        hint.transform.position = new Vector3(_hintPosition.x + _hintSize.x * infos.Pieces[i].Position.x, _hintPosition.y + _hintSize.y * infos.Pieces[i].Position.y, -1f);
                    }
                    hint.Shape = shape;
                }
                ++i;
            }
            if (++colIdx >= m_spawns.Count)
            {
                colIdx = 0;
                rowIdx++;
            }
        }
    }

    public void HideTutorial()
    {
        m_tutorials.GetChild(GameDatas.Instance.CurrentLevel.Tutorial - 1).gameObject.SetActive(false);
    }
}

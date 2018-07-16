using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// LevelSaver class. Used to convert a level into json.
/// </summary>
public class LevelSaver : MonoBehaviour
{
    // Creation zone. Will be scaled to the zone in the play level at load time
    [SerializeField]
    BoxCollider2D m_editZone;

    // EditablePiece prefab to instantiate at runtime
    [SerializeField]
    private EditablePiece m_editablePiecePrefab;

    // Default scale of a piece
    private Vector3 _defaultScale = new Vector3(0.0f, 0.0f, 1.0f);

    // Shortcut to the position of the editzone
    private Vector3 _zonePosition;
    // Shortcut to the size of the editzone
    private Vector3 _zoneSize;

    void Start()
    {
        _zonePosition = m_editZone.bounds.min;
        _zoneSize = m_editZone.bounds.size;
    }

    /// <summary>
    /// Called to display and copy the level's json into the clipboard. When called, the function will collect all the EditablePieces in the scene to convert them into json.
    /// </summary>
    public void CopyLevel()
    {
        // We create the level container
        GameDatas.LevelDatas levelInfos = new GameDatas.LevelDatas();
        levelInfos.Pieces = new List<GameDatas.PieceInfos>();

        // Getting all the pieces of the scene
        EditablePiece[] _pieces = FindObjectsOfType<EditablePiece>();

        // If there is no piece we pop a warning to the user (TODO)
        if (_pieces.Length == 0)
        {
            Debug.Log("Empty level");
            return;
        }
        
        // We add all the pieces infos to the recepter
        for (int i = 0; i < _pieces.Length; ++i)
        {
            levelInfos.Pieces.Add(_pieces[i].GetPieceInfos(m_editZone));
        }
        string s = JsonUtility.ToJson(levelInfos);
        Debug.Log(s);
        CopyToClipboard(s);
    }

    /// <summary>
    /// Copy a string to the clipboard
    /// </summary>
    /// <param name="s">The string to copy</param>
    private void CopyToClipboard(string s)
    {
        TextEditor te = new TextEditor();
        te.text = s;
        te.SelectAll();
        te.Copy();
    }

    public void LoadLevel(string json)
    {
        GameDatas.LevelDatas infos = JsonUtility.FromJson<GameDatas.LevelDatas>(json);

        FindObjectOfType<PiecesManager>().ClearSelection();
        foreach (EditablePiece pieces in FindObjectsOfType<EditablePiece>())
        {
            Destroy(pieces.gameObject);
        }
        for (int i = 0; i < infos.Pieces.Count; ++i)
        {
            Animal animal = GameDatas.Instance.GetAnimalData(infos.Pieces[i].Animal);
            Shape shape = GameDatas.Instance.GetShapeData(infos.Pieces[i].Shape);
            Vector3 position = new Vector3(_zonePosition.x + _zoneSize.x * infos.Pieces[i].Position.x, _zonePosition.y + _zoneSize.y * infos.Pieces[i].Position.y, -1f);
            Vector3 scale = _defaultScale + (Vector3)infos.Pieces[i].Scale;
            scale.x *= _zoneSize.x;
            scale.y *= _zoneSize.y;

            EditablePiece piece = Instantiate(m_editablePiecePrefab);
            piece.transform.localScale = scale;
            piece.transform.position = position;
            piece.PresetProperties(animal, shape, infos.Pieces[i].RecepterBorders);
        }
    }
}

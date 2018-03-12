using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// LevelSaver class. Used to save a custom level created through the level editor.
/// </summary>
public class LevelSaver : MonoBehaviour
{
    // Creation zone. Will be scaled to the zone in the play level at load time
    [SerializeField]
    BoxCollider2D m_editZone;

    /// <summary>
    /// Called to save the level. When called, the function will collect all the EditablePieces in the scene and save their informations using the GameDatas singleton.
    /// </summary>
    public void SaveLevel()
    {
        // We get the edited level using its hash (will be used when the load is implemented in the level editor)
        GameDatas.LevelDatas levelInfos = GameDatas.Instance.GetLevelByHash("");
        if (levelInfos.Hash == "")
        {
            levelInfos.Pieces = new List<GameDatas.PieceInfos>();
            levelInfos.Hash = System.DateTime.Now.Millisecond.ToString();
        }

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
        // Save. Will have to do some checks when the loading will be available
        GameDatas.Instance.Levels.Add(levelInfos);
        GameDatas.Instance.SaveCustomLevels();
    }
}

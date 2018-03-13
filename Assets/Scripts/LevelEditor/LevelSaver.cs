using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// LevelSaver class. Used to save a custom level created through the level editor.
/// </summary>
public class LevelSaver : MonoBehaviour
{
    // Output the level's json in the console instead of saving the data as a custom level. (Editor only)
    [SerializeField]
    private bool m_outputJson;
    // Creation zone. Will be scaled to the zone in the play level at load time
    [SerializeField]
    BoxCollider2D m_editZone;

#if !UNITY_EDITOR
    private void Start()
    {
        m_outputJson = false;
    }
#endif

    /// <summary>
    /// Called to save the level. When called, the function will collect all the EditablePieces in the scene and save their informations using the GameDatas singleton.
    /// </summary>
    public void SaveLevel()
    {
        // We get the edited level using its hash (will be used when the load is implemented in the level editor)
        GameDatas.LevelDatas levelInfos = GameDatas.Instance.GetLevelByHash("");

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
        if (m_outputJson)
        {
            Debug.Log(JsonUtility.ToJson(levelInfos));
        }
        else
        {
            // Save. Will have to do some checks when the loading will be available
            GameDatas.Instance.CustomLevels.Add(levelInfos);
            GameDatas.Instance.SaveCustomLevels();
        }
    }
}

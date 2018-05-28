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

    /// <summary>
    /// Called to display and copy the level's json into the clipboard. When called, the function will collect all the EditablePieces in the scene to convert them into json.
    /// </summary>
    public void CopyLevel()
    {
        // We get the edited level using its hash
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSaver : MonoBehaviour
{
    // Creation zone. Will be scaled to the zone in the play level at load time
    [SerializeField]
    BoxCollider2D m_editZone;

    // Called to save the level
    public void SaveLevel()
    {
        // Class to transform into a json
        LevelInfos levelInfos = new LevelInfos();
        // Getting all the pieces of the scene
        EditablePiece[] _pieces = FindObjectsOfType<EditablePiece>();

        // If there is no piece we pop a warning to the user (TODO)
        if (_pieces.Length == 0)
        {
            Debug.Log("Empty level");
            return;
        }

        // We add all the pieces infos to the LevelInfos
        for (int i = 0; i < _pieces.Length; ++i)
        {
            levelInfos.Pieces.Add(_pieces[i].GetPieceInfos(m_editZone));
        }
        // We write the json to the save file
        System.IO.File.WriteAllText(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/test.json", JsonUtility.ToJson(levelInfos));
    }
}

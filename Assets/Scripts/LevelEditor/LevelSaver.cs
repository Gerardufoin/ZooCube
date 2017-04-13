using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSaver : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D m_editZone;

    public void SaveLevel()
    {
        LevelInfos levelInfos = new LevelInfos();
        EditablePiece[] _pieces = FindObjectsOfType<EditablePiece>();

        if (_pieces.Length == 0)
        {
            Debug.Log("Empty level");
            return;
        }

        for (int i = 0; i < _pieces.Length; ++i)
        {
            levelInfos.Pieces.Add(_pieces[i].GetPieceInfos(m_editZone));
        }
        System.IO.File.WriteAllText(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/test.json", JsonUtility.ToJson(levelInfos));
    }
}

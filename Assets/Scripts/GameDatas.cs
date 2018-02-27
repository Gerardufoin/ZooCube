using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameDatas : MonoBehaviour
{
    [System.Serializable]
    public struct UserDatas
    {
        public string Username;

        public UserDatas(string name = "")
        {
            Username = name;
        }
    }

    [System.Serializable]
    public struct LevelDatas
    {
        public string Name;
    }

    public const string UsersFilename = "Users.dat";
    public const string LevelsFilename = "Levels.dat";

    private List<UserDatas> _users = new List<UserDatas>();
    private List<LevelDatas> _levels = new List<LevelDatas>();

    public static GameDatas Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Save(string filename, object data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + filename);
        bf.Serialize(file, data);
        file.Close();
    }

    public void SaveUsers()
    {
        Save(UsersFilename, _users);
    }

    public void SaveCustomLevels()
    {
        Save(LevelsFilename, _levels);
    }

    public void SaveAll()
    {
        SaveUsers();
        SaveCustomLevels();
    }

    private void Load<T>(string filename, ref T data)
    {
        if (File.Exists(Application.persistentDataPath + "/" + filename))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + filename, FileMode.Open);
            data = (T)bf.Deserialize(file);
            file.Close();
        }
    }

    public void LoadUsers()
    {
        Load<List<UserDatas>>(UsersFilename, ref _users);
    }

    public void LoadCustomLevels()
    {
        Load<List<LevelDatas>>(LevelsFilename, ref _levels);
    }

    public void LoadAll()
    {
        LoadUsers();
        LoadCustomLevels();
    }
}

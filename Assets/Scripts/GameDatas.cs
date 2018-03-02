using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameDatas : MonoBehaviour
{
    public enum AnimalType
    {
        NONE = 0,
        MONKEY,
        RABBIT,
        PENGUIN,
        PIG
    }

    [System.Serializable]
    public struct UserDatas
    {
        public string Username;
        public AnimalType Icon;
        // Hash of the completed levels
        public List<string> FinishedLevels;

        public UserDatas(string name, AnimalType icon)
        {
            Username = name;
            Icon = icon;
            FinishedLevels = new List<string>();
        }
    }

    [System.Serializable]
    public struct LevelDatas
    {
        public string Name;
        public string Hash;
        // level infos
    }

    [HideInInspector]
    public const string UsersFilename = "Users.dat";
    [HideInInspector]
    public const string LevelsFilename = "Levels.dat";

    public List<ScriptableAnimal> ZooAnimals = new List<ScriptableAnimal>();

    public List<UserDatas> Users = new List<UserDatas>();
    public List<LevelDatas> Levels = new List<LevelDatas>();

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
        Save(UsersFilename, Users);
    }

    public void SaveCustomLevels()
    {
        Save(LevelsFilename, Levels);
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
        Load<List<UserDatas>>(UsersFilename, ref Users);
    }

    public void LoadCustomLevels()
    {
        Load<List<LevelDatas>>(LevelsFilename, ref Levels);
    }

    public void LoadAll()
    {
        LoadUsers();
        LoadCustomLevels();
    }

    public ScriptableAnimal GetAnimalData(AnimalType animal)
    {
        if (ZooAnimals.Count == 0)
                return null;
        for (int i = 0; i < ZooAnimals.Count; ++i)
        {
            if (ZooAnimals[i].Type == animal)
            {
                return ZooAnimals[i];
            }
        }
        return ZooAnimals[0];
    }
}

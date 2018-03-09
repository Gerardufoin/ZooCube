using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameDatas : MonoBehaviour
{
    #region DataTypes
    public enum AnimalType
    {
        NONE = 0,
        MONKEY,
        RABBIT,
        PENGUIN,
        PIG
    }

    public enum ShapeType
    {
        NONE = 0,
        CIRCLE,
        SQUARE,
        TRIANGLE,
        SQUARE_ALT
    }

    [System.Serializable]
    public struct UserDatas
    {
        public string Username;
        public AnimalType Icon;
        public int OfficialLevelsProgression;
        // Hash of the completed custom levels
        public List<string> FinishedCustomLevels;

        public UserDatas(string name, AnimalType icon)
        {
            Username = name;
            Icon = icon;
            OfficialLevelsProgression = 0;
            FinishedCustomLevels = new List<string>();
        }
    }

    [System.Serializable]
    public struct CustomLevelDatas
    {
        public string Name;
        public string Hash;
        // level infos
    }
    #endregion

    [HideInInspector]
    public const string UsersFilename = "Users.dat";
    [HideInInspector]
    public const string CustomLevelsFilename = "CustomLevels.dat";

    public List<Animal> ZooAnimals = new List<Animal>();
    public List<Shape> ZooShapes = new List<Shape>();

    [HideInInspector]
    public int CurrentUserIdx;
    public List<UserDatas> Users = new List<UserDatas>();
    public List<CustomLevelDatas> Levels = new List<CustomLevelDatas>();

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
        Save(CustomLevelsFilename, Levels);
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
        Load<List<CustomLevelDatas>>(CustomLevelsFilename, ref Levels);
    }

    public void LoadAll()
    {
        LoadUsers();
        LoadCustomLevels();
    }

    public Animal GetAnimalData(AnimalType animal)
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

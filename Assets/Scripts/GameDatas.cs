using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameDatas : MonoBehaviour
{
    #region DataTypes
    public enum E_AnimalType
    {
        NONE = 0,
        MONKEY,
        RABBIT,
        PENGUIN,
        PIG
    }

    public enum E_ShapeType
    {
        NONE = 0,
        CIRCLE,
        SQUARE,
        TRIANGLE,
        SQUARE_ALT,
        RIGHT_TRIANGLE
    }

    [System.Serializable]
    public class UserDatas
    {
        public string Username;
        public E_AnimalType Icon;
        public int OfficialLevelsProgression;

        public UserDatas(string name, E_AnimalType icon)
        {
            Username = name;
            Icon = icon;
            OfficialLevelsProgression = 0;
        }
    }

    // Structure containing all the needed informations to recreate a specific piece
    [System.Serializable]
    public struct PieceInfos
    {
        public E_AnimalType Animal;
        public E_ShapeType Shape;
        public Vector2 Position;
        public Vector2 Scale;
    }

    [System.Serializable]
    public class LevelDatas
    {
        public string Name;
        public List<PieceInfos> Pieces;
    }
    #endregion

    [HideInInspector]
    public const string UsersFilename = "Users.dat";

    public List<Animal> ZooAnimals = new List<Animal>();
    public List<Shape> ZooShapes = new List<Shape>();
    public List<Level> ZooLevels = new List<Level>();

    [HideInInspector]
    public int CurrentUserIdx;
    [HideInInspector]
    public Level CurrentLevel;
    public List<UserDatas> Users = new List<UserDatas>();

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

    public Animal GetAnimalData(E_AnimalType animal)
    {
        if (ZooAnimals.Count == 0) return null;

        for (int i = 0; i < ZooAnimals.Count; ++i)
        {
            if (ZooAnimals[i].Type == animal)
            {
                return ZooAnimals[i];
            }
        }
        return ZooAnimals[0];
    }

    public Shape GetShapeData(E_ShapeType shape)
    {
        if (ZooShapes.Count == 0) return null;

        for (int i = 0; i < ZooShapes.Count; ++i)
        {
            if (ZooShapes[i].Type == shape)
            {
                return ZooShapes[i];
            }
        }
        return ZooShapes[0];
    }

    public Level GetLevel(int nb)
    {
        nb -= 1;
        return (nb < ZooLevels.Count ? ZooLevels[nb] : ZooLevels[0]);
    }
}

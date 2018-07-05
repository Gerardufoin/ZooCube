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
    public List<Language> Languages = new List<Language>();

    [HideInInspector]
    public int CurrentUserIdx;
    [HideInInspector]
    public Level CurrentLevel;
    public List<UserDatas> Users = new List<UserDatas>();

    // Current language of the game
    private E_Language _currentLanguage;
    // Current Language file converted to a dictionnary
    private Dictionary<E_TranslationKey, string> _currentLocalization = new Dictionary<E_TranslationKey, string>();

    public static GameDatas Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetLocalization((E_Language)PlayerPrefs.GetInt(Options.LOCAL_KEY, (int)E_Language.ENGLISH), true);
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

    /// <summary>
    /// Set the loaded localization of the game. Replace the _currentLocalization dictionnary with the new translation
    /// </summary>
    /// <param name="lang">Selected language</param>
    /// <param name="forceInit">If true, set the localization even if lang is the same as _currentLanguage</param>
    public void SetLocalization(E_Language lang, bool forceInit = false)
    {
        if (lang == _currentLanguage && !forceInit) return;

        for (int i = 0; i < Languages.Count; ++i)
        {
            if (Languages[i].Name == lang)
            {
                _currentLanguage = lang;
                _currentLocalization.Clear();
                foreach (Localization item in Languages[i].Translation)
                {
                    _currentLocalization.Add(item.Key, item.Value);
                }
                foreach (LocalizeText text in FindObjectsOfType<LocalizeText>())
                {
                    text.Localize();
                }
                return;
            }
        }
    }

    /// <summary>
    /// Get the translation for a specific key
    /// </summary>
    /// <param name="key">Selected translation key</param>
    /// <returns></returns>
    public string GetLocalization(E_TranslationKey key)
    {
        return _currentLocalization[key];
    }
}

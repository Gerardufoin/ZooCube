using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserCreation : MonoBehaviour
{
    private const int MAX_USERNAME_LENGTH = 35;

    [SerializeField]
    private GameObject m_iconPrefab;
    [SerializeField]
    private Transform m_avatarPanel;
    [SerializeField]
    private TMP_InputField m_usernameInputField;

    private LevelSelectManager _levelSelectManager;

    private string _currentUsername;
    private GameDatas.AnimalType _currentIcon;

    private void Start()
    {
        _levelSelectManager = GameObject.FindObjectOfType<LevelSelectManager>();
        for (int i = 0; i < GameDatas.Instance.ZooAnimals.Count; ++i)
        {
            ScriptableAnimal animal = GameDatas.Instance.ZooAnimals[i];
            GameObject newIcon = GameObject.Instantiate(m_iconPrefab, m_avatarPanel);
            newIcon.GetComponent<Image>().color = animal.Color;
            newIcon.GetComponent<Button>().onClick.AddListener(() => { SelectIcon(animal.Type); });
            newIcon.transform.Find("Avatar").GetComponent<Image>().sprite = animal.Face;
        }
    }

    public void ResetProfile()
    {
        _currentUsername = "";
        _currentIcon = GameDatas.AnimalType.NONE;
        m_usernameInputField.text = "";
    }

    public void OnUsernameUpdate(string username)
    {
        if (username.Length > MAX_USERNAME_LENGTH)
        {
            username = username.Substring(0, MAX_USERNAME_LENGTH);
            m_usernameInputField.text = username;
        }
        _currentUsername = username;
    }

    public void SelectIcon(GameDatas.AnimalType icon)
    {
        _currentIcon = icon;
    }

    public void CreateUser()
    {
        if (_currentUsername.Length == 0)
        {
            Debug.Log("No username provided.");
        }
        else if (_currentIcon == GameDatas.AnimalType.NONE)
        {
            Debug.Log("No avatar selected.");
        }
        else
        {
            GameDatas.Instance.Users.Add(new GameDatas.UserDatas(_currentUsername, _currentIcon));
            GameDatas.Instance.SaveUsers();
            Debug.Log("User '" + _currentUsername + "' added.");
            _levelSelectManager.NewUserPanel(false);
        }
    }
}

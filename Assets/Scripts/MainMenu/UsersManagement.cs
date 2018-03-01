using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UsersManagement : MonoBehaviour
{
    private const int MAX_USERNAME_LENGTH = 35;

    [SerializeField]
    private TMP_InputField m_usernameInputField;

    private LevelSelectManager _levelSelectManager;

    private string _currentUsername;
    private GameDatas.UserIcon _currentIcon;

    private void Start()
    {
        _levelSelectManager = GameObject.FindObjectOfType<LevelSelectManager>();
    }

    public void ResetProfile()
    {
        _currentUsername = "";
        _currentIcon = GameDatas.UserIcon.NONE;
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

    public void SelectIcon(int icon)
    {
        _currentIcon = (GameDatas.UserIcon)icon;
    }

    public void CreateUser()
    {
        if (_currentUsername.Length == 0)
        {
            Debug.Log("No username provided.");
        }
        else if (_currentIcon == GameDatas.UserIcon.NONE)
        {
            Debug.Log("No avatar selected.");
        }
        else
        {
            GameDatas.Instance.Users.Add(new GameDatas.UserDatas(_currentUsername, _currentIcon));
            GameDatas.Instance.SaveUsers();
            _levelSelectManager.NewUserPanel(false);
            Debug.Log("User '" + _currentUsername + "' added.");
        }
    }
}

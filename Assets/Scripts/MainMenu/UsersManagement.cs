using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UsersManagement : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField m_usernameInputField;

    private string _currentUsername;
    private GameDatas.UserIcon _currentIcon;

    public void ResetProfile()
    {
        _currentUsername = "";
        _currentIcon = GameDatas.UserIcon.NONE;
        m_usernameInputField.text = "";
    }

    public void OnUsernameUpdate(string username)
    {
        _currentUsername = username;
    }

    public void SelectIcon(GameDatas.UserIcon icon)
    {
        _currentIcon = icon;
    }

    public void CreateUser()
    {
        if (_currentUsername.Length > 0 && _currentIcon != GameDatas.UserIcon.NONE)
        {
            GameDatas.Instance.Users.Add(new GameDatas.UserDatas(_currentUsername, _currentIcon));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator;

    private UsersManagement _usersManagement;

    private void Start()
    {
        _usersManagement = GameObject.FindObjectOfType<UsersManagement>();
    }

    public void NewUserPanel(bool state)
    {
        m_animator.SetBool("NewUserPanel", state);
        if (state == false)
        {
            _usersManagement.ResetProfile();
        }
    }

    public void LevelSelectPanel(bool state)
    {
        m_animator.SetBool("LevelSelectPanel", state);
    }
}

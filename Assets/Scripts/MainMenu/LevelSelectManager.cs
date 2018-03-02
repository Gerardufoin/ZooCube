using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator;

    private UserCreation _userCreation;
    private UserSelect _userSelect;

    private void Start()
    {
        _userCreation = GetComponentInChildren<UserCreation>(true);
    }

    public void NewUserPanel(bool state)
    {
        m_animator.SetBool("NewUserPanel", state);
        if (state == false)
        {
            _userCreation.ResetProfile();
        }
    }

    public void LevelSelectPanel(bool state)
    {
        m_animator.SetBool("LevelSelectPanel", state);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField]
    private Transform m_levels;
    [SerializeField]
    private List<Color> m_difficultyColors;

    public void OnEnable()
    {
        GameDatas.UserDatas user = GameDatas.Instance.Users[GameDatas.Instance.CurrentUserIdx];
        for (int i = 0; i < m_levels.childCount; ++i)
        {
            Transform level = m_levels.GetChild(i);
            Transform levelNumber = level.GetChild(0);
            Transform levelLock = level.GetChild(1);
            Transform levelStar = level.GetChild(2);
            Button btn = level.GetComponent<Button>();
            bool levelAvailable = (i <= user.OfficialLevelsProgression);

            level.GetComponent<Image>().color = (levelAvailable ? m_difficultyColors[(i / 10 < m_difficultyColors.Count ? i / 10 : m_difficultyColors.Count)] : Color.white);
            btn.enabled = levelAvailable;
            btn.onClick.RemoveAllListeners();
            if (levelAvailable)
            {
                int id = i;
                btn.onClick.AddListener(() =>
                {
                    GameDatas.Instance.CurrentLevel = GameDatas.Instance.GetLevel(id + 1);
                    FindObjectOfType<MainMenuManager>().Play();
                });
            }
            else
            {
                btn.GetComponent<ButtonFX>().enabled = false;
            }
            levelNumber.gameObject.SetActive(levelAvailable);
            levelLock.gameObject.SetActive(!levelAvailable);
            levelStar.gameObject.SetActive(user.OfficialLevelsProgression > i);
        }
    }
}

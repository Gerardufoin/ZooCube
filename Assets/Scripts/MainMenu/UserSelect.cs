﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserSelect : MonoBehaviour
{
    public const int MAX_SLOTS_BY_PANEL = 8;

    [SerializeField]
    private GameObject m_userSlotPrefab;
    [SerializeField]
    private Transform m_slotsPanel;
    [SerializeField]
    private GameObject m_leftArrow;
    [SerializeField]
    private GameObject m_rightArrow;

    private LevelSelectManager _levelSelectManager;

    private int _slotPageIdx;

    private void Start()
    {
        _levelSelectManager = FindObjectOfType<LevelSelectManager>();
    }

    private void OnEnable()
    {
        GameDatas.Instance.LoadUsers();
        LoadSlots(_slotPageIdx);
    }

    public void LoadSlots(int pageIdx)
    {
        _slotPageIdx = (pageIdx >= 0  && pageIdx * MAX_SLOTS_BY_PANEL < GameDatas.Instance.Users.Count) ? pageIdx : 0;
        foreach (Transform child in m_slotsPanel)
        {
            GameObject.Destroy(child.gameObject);
        }
        for (int i = 0; _slotPageIdx * MAX_SLOTS_BY_PANEL + i < GameDatas.Instance.Users.Count && i < MAX_SLOTS_BY_PANEL; ++i)
        {
            int idx = _slotPageIdx * MAX_SLOTS_BY_PANEL + i;
            GameDatas.UserDatas user = GameDatas.Instance.Users[idx];
            ScriptableAnimal animal = GameDatas.Instance.GetAnimalData(user.Icon);
            GameObject newSlot = GameObject.Instantiate(m_userSlotPrefab, m_slotsPanel);
            Transform avatarPanel = newSlot.transform.Find("AvatarPanel");

            avatarPanel.GetComponent<Button>().onClick.AddListener(() =>
            {
                GameDatas.Instance.CurrentUserIdx = idx;
                _levelSelectManager.LevelSelectPanel(true);
            });
            avatarPanel.GetComponent<Image>().color = animal.Color;
            avatarPanel.Find("Avatar").GetComponent<Image>().sprite = animal.Face;
            newSlot.transform.Find("NamePanel/Username").GetComponent<TextMeshProUGUI>().text = user.Username;
        }
        m_rightArrow.SetActive(_slotPageIdx > 0);
        m_leftArrow.SetActive((_slotPageIdx + 1) * MAX_SLOTS_BY_PANEL < GameDatas.Instance.Users.Count);
    }

    public void NextProfiles()
    {
        LoadSlots(_slotPageIdx + 1);
    }

    public void PreviousProfiles()
    {
        LoadSlots(_slotPageIdx - 1);
    }
}

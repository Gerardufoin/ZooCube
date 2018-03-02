using System.Collections;
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

    private int _slotPageIdx;

    private void OnEnable()
    {
        GameDatas.Instance.LoadAll();
        LoadSlots(0);
    }

    public void LoadSlots(int pageIdx)
    {
        _slotPageIdx = (pageIdx * MAX_SLOTS_BY_PANEL < GameDatas.Instance.Users.Count) ? pageIdx : 0;
        foreach (Transform child in m_slotsPanel)
        {
            GameObject.Destroy(child.gameObject);
        }
        for (int i = 0; _slotPageIdx * MAX_SLOTS_BY_PANEL + i < GameDatas.Instance.Users.Count && i < MAX_SLOTS_BY_PANEL; ++i)
        {
            GameDatas.UserDatas user = GameDatas.Instance.Users[_slotPageIdx * MAX_SLOTS_BY_PANEL + i];
            ScriptableAnimal animal = GameDatas.Instance.GetAnimalData(user.Icon);
            GameObject newSlot = GameObject.Instantiate(m_userSlotPrefab, m_slotsPanel);
            newSlot.transform.Find("AvatarPanel").GetComponent<Image>().color = animal.Color;
            newSlot.transform.Find("AvatarPanel/Avatar").GetComponent<Image>().sprite = animal.Face;
            newSlot.transform.Find("NamePanel/Username").GetComponent<TextMeshProUGUI>().text = user.Username;
        }
    }
}

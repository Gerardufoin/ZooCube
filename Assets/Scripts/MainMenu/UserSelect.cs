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
    [SerializeField]
    private GameObject m_leftArrow;
    [SerializeField]
    private GameObject m_rightArrow;
    [SerializeField]
    private ConfirmationPanel m_confirmationPanel;
    [SerializeField]
    private GameObject m_profileCreationButton;
    [SerializeField]
    private GameObject m_backButton;

    private LevelSelectManager _levelSelectManager;

    private int _slotPageIdx;
    private bool _deleteMode;

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
        if (GameDatas.Instance.Users.Count > 0)
        {
            _slotPageIdx = (pageIdx < 0 ? 0 : (pageIdx * MAX_SLOTS_BY_PANEL >= GameDatas.Instance.Users.Count ? (GameDatas.Instance.Users.Count - 1) / MAX_SLOTS_BY_PANEL : pageIdx));
            foreach (Transform child in m_slotsPanel)
            {
                GameObject.Destroy(child.gameObject);
            }
            for (int i = 0; _slotPageIdx * MAX_SLOTS_BY_PANEL + i < GameDatas.Instance.Users.Count && i < MAX_SLOTS_BY_PANEL; ++i)
            {
                int idx = _slotPageIdx * MAX_SLOTS_BY_PANEL + i;
                GameDatas.UserDatas user = GameDatas.Instance.Users[idx];
                Animal animal = GameDatas.Instance.GetAnimalData(user.Icon);
                GameObject newSlot = GameObject.Instantiate(m_userSlotPrefab, m_slotsPanel);
                Transform avatarPanel = newSlot.transform.Find("AvatarPanel");
                Transform deleteButton = newSlot.transform.Find("DeleteButton");

                avatarPanel.GetComponent<Button>().onClick.AddListener(() =>
                {
                    GameDatas.Instance.CurrentUserIdx = idx;
                    _levelSelectManager.LevelSelectPanel(true);
                });
                deleteButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    DeleteUser(idx);
                });
                avatarPanel.GetComponent<Image>().color = animal.Color;
                avatarPanel.Find("Avatar").GetComponent<Image>().sprite = animal.Face;
                newSlot.transform.Find("NamePanel/Username").GetComponent<TextMeshProUGUI>().text = user.Username;
            }
            DeleteMode(_deleteMode);
        }
        else
        {
            _slotPageIdx = 0;
            DeleteMode(false);
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

    public void DeleteMode(bool mode)
    {
        _deleteMode = mode;
        m_profileCreationButton.SetActive(!_deleteMode);
        m_backButton.SetActive(!_deleteMode);
        foreach (Transform child in m_slotsPanel)
        {
            child.GetComponent<Animator>().SetBool("DeleteMode", _deleteMode);
            child.Find("AvatarPanel").GetComponent<Button>().interactable = !_deleteMode;
        }
    }

    public void ToggleDeleteMode()
    {
        DeleteMode(!_deleteMode);
    }

    public void DeleteUser(int idx)
    {
        if (idx >= 0 && idx < GameDatas.Instance.Users.Count)
        {
            m_confirmationPanel.gameObject.SetActive(true);
            m_confirmationPanel.InitConfirmation(GameDatas.Instance.Users[idx].Username, () =>
            {
                DeleteConfirme(idx);
            });
        }
    }

    public void DeleteConfirme(int idx)
    {
        GameDatas.Instance.Users.RemoveAt(idx);
        GameDatas.Instance.SaveUsers();
        LoadSlots(_slotPageIdx);
    }
}

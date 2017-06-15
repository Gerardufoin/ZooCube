using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theater : MonoBehaviour
{
    // Reference to the loading text
    [SerializeField]
    private GameObject _loadingText;

    public delegate void Callback();
    public Callback CurtainCloseActions;

    public void ShowLoading()
    {
        _loadingText.SetActive(true);
    }

    public void HideLoading()
    {
        _loadingText.SetActive(false);
    }

    // Animation callback
    public void CurtainClosed()
    {
        if (CurtainCloseActions != null)
        {
            CurtainCloseActions();
            CurtainCloseActions = null;
        }
    }
}

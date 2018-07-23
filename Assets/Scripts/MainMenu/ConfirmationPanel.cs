using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Confirmation panel to validate an action. Activate it and set a message and a callback to use.
/// </summary>
public class ConfirmationPanel : MonoBehaviour
{
    // Custom message that can be set by the caller
    [SerializeField]
    private TextMeshProUGUI _message;

    // Callback action when validating
    private System.Action _callback;

    /// <summary>
    /// Hide the confirmation on start.
    /// </summary>
    private void Start()
    {
        if (_callback == null)
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Call to init the confirmation panel.
    /// </summary>
    /// <param name="message">Message for the user</param>
    /// <param name="cb">Callback when validating</param>
    public void InitConfirmation(string message, System.Action cb)
    {
        _callback = cb;
        _message.text = message;
    }

    /// <summary>
    /// Cancel action for the UI
    /// </summary>
    public void Cancel()
    {
        _callback = null;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Callback for the UI
    /// </summary>
    public void Callback()
    {
        if (_callback != null)
        {
            _callback();
        }
        Cancel();
    }
}

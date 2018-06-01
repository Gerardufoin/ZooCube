using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ToggleIcon class. Allows to change the sprite of the linked UI.Image element depending on two states.
/// </summary>
[RequireComponent(typeof(Image))]
public class ToggleIcon : MonoBehaviour
{
    // ON state
    [SerializeField]
    private Sprite m_onImage;
    // OFF state
    [SerializeField]
    private Sprite m_offImage;

    // Reference to the Image
    private Image _image;
    private Image ToggleImage
    {
        get
        {
            if (!_image)
            {
                _image = GetComponent<Image>();
            }
            return _image;
        }
    }

    /// <summary>
    /// Selects the ON or OFF image to display depending on the passed state.
    /// </summary>
    /// <param name="state">If true, displays the ON image. Displays the OFF image otherwise.</param>
    public void SetState(bool state)
    {
        ToggleImage.sprite = (state ? m_onImage : m_offImage);
    }
}

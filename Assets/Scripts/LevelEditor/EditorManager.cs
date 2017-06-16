using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    // Reference to the main camera
    [SerializeField]
    private Camera m_camera;

    void Start()
    {
        GameObject theater = GameObject.FindGameObjectWithTag("Theater");
        if (theater != null)
        {
            Animator anim = theater.GetComponent<Animator>();
            theater.transform.parent.GetComponent<Canvas>().worldCamera = m_camera;
            anim.SetTrigger("Open");
            anim.SetTrigger("SlideOut");
        }
    }
}

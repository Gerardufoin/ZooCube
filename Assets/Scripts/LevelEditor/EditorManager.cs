using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    void Start()
    {
        GameObject theater = GameObject.FindGameObjectWithTag("Theater");
        if (theater != null)
        {
            theater.GetComponent<Theater>().OpenCurtains(true);
        }
    }
}

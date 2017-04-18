using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RecepterManager : MonoBehaviour
{
    // When the mouse hover above a recepter, it is temporarily added to this list
    private List<Recepter> _hover = new List<Recepter>();
    // Number of recepter in the scene
    [HideInInspector]
    public int RecepterCount;

    // Check a recepter in the _hover list matches a given id
    public Recepter Fit(int id)
    {
        for (int i = 0; i < _hover.Count; ++i)
        {
            if (id == _hover[i].Id)
            {
                return _hover[i];
            }
        }
        return null;
    }

    // Add a recepter to the _hover list (called in Recepter)
    public void AddRecepter(Recepter recepter)
    {
        for (int i = 0; i < _hover.Count; ++i)
        {
            if (_hover[i].Id == recepter.Id)
            {
                return ;
            }
        }
        _hover.Add(recepter);
    }

    // Remove a recepter to the _hover list (called in Recepter)
    public void RemoveRecepter(Recepter recepter)
    {
        for (int i = 0; i < _hover.Count; ++i)
        {
            if (_hover[i].Id == recepter.Id)
            {
                _hover.RemoveAt(i);
                return ;
            }
        }
    }
}

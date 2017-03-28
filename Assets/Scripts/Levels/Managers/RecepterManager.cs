using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RecepterManager : MonoBehaviour
{
    private List<Recepter> hover = new List<Recepter>();

    public Recepter Fit(int id)
    {
        for (int i = 0; i < hover.Count; ++i)
        {
            if (id == hover[i].Id)
            {
                return hover[i];
            }
        }
        return null;
    }

    public void AddRecepter(Recepter recepter)
    {
        for (int i = 0; i < hover.Count; ++i)
        {
            if (hover[i].Id == recepter.Id)
            {
                return ;
            }
        }
        hover.Add(recepter);
    }

    public void RemoveRecepter(Recepter recepter)
    {
        for (int i = 0; i < hover.Count; ++i)
        {
            if (hover[i].Id == recepter.Id)
            {
                hover.RemoveAt(i);
                return ;
            }
        }
    }
}

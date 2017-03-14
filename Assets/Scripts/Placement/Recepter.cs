using UnityEngine;
using System.Collections;

public class Recepter : MonoBehaviour
{
    public int Id;
    //public int Id { get; set; }

    private RecepterManager recepterManager;

	// Use this for initialization
	void Start ()
    {
        recepterManager = FindObjectOfType<RecepterManager>();
	}
	
    void OnMouseEnter()
    {
        recepterManager.AddRecepter(this);
    }

    void OnMouseExit()
    {
        recepterManager.RemoveRecepter(this);
    }
}

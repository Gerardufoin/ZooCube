using UnityEngine;
using System.Collections;

public class MovablePiece : MonoBehaviour
{
    public int Id;
    //public int Id { get; set; }

    private bool selected;
    private bool reseting;
    private bool placed;

    private Vector3 basePosition;

    private RecepterManager recepterManager;

    void Start()
    {
        basePosition = transform.position;
        recepterManager = FindObjectOfType<RecepterManager>();
    }

	// Update is called once per frame
	void Update ()
    {
	    if (selected)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            transform.position = Vector3.MoveTowards(transform.position, position, 50.0f * Time.deltaTime);
        }
        if (reseting)
        {
            if (transform.position != basePosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, basePosition, 50.0f * Time.deltaTime); ;
            }
            else
            {
                reseting = false;
            }
        }
	}

    void OnMouseDown()
    {
        if (!placed && !reseting)
        {
            selected = true;
        }
    }
    
    void OnMouseUp()
    {
        if (selected)
        {
            selected = false;
            Recepter recptr = recepterManager.Fit(Id);
            if (recptr != null)
            {
                placed = true;
                transform.position = recptr.transform.position;
            }
            else
            {
                reseting = true;
            }
        }
    }
}

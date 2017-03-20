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

    private RecepterManager _recepterManager;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        basePosition = transform.position;
        _recepterManager = FindObjectOfType<RecepterManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
            _spriteRenderer.sortingLayerName = "Selected";
        }
    }
    
    void OnMouseUp()
    {
        if (selected)
        {
            selected = false;
            _spriteRenderer.sortingLayerName = "Middleground";

            Recepter recptr = _recepterManager.Fit(Id);
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

using UnityEngine;
using System.Collections;

public class MovablePiece : MonoBehaviour
{
    public int Id;
    public Animal Animal;
    public Shape Shape;

    private bool selected;
    private bool reseting;
    private bool placed;

    private Vector3 basePosition;

    private GameManager _gameManager;
    private RecepterManager _recepterManager;
    private Renderer _renderer;

    void Start()
    {
        basePosition = transform.position;
        _gameManager = FindObjectOfType<GameManager>();
        _recepterManager = FindObjectOfType<RecepterManager>();
        _renderer = GetComponent<Renderer>();
        StartCoroutine(SetShader());
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
            _renderer.sortingLayerName = "Selected";
        }
    }
    
    void OnMouseUp()
    {
        if (selected)
        {
            selected = false;
            _renderer.sortingLayerName = "Middleground";

            Recepter recptr = _recepterManager.Fit(Id);
            if (recptr != null)
            {
                placed = true;
                transform.position = recptr.transform.position;
                _gameManager.PlacePiece();
            }
            else
            {
                reseting = true;
            }
        }
    }

    private IEnumerator SetShader()
    {
        yield return new WaitForFixedUpdate();
        MaterialPropertyBlock properties = new MaterialPropertyBlock();
        _renderer.GetPropertyBlock(properties);

        properties.SetTexture("_Face", Animal.Face.texture);
        properties.SetColor("_BackgroundColor", Animal.Color);
        properties.SetColor("_BorderColor", Animal.BorderColor);
        properties.SetFloat("_FaceScale", Shape.FaceScale);
        properties.SetTexture("_ShapeMask", Shape.Mask.texture);
        properties.SetFloat("_BorderWidth", Shape.BorderWidth);
        properties.SetFloat("_BorderXOffset", Shape.BorderOffset.x);
        properties.SetFloat("_BorderYOffset", Shape.BorderOffset.y);
        properties.SetFloat("_FaceXOffset", Shape.FaceOffset.x);
        properties.SetFloat("_FaceYOffset", Shape.FaceOffset.y);

        _renderer.SetPropertyBlock(properties);
    }
}

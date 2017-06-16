using UnityEngine;
using System.Collections;

public class MovablePiece : MonoBehaviour
{
    public int Id;
    public PieceInfos PieceInfos;

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

        properties.SetTexture("_MainTex", Resources.Load<Texture2D>(PieceInfos.FacePath));
        properties.SetTexture("_ShapeMask", Resources.Load<Texture2D>(PieceInfos.ShapePath));
        properties.SetColor("_BackgroundColor", PieceInfos.BackgroundColor);
        properties.SetColor("_BorderColor", PieceInfos.BorderColor);
        properties.SetFloat("_ImageScale", PieceInfos.ImageScale);
        properties.SetFloat("_BorderWidth", PieceInfos.BorderWidth);
        properties.SetFloat("_BorderXOffset", PieceInfos.BorderXOffset);
        properties.SetFloat("_BorderYOffset", PieceInfos.BorderYOffset);
        properties.SetFloat("_ImageXOffset", PieceInfos.ImageXOffset);
        properties.SetFloat("_ImageYOffset", PieceInfos.ImageYOffset);

        _renderer.SetPropertyBlock(properties);
    }
}

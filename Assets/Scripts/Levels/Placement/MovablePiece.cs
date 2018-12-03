using UnityEngine;
using System.Collections;

/// <summary>
/// Script managing the behavior of a game piece. Can be picked by the player and has to be placed on the corresponding Recepter.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class MovablePiece : MonoBehaviour
{
    // Used to prevent the MaterialProperty to try to optimize the rendering as it breaks the shader
    private static int Material_ID = 0;
    // Id of the piece, linked to a Recepter for placement
    public int Id;
    // Type of the animal displayed on the piece. Set at creation
    [HideInInspector]
    public Animal Animal;
    // Type of the shape displayed on the piece. Set at creation
    [HideInInspector]
    public Shape Shape;

    // Sound to play when the piece is picked
    [SerializeField]
    private AudioClip _piecePicked;
    // Sound to play when the piece is placed
    [SerializeField]
    private AudioClip _piecePlaced;
    // Sound to play when the piece is resetting
    [SerializeField]
    private AudioClip _pieceResetting;

    // Is the piece currently selected by the player ?
    private bool selected;
    // Is the piece released and returning to its default position ?
    private bool reseting;
    // Is the piece placed on the correct recepter ?
    private bool placed;

    // Default position of the piece
    private Vector3 basePosition;

    // Reference to the piece's AudioSource
    private AudioSource _audioSource;
    // Reference to the GameManager
    private GameManager _gameManager;
    // Reference to the RecepterManager
    private RecepterManager _recepterManager;
    // Reference to the piece's Renderer
    private Renderer _renderer;

    /// <summary>
    /// Initialization
    /// </summary>
    void Start()
    {
        basePosition = transform.position;
        _audioSource = GetComponent<AudioSource>();
        _gameManager = FindObjectOfType<GameManager>();
        _recepterManager = FindObjectOfType<RecepterManager>();
        _renderer = GetComponent<Renderer>();
        StartCoroutine(SetShader());
    }

	/// <summary>
    /// Manage the piece movements when picked or released.
    /// </summary>
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

    /// <summary>
    /// Pick the piece on mouse down if it is not resetting or already placed
    /// </summary>
    void OnMouseDown()
    {
        if (!placed && !reseting)
        {
            selected = true;
            _audioSource.PlayOneShot(_piecePicked);
            _renderer.sortingLayerName = "Selected";
        }
    }
    
    /// <summary>
    /// Release the piece and either place it if above the right Recepter or start the reset action
    /// </summary>
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
                _audioSource.PlayOneShot(_piecePlaced);
                transform.position = recptr.transform.position;
                _gameManager.PlacePiece();
            }
            else
            {
                reseting = true;
                _audioSource.PlayOneShot(_pieceResetting);
            }
        }
    }

    /// <summary>
    /// Called at the creation of the piece. Set all the informations needed by the shader.
    /// Shader should not be set before the start function of the class has been called.
    /// </summary>
    private IEnumerator SetShader()
    {
        yield return new WaitForFixedUpdate();
        MaterialPropertyBlock properties = new MaterialPropertyBlock();
        _renderer.GetPropertyBlock(properties);

        properties.SetFloat("_ID", Material_ID++);
        properties.SetTexture("_Face", Animal.Face.texture);
        properties.SetColor("_BackgroundColor", Animal.Color);
        properties.SetColor("_BorderColor", Animal.BorderColor);
        properties.SetFloat("_FaceScale", Shape.FaceScale);
        properties.SetTexture("_ShapeMask", Shape.Mask.texture);
        properties.SetVector("_BordersWidth", Vector4.one * Shape.BorderWidth);
        properties.SetFloat("_BorderXOffset", Shape.BorderOffset.x);
        properties.SetFloat("_BorderYOffset", Shape.BorderOffset.y);
        properties.SetFloat("_FaceXOffset", Shape.FaceOffset.x);
        properties.SetFloat("_FaceYOffset", Shape.FaceOffset.y);
        properties.SetFloat("_KeepScale", Shape.KeepScale);

        _renderer.SetPropertyBlock(properties);
    }
}

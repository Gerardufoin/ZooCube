using UnityEngine;
using System.Collections;

public class Recepter : MonoBehaviour
{
    // Id of the receter, should match a MovablePiece
    public int Id;
    // Contains the info needed for the shader, loaded from the level's json
    public Shape Shape;
    // Contains the informations about the recepter borders scale, loaded from the level's json
    public Vector4 Borders;

    // Reference to the RecepterManager
    private RecepterManager _recepterManager;

    // Reference to the renderer
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _recepterManager = FindObjectOfType<RecepterManager>();
        // Increase the recepter count
        _recepterManager.RecepterCount++;
        StartCoroutine(SetShader());
    }

    // When the mouse enter the recepter, we signal it to the RecepterManager
    void OnMouseEnter()
    {
        _recepterManager.AddRecepter(this);
    }

    // When the mouse leaves the recepter, we signal it to the RecepterManager
    void OnMouseExit()
    {
        _recepterManager.RemoveRecepter(this);
    }

    // Set the shape map of the recepter shader
    IEnumerator SetShader()
    {
        yield return new WaitForFixedUpdate();
        MaterialPropertyBlock properties = new MaterialPropertyBlock();
        _renderer.GetPropertyBlock(properties);
        properties.SetTexture("_ShapeMask", Shape.Mask.texture);
        properties.SetVector("_BordersWidth", Borders * (Shape.BorderWidth / 2));
        properties.SetFloat("_BorderXOffset", Shape.BorderOffset.x);
        properties.SetFloat("_BorderYOffset", Shape.BorderOffset.y);
        properties.SetFloat("_KeepScale", Shape.KeepScale);
        _renderer.SetPropertyBlock(properties);
    }
}

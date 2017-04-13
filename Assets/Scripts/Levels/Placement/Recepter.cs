using UnityEngine;
using System.Collections;

public class Recepter : MonoBehaviour
{
    public int Id;
    //public int Id { get; set; }
    public PieceInfos PieceInfos;

    private RecepterManager recepterManager;

    private Renderer _renderer;

	// Use this for initialization
	void Start ()
    {
        _renderer = GetComponent<Renderer>();
        SetShader();
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

    void SetShader()
    {
        MaterialPropertyBlock properties = new MaterialPropertyBlock();
        _renderer.GetPropertyBlock(properties);
        properties.SetTexture("_MainTex", Resources.Load<Texture2D>(PieceInfos.ShapePath));
        _renderer.SetPropertyBlock(properties);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EditablePiece : MonoBehaviour {

    private SpriteRenderer _renderer;

    private bool _placedOnce = false;
    private Vector3 _placedPosition;

    private S_FaceInfos _faceInfos;
    private S_ShapeInfos _shapeInfos;

    private MaterialPropertyBlock _properties;
    private PieceInfos _infos;

    private bool _reseting;

    public void PresetProperties(S_FaceInfos face, S_ShapeInfos shape)
    {
        _faceInfos = face;
        _shapeInfos = shape;
    }

    private void Start()
    {
        _properties = new MaterialPropertyBlock();
        _renderer = GetComponent<SpriteRenderer>();
        SetShaderProperties();
    }

    private void Update()
    {
        if (_reseting)
        {
            if (transform.position != _placedPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, _placedPosition, 50.0f * Time.deltaTime);
            }
            else
            {
                _reseting = false;
            }
        }
    }

    public void Move(Vector3 position, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
    }

    public void Place()
    {
        _placedOnce = true;
        _placedPosition = transform.position;
    }

    public void Reset()
    {
        if (!_placedOnce)
        {
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            _reseting = true;
        }
    }

    public void SetFaceInfos(S_FaceInfos face)
    {
        _faceInfos = face;
        SetShaderProperties();
    }

    public void SetShapeInfos(S_ShapeInfos shape)
    {
        _shapeInfos = shape;
        SetShaderProperties();
    }

    void SetShaderProperties()
    {
        _renderer.GetPropertyBlock(_properties);

        _properties.SetTexture("_Face", _faceInfos.Face);
        _properties.SetColor("_BackgroundColor", _faceInfos.BackgroundColor);
        _properties.SetColor("_BorderColor", _faceInfos.BorderColor);
        _properties.SetFloat("_BorderWidth", 0.1f);
        _properties.SetTexture("_ShapeMask", _shapeInfos.Shape);
        _properties.SetFloat("_ImageScale", _shapeInfos.ImageScale);
        _properties.SetFloat("_ImageXOffset", _shapeInfos.ImageXOffset);
        _properties.SetFloat("_ImageYOffset", _shapeInfos.ImageYOffset);
        _properties.SetFloat("_BorderXOffset", _shapeInfos.BorderXOffset);
        _properties.SetFloat("_BorderYOffset", _shapeInfos.BorderYOffset);

        _renderer.SetPropertyBlock(_properties);
    }

    public PieceInfos GetPieceInfos(BoxCollider2D zone)
    {
        _infos.FacePath = _faceInfos.FaceAssetPath;
        _infos.BackgroundColor = _faceInfos.BackgroundColor;
        _infos.BorderColor = _faceInfos.BorderColor;

        _infos.ShapePath = _shapeInfos.ShapeAssetPath;
        _infos.ImageScale = _shapeInfos.ImageScale;
        _infos.ImageXOffset = _shapeInfos.ImageXOffset;
        _infos.ImageYOffset = _shapeInfos.ImageYOffset;
        _infos.BorderWidth = 0.1f;
        _infos.BorderXOffset = _shapeInfos.BorderXOffset;
        _infos.BorderYOffset = _shapeInfos.BorderYOffset;

        _infos.Position.x = (transform.position.x - zone.bounds.min.x) / zone.bounds.size.x;
        _infos.Position.y = (transform.position.y - zone.bounds.min.y) / zone.bounds.size.y;

        _infos.Scale.x = transform.localScale.x / zone.bounds.size.x;
        _infos.Scale.y = transform.localScale.y / zone.bounds.size.y;
        return (_infos);
    }
}

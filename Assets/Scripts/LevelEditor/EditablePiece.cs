﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EditablePiece class. Attached to the block pieces when in the level editor.
/// Contains information about the piece it is currently attached to.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class EditablePiece : MonoBehaviour
{
    // ID is used to prevent the MaterialProperty to try to optimize the rendering as it breaks the shader
    private static int ID = 0;

    // Reference to the SpriteRenderer
    private SpriteRenderer _renderer;

    // Set to true once the piece has been placed once. Used in the Reset method.
    private bool _placedOnce = false;
    // Position where the piece has been placed.
    private Vector3 _placedPosition;

    // Animal displayed by the piece.
    private Animal _animal;
    public Animal Animal
    {
        get { return _animal; }
        set
        {
            _animal = value;
            ApplyAnimalToShader();
        }
    }
    // Shape of the piece.
    private Shape _shape;
    public Shape Shape
    {
        get { return _shape; }
        set
        {
            _shape = value;
            ApplyShapeToShader();
        }
    }

    // PropertyBlock used to modify shader (to avoid creating a new one every time).
    private MaterialPropertyBlock _properties;

    // Is the piece currently being reset ?
    private bool _reseting;

    /// <summary>
    /// Used when the piece is created to preset the Animal and Shape datas.
    /// </summary>
    /// <param name="animal">ScriptableObject Animal datas</param>
    /// <param name="shape">ScriptableObject Shape datas</param>
    public void PresetProperties(Animal animal, Shape shape)
    {
        _animal = animal;
        _shape = shape;
    }

    private void Start()
    {
        _properties = new MaterialPropertyBlock();
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.GetPropertyBlock(_properties);
        _properties.SetFloat("_ID", ID++);
        _renderer.SetPropertyBlock(_properties);
        ApplyPropertiesToShader();
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

    /// <summary>
    /// Called to move the piece.
    /// </summary>
    /// <param name="position">Destination</param>
    /// <param name="speed">Movement speed</param>
    public void Move(Vector3 position, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
    }

    /// <summary>
    /// Called when placing the piece (once the user is done moving it).
    /// </summary>
    public void Place()
    {
        _placedOnce = true;
        _placedPosition = transform.position;
    }

    /// <summary>
    /// Called when the user cancels a movement. The piece is destroyed if it was never placed before.
    /// </summary>
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

    /// <summary>
    /// Apply the values stored in _shape to the shader's properties.
    /// </summary>
    private void ApplyShapeToShader()
    {
        if (_shape != null)
        {
            _renderer.GetPropertyBlock(_properties);
            _properties.SetFloat("_FaceScale", _shape.FaceScale);
            _properties.SetTexture("_ShapeMask", _shape.Mask.texture);
            _properties.SetFloat("_BorderWidth", _shape.BorderWidth);
            _properties.SetFloat("_BorderXOffset", _shape.BorderOffset.x);
            _properties.SetFloat("_BorderYOffset", _shape.BorderOffset.y);
            _properties.SetFloat("_FaceXOffset", _shape.FaceOffset.x);
            _properties.SetFloat("_FaceYOffset", _shape.FaceOffset.y);
            _renderer.SetPropertyBlock(_properties);
        }
    }

    /// <summary>
    /// Apply the values stored in _animal to the shader's properties.
    /// </summary>
    private void ApplyAnimalToShader()
    {
        if (_animal != null)
        {
            _renderer.GetPropertyBlock(_properties);
            _properties.SetTexture("_Face", _animal.Face.texture);
            _properties.SetColor("_BackgroundColor", _animal.Color);
            _properties.SetColor("_BorderColor", _animal.BorderColor);
            _renderer.SetPropertyBlock(_properties);
        }
    }

    /// <summary>
    /// Apply both the animal and shape datas at the same time.
    /// </summary>
    private void ApplyPropertiesToShader()
    {
        ApplyAnimalToShader();
        ApplyShapeToShader();
    }

    /// <summary>
    /// Used to get the informations of the piece when saving.
    /// </summary>
    /// <param name="zone">Collider of the play zone. Used to get the relative size of the piece</param>
    /// <returns></returns>
    public GameDatas.PieceInfos GetPieceInfos(BoxCollider2D zone)
    {
        GameDatas.PieceInfos piece;

        piece.Animal = _animal.Type;
        piece.Shape = _shape.Type;

        piece.Position.x = (transform.position.x - zone.bounds.min.x) / zone.bounds.size.x;
        piece.Position.y = (transform.position.y - zone.bounds.min.y) / zone.bounds.size.y;

        piece.Scale.x = transform.localScale.x / zone.bounds.size.x;
        piece.Scale.y = transform.localScale.y / zone.bounds.size.y;

        return (piece);
    }
}

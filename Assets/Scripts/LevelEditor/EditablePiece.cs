using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EditablePiece : MonoBehaviour {

    private SpriteRenderer _renderer;

    private bool _placedOnce = false;
    private Vector3 _placedPosition;

    private MaterialPropertyBlock _properties;

    private bool _reseting;

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

    public void SetShaderProperties(MaterialPropertyBlock properties)
    {
        _properties = properties;
        _renderer.SetPropertyBlock(_properties);
    }

    public MaterialPropertyBlock GetShaderProperties()
    {
        return _properties;
    }
}

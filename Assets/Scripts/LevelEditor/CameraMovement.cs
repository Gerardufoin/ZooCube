using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _lastPosition;

	void Start ()
    {
        _camera = GetComponent<Camera>();
    }
	
	void Update ()
    {
        Vector3 position = Input.mousePosition;
        position.z = 0;
        if (Input.GetMouseButton(2))
        {
            _camera.transform.position += (_lastPosition - position) * 0.006f;
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            _camera.orthographicSize -= scroll;
            if (_camera.orthographicSize <= 0f)
            {
                _camera.orthographicSize = 0.1f;
            }
            else if (_camera.orthographicSize > 20f)
            {
                _camera.orthographicSize = 20f;
            }
        }
        _lastPosition = position;
    }
}

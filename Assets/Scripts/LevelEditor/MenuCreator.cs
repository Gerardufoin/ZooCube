using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject _togglePrefab;
    [SerializeField]
    private ToggleGroup _shapesGroup;
    [SerializeField]
    private ToggleGroup _facesGroup;

    private PieceCreationManager _manager;

	void Start ()
    {
        _manager = FindObjectOfType<PieceCreationManager>();
        int count = 0;

        foreach (Transform child in _shapesGroup.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Shape shape in GameDatas.Instance.ZooShapes)
        {
            GameObject inst = Instantiate(_togglePrefab, _shapesGroup.transform);
            Toggle toggle = inst.GetComponent<Toggle>();
            toggle.group = _shapesGroup;
            toggle.onValueChanged.AddListener(delegate {
                if (toggle.isOn)
                {
                    _manager.ChangePieceShape(shape);
                }
            });
            Image img = inst.transform.Find("Background/Image").GetComponent<Image>();
            img.sprite = shape.Mask;
            toggle.isOn = (count++ == 0);
        }

        foreach (Transform child in _facesGroup.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        count = 0;
        foreach (Animal animal in GameDatas.Instance.ZooAnimals)
        {
            GameObject inst = Instantiate(_togglePrefab, _facesGroup.transform);
            Toggle toggle = inst.GetComponent<Toggle>();
            toggle.group = _facesGroup;
            toggle.onValueChanged.AddListener(delegate {
                if (toggle.isOn)
                {
                    _manager.ChangePieceFace(animal);
                }
            });
            Image img = inst.transform.Find("Background/Image").GetComponent<Image>();
            img.sprite = animal.Face;
            toggle.isOn = (count++ == 0);
        }
    }
}

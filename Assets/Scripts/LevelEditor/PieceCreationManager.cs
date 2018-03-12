using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// PieceCreationManager class. Manage the creation menu accessible by the user, from the creation of the toggles to their callbacks.
/// </summary>
public class PieceCreationManager : MonoBehaviour
{
    // ToggleGroup managing the animals type
    [SerializeField]
    private ToggleGroup m_facesToggles;
    // ToggleGroup managing the shapes
    [SerializeField]
    private ToggleGroup m_shapesToggles;

    // Current animal
    private Animal _currentAnimal;
    // Current shape
    private Shape _currentShape;

    // Reference to the PiecesManager
    private PiecesManager _piecesManager;

    private void Start()
    {
        _piecesManager = FindObjectOfType<PiecesManager>();
        _currentAnimal = GameDatas.Instance.ZooAnimals[0];
        _currentShape = GameDatas.Instance.ZooShapes[0];
    }

    /// <summary>
    /// Toggle callback allowing to change the current piece animal type.
    /// </summary>
    /// <param name="e">GameDatas.E_AnimalType enum passed as an int</param>
    public void ChangePieceFace(int e)
    {
        if (!m_facesToggles.transform.GetChild(e - 1).GetComponent<Toggle>().isOn) return;

        _currentAnimal = GameDatas.Instance.GetAnimalData((GameDatas.E_AnimalType)e);
        _piecesManager.ApplyFaceOnSelection(_currentAnimal);
    }

    /// <summary>
    /// Toggle callback allowing to change the current piece shape type.
    /// </summary>
    /// <param name="e">GameDatas.E_ShapeType enum passed as an int</param>
    public void ChangePieceShape(int e)
    {
        if (!m_shapesToggles.transform.GetChild(e - 1).GetComponent<Toggle>().isOn) return;

        _currentShape = GameDatas.Instance.GetShapeData((GameDatas.E_ShapeType)e);
        _piecesManager.ApplyShapeOnSelection(_currentShape);
    }

    /// <summary>
    /// Callback to the create button, create a new piece with _currentAnimal as face and _currentShape as shape.
    /// </summary>
    public void CreatePiece()
    {
        _piecesManager.CreatePiece(_currentAnimal, _currentShape);
    }
}

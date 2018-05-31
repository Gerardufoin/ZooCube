using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// PieceCreationManager class. Manage the creation menu accessible by the user, from the creation of the toggles to their callbacks.
/// </summary>
public class PieceCreationManager : MonoBehaviour
{
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
    /// <param name="animal">New animal</param>
    public void ChangePieceFace(Animal animal)
    {
        _currentAnimal = animal;
        _piecesManager.ApplyFaceOnSelection(_currentAnimal);
    }

    /// <summary>
    /// Toggle callback allowing to change the current piece shape type.
    /// </summary>
    /// <param name="shape">New shape</param>
    public void ChangePieceShape(Shape shape)
    {
        _currentShape = shape;
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

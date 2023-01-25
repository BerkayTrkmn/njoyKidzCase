using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Vector2 tilePosition;
    [SerializeField] private bool isOccupied = false;
    [SerializeField] private Character occupatedItem = null;
    [SerializeField] private TextMeshPro indexText;

    public Vector2 TilePosition { get => tilePosition; set => tilePosition = value; }
    public bool IsOccupied { get => isOccupied; }
    public Character OccupatedItem { get => occupatedItem; }
    public TextMeshPro IndexText { get => indexText; }

    private void Awake()
    {
        indexText = transform.GetChild(0).GetComponent<TextMeshPro>();

    }

    public void SetTile(Vector2 _tilePosition, bool _isOccupied, Character _occupatedItem = null)
    {
        tilePosition = _tilePosition;
        isOccupied = _isOccupied;
        if (_occupatedItem != null) occupatedItem = _occupatedItem;
        SetIndexText(_tilePosition);

    }
    public void OccupyTile(Character _occupiedCharacter)
    {
        isOccupied = true;
        occupatedItem = _occupiedCharacter;
    }
    public void UnoccupyTile()
    {
        isOccupied = false;
        occupatedItem = null;
    }
    public void SetIndexText(Vector2 _index)
    {
        indexText.text = _index.x + "," + _index.y;
    }
}

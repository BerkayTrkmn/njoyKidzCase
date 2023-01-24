using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Vector2 tilePosition;
    private bool isOccupied = false;
    private Character occupatedItem = null;

    public Vector2 TilePosition { get => tilePosition; set => tilePosition = value; }
    public bool IsOccupied { get => isOccupied; }
    public Character OccupatedItem { get => occupatedItem; }

    public void SetTile(Vector2 _tilePosition, bool _isOccupied, Character _occupatedItem = null)
    {
        tilePosition = _tilePosition;
        isOccupied = _isOccupied;
        if (_occupatedItem != null) occupatedItem = _occupatedItem;
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
}

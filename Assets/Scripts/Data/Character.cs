using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int ID;
    public Tile CurrentTile;
    public float MovingTime = 0.5f;


    protected Tile GetTile(Vector2 _tileIndex)
    {
        return Config.TileGrid[_tileIndex];
    }
    protected void MoveTile(Tile _destinationTile, Action _onComplete = null)
    {
        transform.DOMove(_destinationTile.transform.position, MovingTime).OnComplete(() => { _onComplete?.Invoke(); });
    }
    protected void MoveTile(Vector2 _destinationIndex, Action _onComplete = null)
    {
        Tile _upTile = GetTile(_destinationIndex);
        transform.DOMove(_upTile.transform.position, MovingTime).SetEase(Ease.InOutSine).OnComplete(() => { _onComplete?.Invoke(); });
    }
}

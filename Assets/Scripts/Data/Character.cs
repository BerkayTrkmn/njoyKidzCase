using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//OOyunda bulunan tüm karakterler ortak scripti (Inheritance)
public class Character : MonoBehaviour
{
    public int ID;
    public Tile CurrentTile;
    public float MovingTime = 0.5f;

    protected Tile GetTile(Vector2 _tileIndex)
    {
        return LevelCreator.Instance.TileGrid[_tileIndex];
    }
    protected void MoveTile(Tile _destinationTile, Action _onComplete = null)
    {
        transform.DOMove(_destinationTile.transform.position, MovingTime).OnComplete(() => { _onComplete?.Invoke(); });
        transform.DOScale(new Vector3(1.5f, 1.5f, transform.localScale.z), MovingTime/2).SetLoops(2, LoopType.Yoyo);
    }
    protected void MoveTile(Vector2 _destinationIndex, Action _onComplete = null)
    {
        Tile _upTile = GetTile(_destinationIndex);
        transform.DOMove(_upTile.transform.position, MovingTime).SetEase(Ease.InOutSine).OnComplete(() => { _onComplete?.Invoke(); });
        transform.DOScale(new Vector3(1.5f, 1.5f,transform.localScale.z), MovingTime/2).SetLoops(2, LoopType.Yoyo);
    }
    protected virtual void MoveCharacter(Vector2 _destinationIndex)
    {
        Tile _destinationTile = GetTile(_destinationIndex);
        if (!_destinationTile.IsOccupied)
        {
            CurrentTile.UnoccupyTile();
            _destinationTile.OccupyTile(this);
            MoveTile(_destinationIndex, () => { CurrentTile = _destinationTile; });
            Debug.Log("Moving: " + _destinationIndex);
            Config.OnPlayerMoved?.Invoke(_destinationIndex);
        }
        else
            Debug.Log("Can't move Occupied!!! ");
    }
    public bool CharacterFalling(Vector2 _destinationIndex, Action _onComplete =null)
    {
        //TODO: Check inside of grid use FallFromGrid();
        if (_destinationIndex.x < 0)
        { FallFromGrid(new Vector2(-LevelCreator.Instance.TileWidth, 0), _onComplete); return true; }
        else if (_destinationIndex.x >= LevelCreator.Instance.GridWidth)
        { FallFromGrid(new Vector2(LevelCreator.Instance.TileWidth, 0), _onComplete); return true; }
        else if (_destinationIndex.y < 0)
        { FallFromGrid(new Vector2(0, -LevelCreator.Instance.TileHeight), _onComplete); return true; }
        else if (_destinationIndex.y >= LevelCreator.Instance.GridHeight)
        { FallFromGrid(new Vector2(0, LevelCreator.Instance.TileHeight), _onComplete); return true; }

        return false;
    }

    protected virtual void FallFromGrid(Vector2 _fallingDirection, Action _onComplete = null)
    {
        transform.DOMove(transform.position + (Vector3)_fallingDirection, MovingTime).SetEase(Ease.OutCirc).OnComplete(() => { _onComplete?.Invoke(); });
        transform.DOScale(new Vector3(1.5f, 1.5f, transform.localScale.z), MovingTime / 2)
            .OnComplete(() => {
                transform.DOScale(new Vector3(0f, 0f, transform.localScale.z), MovingTime / 2);
            });

    }
}

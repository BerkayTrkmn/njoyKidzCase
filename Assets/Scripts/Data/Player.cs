using DG.Tweening;
using HelloScripts;//Hello scripts benim güzel bulduðum kendim oluþturduðum scriptlerimdir
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState
{
    Idle,
    Moving,
    Dead
}
//Oyuncunun kontrol ettiði karakter
public class Player : Character
{
    private PlayerState playerState = PlayerState.Idle;
    [SerializeField] private bool isKeyboardOn = true;
    [SerializeField] private KeyCode upKey = KeyCode.W;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode downKey = KeyCode.S;
   
    private void Update()
    {
        if (isKeyboardOn) KeyboardMovement();
    }
    private void KeyboardMovement()
    {
        if (playerState == PlayerState.Idle)
        {
            MoveTile(upKey, CurrentTile.TilePosition + Vector2.up);
            MoveTile(leftKey, CurrentTile.TilePosition + Vector2.left);
            MoveTile(rightKey, CurrentTile.TilePosition + Vector2.right);
            MoveTile(downKey, CurrentTile.TilePosition + Vector2.down);
        }

    }
    private void MoveTile(KeyCode _keyCode, Vector2 _destinationIndex)
    {
        if (Input.GetKey(_keyCode))
        {
            if (CharacterFalling(_destinationIndex, () => { /*OnFall level failed */Config.OnGameFailed?.Invoke(); })) return;
            MoveCharacter(_destinationIndex);

        }

    }
    protected override void FallFromGrid(Vector2 _fallingDirection, Action _onComplete = null)
    {
        base.FallFromGrid(_fallingDirection, _onComplete);
        playerState = PlayerState.Dead;
    }
    protected override void MoveCharacter(Vector2 _destinationIndex)
    {
        Tile _destinationTile = GetTile(_destinationIndex);
        if (!_destinationTile.IsOccupied)
        {
            PlayerOccupyTile(_destinationTile, _destinationIndex);
        }
        else
        {
            if (!CheckCoin(_destinationTile, _destinationIndex))
            {
                Debug.Log("Can't move Occupied!!! ");
                playerState = PlayerState.Idle;
            }
        }
    }
    public void PlayerOccupyTile(Tile _destinationTile, Vector2 _destinationIndex, Action _onComplete=null)
    {
        playerState = PlayerState.Moving;
        CurrentTile.UnoccupyTile();
        _destinationTile.OccupyTile(this);
        MoveTile(_destinationIndex, () => { CurrentTile = _destinationTile; playerState = PlayerState.Idle; _onComplete?.Invoke(); });
        Debug.Log("Moving: " + _destinationIndex);
        Config.OnPlayerMoved?.Invoke(_destinationIndex);
    }

    public bool CheckCoin(Tile _destinationTile, Vector2 _destinationIndex)
    {
        if(_destinationTile.OccupatedItem.GetType() == typeof(Coin))
        {
            Coin _currentCoin = (Coin)_destinationTile.OccupatedItem;
            if (_currentCoin.Order == LevelCreator.Instance.nextCoinOrder)
            {
                //Pickup Coin
                PlayerOccupyTile(_destinationTile, _destinationIndex,
                    ()=> {
                        Debug.Log("Coin " + LevelCreator.Instance.nextCoinOrder + "collected ");
                        LevelCreator.Instance.nextCoinOrder++;
                        LevelCreator.Instance.coinList.Remove(_currentCoin);
                        _currentCoin.gameObject.Destroy();
                        if (LevelCreator.Instance.coinList.Count == 0)
                            Config.OnGameCompleted?.Invoke();
                        Config.OnCoinCollected.Invoke(_currentCoin, LevelCreator.Instance.nextCoinOrder);
                    });
                return true;
            }
            else
                return false;
        }
        return false;
    }
}

using DG.Tweening;
using HelloScripts;
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
            Tile _destinationTile = GetTile(_destinationIndex);
            if (!_destinationTile.IsOccupied)
            {
                playerState = PlayerState.Moving;
                CurrentTile.UnoccupyTile();
                _destinationTile.OccupyTile(this);
                MoveTile(_destinationIndex, () => { CurrentTile = _destinationTile; playerState = PlayerState.Idle; });
                Debug.Log("Moving: " + _destinationIndex);
                Config.OnPlayerMoved?.Invoke(_destinationIndex);
            }
            else
                Debug.Log("Can't move Occupied!!! ");

        }

    }
    protected override void FallFromGrid(Vector2 _fallingDirection, Action _onComplete = null)
    {
        base.FallFromGrid(_fallingDirection, _onComplete);
        playerState = PlayerState.Dead;
    }

}

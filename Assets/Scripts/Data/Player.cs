using DG.Tweening;
using HelloScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState
{
    Idle,
    Moving
}
[RequireComponent(typeof(TouchManager))]
public class Player : Character
{
    private TouchManager touchManager;
    private PlayerState playerState = PlayerState.Idle;
    [SerializeField] private bool isKeyboardOn = true;
    [SerializeField] private KeyCode upKey = KeyCode.W;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode downKey = KeyCode.S;


    private void OnEnable()
    {
        touchManager = GetComponent<TouchManager>();
        touchManager.onTouchMoved += OnTouchMoved;
    }
    private void Update()
    {
        if (isKeyboardOn) KeyboardMovement();
    }

    private void OnTouchMoved(TouchInput touch)
    {
    }



    private void KeyboardMovement()
    {
        if(playerState == PlayerState.Idle)
        {
            KeyboardMovement(upKey, CurrentTile.TilePosition + Vector2.up);
            KeyboardMovement(leftKey, CurrentTile.TilePosition + Vector2.left);
            KeyboardMovement(rightKey, CurrentTile.TilePosition + Vector2.right);
            KeyboardMovement(downKey, CurrentTile.TilePosition + Vector2.down);
        }
       
    }
    private void KeyboardMovement(KeyCode _keyCode, Vector2 _destinationIndex)
    {
        if (Input.GetKey(_keyCode))
        {
            Tile _destinationTile = GetTile(_destinationIndex);
            if (!_destinationTile.IsOccupied)
            {
                playerState = PlayerState.Moving;
                CurrentTile.UnoccupyTile();
                _destinationTile.OccupyTile(this);
                MoveTile(_destinationIndex, () => { CurrentTile = _destinationTile; playerState = PlayerState.Idle; });
                Debug.Log("Moving ");
            }
            else
                Debug.Log("Can't move Occupied!!! ");
           
        }

    }




    private void OnDisable()
    {
        touchManager.onTouchMoved -= OnTouchMoved;

    }


}

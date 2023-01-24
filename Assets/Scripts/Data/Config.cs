using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{

    public static Dictionary<Vector2, Tile> TileGrid =new Dictionary<Vector2, Tile>();
    public static Player Player;
    public static Action OnGameStarted;
    public static Action OnGameCompleted;
    public static Action OnGameFailed;
    public static Action OnPlayerMoved;
}

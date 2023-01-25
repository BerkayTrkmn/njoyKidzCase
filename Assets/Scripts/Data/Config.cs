using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{

    public static Dictionary<Vector2, Tile> TileGrid =new Dictionary<Vector2, Tile>();
    public static Player Player;
    /// <summary>
    /// Tile length , grid count
    /// </summary>
    public static Action<Vector2,Vector2> OnLevelCreated;
    public static Action OnGameStarted;
    public static Action OnGameCompleted;
    public static Action OnGameFailed;
    public static Action<Vector2> OnPlayerMoved;
}

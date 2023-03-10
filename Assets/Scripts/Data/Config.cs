using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Oyundaki ortak değişkenler static veya const
public class Config : MonoBehaviour
{

    
    public static Player Player;
    /// <summary>
    /// Tile length , grid count
    /// </summary>
    public static Action<Vector2,Vector2> OnLevelCreated;
    public static Action OnGameStarted;
    public static Action OnGameCompleted;
    public static Action OnGameFailed;
    public static Action<Coin,int> OnCoinCollected;
    public static Action<Vector2> OnPlayerMoved;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelloScripts;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Player playerPrefab;
    [SerializeField] private int gridHeight = 15;
    [SerializeField] private int gridWidth = 15;
    //Tile size + space
    [SerializeField] private float tileHeight = 1.5f;
    [SerializeField] private float tileWidth = 1.5f;
    private Camera cam;
    [SerializeField] private Vector2 playerStartPoint = new Vector2(7, 7);
    private void Awake()
    {
        cam = Camera.main;
        SetLevel();
        SetCamera();
    }

    public void SetLevel()
    {
        CreateLevel(gridHeight, gridWidth);
        CreatePlayer(Config.TileGrid[playerStartPoint]);
    }
    public void SetCamera()
    {
        cam.transform.position = cam.transform.position.ChangeXY( Config.Player.transform.position.x, Config.Player.transform.position.y);
        cam.transform.parent = Config.Player.transform;
    }

    public void CreateLevel(int _height, int _width)
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                Tile tempTile = Instantiate(tilePrefab, new Vector3(x * tileWidth, y * tileHeight), Quaternion.identity, transform);
                tempTile.SetTile(new Vector2(x, y), false);
                Config.TileGrid.Add(new Vector3(x, y), tempTile);
            }
        }
    }
    public void CreatePlayer(Tile _startingTile)
    {
        Config.Player = Instantiate(playerPrefab, _startingTile.transform.position, Quaternion.identity);
        Config.Player.CurrentTile = _startingTile;
        _startingTile.OccupyTile(Config.Player);
    }


}

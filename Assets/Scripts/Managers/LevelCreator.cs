using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelloScripts;

public class LevelCreator : MonoBehaviour
{
    public static LevelCreator Instance;

    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Player playerPrefab;
    public int GridHeight = 15;
    public int GridWidth = 15;
    //Tile size + space
    public float TileHeight = 1.5f;
    public float TileWidth = 1.5f;
    private Camera cam;
    [SerializeField] private Vector2 playerStartPoint = new Vector2(7, 7);
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);

        cam = Camera.main;
        SetLevel();
        SetCamera();
    }

    public void SetLevel()
    {
        CreateLevel(GridHeight, GridWidth);
        CreatePlayer(Config.TileGrid[playerStartPoint]);
    }
    public void SetCamera()
    {
        cam.transform.position = cam.transform.position.ChangeXY(Config.Player.transform.position.x, Config.Player.transform.position.y);
        cam.transform.parent = Config.Player.transform;
    }

    public void CreateLevel(int _height, int _width)
    {
        for (int y = 0; y < GridHeight; y++)
        {
            for (int x = 0; x < GridWidth; x++)
            {
                Tile tempTile = Instantiate(tilePrefab, new Vector3(x * TileWidth, y * TileHeight), Quaternion.identity, transform);
                tempTile.SetTile(new Vector2(x, y), false);
                Config.TileGrid.Add(new Vector3(x, y), tempTile);
            }
        }
        Config.OnLevelCreated?.Invoke(new Vector2(TileWidth, TileHeight), new Vector2(GridWidth, GridHeight));
    }
    public void CreatePlayer(Tile _startingTile)
    {
        Config.Player = Instantiate(playerPrefab, _startingTile.transform.position, Quaternion.identity);
        Config.Player.CurrentTile = _startingTile;
        _startingTile.OccupyTile(Config.Player);
    }


}

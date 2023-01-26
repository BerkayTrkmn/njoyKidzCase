using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelloScripts;//Hello scripts benim güzel bulduðum kendim oluþturduðum scriptlerimdir
//Oyuna baþlarken yapýlan level oluþturma iþlemleri
public class LevelCreator : MonoBehaviour
{
    public static LevelCreator Instance;

    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Vector2 playerStartPoint = new Vector2(7, 7);

    [Range(5,50)]
    public int GridHeight = 15;
    [Range(5, 50)]
    public int GridWidth = 15;

    //Tile size + space
    public float TileHeight = 1.5f;
    public float TileWidth = 1.5f;

    private Camera cam;

    //COIN
    [SerializeField]private bool isCoinNeeded = false;
    [SerializeField] private int coinCount = 5;
    [SerializeField]private Coin coinPrefab;
    public List<Coin> coinList;

    public Dictionary<Vector2, Tile> TileGrid = new Dictionary<Vector2, Tile>();

    public int nextCoinOrder = 1;
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
        CreatePlayer(TileGrid[playerStartPoint]);
        if (isCoinNeeded) CreateCoins(coinCount);
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
                TileGrid.Add(new Vector3(x, y), tempTile);
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
    public void CreateCoins(int _number)
    {
       
        for (int i = 0; i < _number; i++)
        {
            Tile _currentTile;
            bool _isOccupied;
            do
            {
                int _randomX = Random.Range(0, GridWidth);
                int _randomY = Random.Range(0, GridHeight);

                 _currentTile = TileGrid[new Vector3(_randomX, _randomY)];
                if (_currentTile.IsOccupied) _isOccupied = true;
                else _isOccupied = false;
            } while (_isOccupied);

            Coin _currentCoin = Instantiate(coinPrefab, _currentTile.transform.position, Quaternion.identity);
            _currentCoin.Order = i+1;
            _currentTile.OccupyTile(_currentCoin);
            coinList.Add(_currentCoin);
        }
    }

}

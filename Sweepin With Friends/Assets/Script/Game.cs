
using UnityEngine;

public class Game : MonoBehaviour
{

    public int width = 16;
    public int height = 16;

    private Board board;
    private Tile[,] state;

    private void Awake()
    {
        board = GetComponentInChildren<Board>();
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        state = new Tile[width, height];

        GenerateCells();

        board.Draw(state);
    }   

    private void GenerateCells()
    {
        for(int x = 0; x < width; x++) {
            for(int y =0; y < height; y++)
            {
                Tile tile = new Tile();
                tile.position = new Vector3Int(x, y, 0);
                tile.type = Tile.Type.Empty;
                state[x, y] = tile;
            }    
        }
    }

}

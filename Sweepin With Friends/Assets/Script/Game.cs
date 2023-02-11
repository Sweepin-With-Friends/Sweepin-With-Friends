
using UnityEngine;

public class Game : MonoBehaviour
{

    public int width = 16;
    public int height = 16;
    public int mineCount = 32;
    public int cameraPadding = 1;

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

        GenerateTiles();

        Camera.main.transform.position = new Vector3(width / 2f, height / 2f, -10f);
        Camera.main.orthographicSize = Mathf.Max(width, height) / 2f + cameraPadding;

        board.Draw(state);
    }   

    private void GenerateTiles() // generates the tiles for teh board
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

    private void GenerateMines() // generates mines for the board 
    {
        for(int i = 0; i < width; i++)
        {
            int x = Random.Range(0, width - i);
            int y = Random.Range(0, height - i);

            if (state[x,y].type == Tile.Type.Mine)
            {
                GenerateMines(); //hopefull recursive
            }

            state[x, y].type = Tile.Type.Mine;
        }
    }

}


using System.ComponentModel;
using System.Threading;
using Unity.Mathematics;
using Random = UnityEngine.Random;
using UnityEngine;

public class Game : MonoBehaviour
{

    public int width = 16;
    public int height = 16;
    public int mineCount = 32;
    public int cameraPadding = 1;

    private Board board;
    private Tiles[,] state;

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
        state = new Tiles[width, height];

        GenerateTiles();
        GenerateMines();
        GenerateNumbers();
        

        Camera.main.transform.position = new Vector3(width / 2f, height / 2f, -10f);
        Camera.main.orthographicSize = Mathf.Max(width, height) / 2f + cameraPadding;

        board.Draw(state);
    }   

    private void GenerateTiles() // generates the tiles for teh board
    {
        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Tiles tile = new Tiles();
                tile.position = new Vector3Int(x, y, 0);
                tile.type = Tiles.Type.Empty;
                tile.hidden = true;
                state[x, y] = tile;
                //Debug.Log("Started");
            }
        }
    }

    private void GenerateMines() // generates mines for the board 
    {

        for(int i = 0; i < mineCount; i++)
        {
            
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            while (state[x,y].type == Tiles.Type.Mine)
            {
                 x = Random.Range(0, width);
                 y = Random.Range(0, height);
            }
             
        

               state[x, y].type = Tiles.Type.Mine;
               

            
        }
    }

    private void GenerateNumbers() // generates numbers that are next to mines
    {
        for(int x=0; x< width; x++)
        {
            for(int y=0; y< height; y++)
            {
                Tiles tile = state[x, y];

                if(tile.type == Tiles.Type.Mine)
                {
                    continue;
                }

                tile.number = CountMine(x, y);

                if(tile.number > 0)
                {
                    tile.type = Tiles.Type.Number;
                }

                //tile.hidden = false;
                state[x, y] = tile;

            }
        }



    }

    private int CountMine(int tileX, int tileY)
    {
        int count = 0;

        for (int adjacentX = -1; adjacentX <=1; adjacentX++)
        {
            for(int adjacentY =-1;adjacentY <=1; adjacentY++)
            {
                if(adjacentX== 0 && adjacentY == 0)
                {
                    continue;
                }

                int x = tileX + adjacentX;
                int y = tileY + adjacentY;

                if(GetTile(x,y).type== Tiles.Type.Mine)
                {            
                    count++;
                }

            }
        }

        return count;

    }

    private void Update()//special unity function
    {
        if (Input.GetMouseButtonDown(1))
        {
            Flag();
        }
    }

    private void Flag()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePosition = board.tilemap.WorldToCell(worldPosition);

        Tiles tile = GetTile(tilePosition.x, tilePosition.y);

        if(tile.type == Tiles.Type.Invalid || !tile.hidden)
        {
            //Debug.Log("Started");
            return;
        }

        tile.flagged = !tile.flagged;
        state[tilePosition.x, tilePosition.y] = tile;
        board.Draw(state);

    }

    private Tiles GetTile(int x , int y)
    {
        if (IsValid(x, y))
        {
            return state[x,y];

        }
        else
        {
            return new Tiles();
        }
    }

    private bool IsValid(int x, int y)
    {
        return x>=0 && x < width && y>=0 && y < height;
    }


}

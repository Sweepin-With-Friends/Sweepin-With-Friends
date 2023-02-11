
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{

    public Tilemap tilemap { get; private set; }

    public Tile tileUnknown; // untouched tile
    public Tile tileEmpty; // empty tile
    public Tile tileMine; // mine tile
    public Tile tileExploded; // exploded mine
    public Tile tileFlag; // flag tile
    public Tile tileNum1; // number tile
    public Tile tileNum2;
    public Tile tileNum3;
    public Tile tileNum4;
    public Tile tileNum5;
    public Tile tileNum6;
    public Tile tileNum7;
    public Tile tileNum8;
    public Tile tileNum9;
    public Tile tileNum10;
    
    

    private void Awake() // creates map on activation
    {

        tilemap = GetComponent<Tilemap>();
    }

    public void Draw(Tile[,] state) // creates map based on size of board
    {
        int width = state.GetLength(0);
        int height = state.GetLength(1);

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; x < height; x++)
            {
                Tile tile = state[x, y];
                //tilemap.SetTile(tile.position, GetTile(tile)); // throws error?!
            }
        }

    }
    
    private Tile GetTile(Tile tile) // returns the tile image
    {
        if (tile.hidden)
        {
            return tileUnknown;
        }
        else if (tile.flagged)
        {
            return tileFlag;
        }
        else
        {
            return GetKnownTile(tile);
        }

    }
    
    private Tile GetKnownTile(Tile tile) // returns the tile image other than unknown or flagged
    {
        switch (tile.type)
        {
            case Tile.Type.Empty: return tileEmpty;
            case Tile.Type.Mine: return tileMine;
            case Tile.Type.Number: return GetNumberTile(tile);
            default: return tileNum9;

        }
    }

    private Tile GetNumberTile(Tile tile) // returns the tile image for numbers
    {
        switch (tile.number)
        {
            case 1: return tileNum1;
            case 2: return tileNum2;
            case 3: return tileNum3;
            case 4: return tileNum4;
            case 5: return tileNum5;
            case 6: return tileNum6;
            case 7: return tileNum7;
            case 8: return tileNum8;
            case 9: return tileNum9;
            default: return tileNum9;
        }

    }

}








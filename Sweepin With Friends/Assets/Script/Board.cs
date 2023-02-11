using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class Board : MonoBehaviour
{   
    public Tilemap tilemap { get; private set; }

    public Tile tileUnknown; // untouched tile
    public Tile tileEmpty; // empty tile
    public Tile tileMine; // mine tile
    public Tile tileHit; // exploded mine
    public Tile tileFlag; // flag tile
    public Tile tileOne; // number tile
    public Tile tileTwo;
    public Tile tileThree;
    public Tile tileFour;
    public Tile tileFive;
    public Tile tileSix;
    public Tile tileSeven;
    public Tile tileEight;

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
                //tilemap.SetTile(tile.position, GetTile(tile));
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
            default: return tileUnknown;

        }
    }

    private Tile GetNumberTile(Tile tile) // returns the tile image for numbers
    {
        switch (tile.number)
        {
            case 1: return tileOne;
            case 2: return tileTwo;
            case 3: return tileThree;
            case 4: return tileFour;
            case 5: return tileFive;
            case 6: return tileSix;
            case 7: return tileSeven;
            case 8: return tileEight;
            default: return tileUnknown;
        }

    }

}








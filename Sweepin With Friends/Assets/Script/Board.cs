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

    public void Draw(Tiles[,] state) // creates map based on size of board
    {
        int width = state.GetLength(0);
        int height = state.GetLength(1);

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; x < height; x++)
            {
                Tiles tiles = state[x, y];
                tilemap.SetTile(tiles.position, GetTile(tiles));
            }
        }

    }
    
    private Tile GetTile(Tiles tiles) // returns the tile image
    {
        if (tiles.hidden)
        {
            return tileUnknown;
        }
        else if (tiles.flagged)
        {
            return tileFlag;
        }
        else
        {
            return GetKnownTile(tiles);
        }

    }
    
    private Tile GetKnownTile(Tiles tiles) // returns the tile image other than unknown or flagged
    {
        switch (tiles.type)
        {
            case Tiles.Type.Empty: return tileEmpty;
            case Tiles.Type.Mine: return tileMine;
            case Tiles.Type.Number: return GetNumberTile(tiles);
            default: return tileUnknown;

        }
    }

    private Tile GetNumberTile(Tiles tiles) // returns the tile image for numbers
    {
        switch (tiles.number)
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








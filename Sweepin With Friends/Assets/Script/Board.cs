
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{

    public Tilemap tilemap { get; private set; }

    public Tile tileUnkown;
    public Tile tileEmpty;
    public Tile tileMine;
    public Tile tileExploded;
    public Tile tileFlag;
    public Tile tileNum1;
    public Tile tileNum2;
    public Tile tileNum3;
    public Tile tileNum4;
    public Tile tileNum5;
    public Tile tileNum6;
    public Tile tileNum7;
    public Tile tileNum8;
    public Tile tileNum9;
    

    private void Awake()
    {

        //tilemap = GetComponent<tilemap>();
    }

    public void Draw(Tile[,] state)
    {
        int width = state.GetLength(0);
        int height = state.GetLength(1);

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; x < height; x++)
            {
                Tile cell = state[x, y];
               // tilemap.SetTile(cell.position, GetTile(cell));
            }
        }

    }

}
/*
private Tile GetTile(Tile tile)
{
    if (cell.hidden) 
    {

    } 
    else if (cell.flagged)
    {

    } 
    else
    {

    } 
    
} 
*/


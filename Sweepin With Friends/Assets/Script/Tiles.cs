using UnityEngine;

//[System.Serializable]
public struct Tiles
{
    public enum Type
    { 
        Empty, // tile is empty
        Mine, // tile is a mine
        Number, // number of mines touching tile
    }

    public Vector3Int position; // position on board
    public Type type; // type of tile
    public int number; // number of mines touching tile
    public bool hidden, flagged, hit; // apearance of tile


}

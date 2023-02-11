
using UnityEngine;

public struct Tile
{

    public enum Type
    { 
        Empty,
        Mine,
        Number,
    }

    public Vector3Int position;
    public Type type;
    public int number;
    public bool hidden, flagged, hit;




}
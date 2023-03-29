using UnityEngine.Tilemaps;
using UnityEngine;

public enum Letter
{
    A,
    B,
    /*
    C,
    D,
    E,
    F,
    G,
    H,
    I,
    J,
    K,
    L,
    M,
    N,
    O,
    Q,
    P,
    R,
    S,
    T,
    U,
    W,
    X,
    V,
    Y,
    Z,
    */
}

[System.Serializable]
public struct TileData
{
    public Letter letter;
    public Tile tile;
    public Vector2Int[] cells {get; private set; }

    //priskiria kordinates kaladelem ir 2d figuras padaro
    public void Initialize()
    {
        this.cells = Data.Cells[this.letter];
    }
}
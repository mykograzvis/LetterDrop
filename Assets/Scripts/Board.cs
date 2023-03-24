using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    //kaladeliu masyvas
    public TileData[] tiles;
    //zemelapis
    public Tilemap tilemap {get; private set;}
    //active kaladele
    public Piece activePiece {get; private set; }
    //kordinates kur atspanint kaladeeles
    public Vector3Int spawnPosition;

    //kai pastartini zaidima priskiria reiksmes viskam
    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();

        for (int i = 0; i < this.tiles.Length; i++)
        {
            this.tiles[i].Initialize();
        }
    }

    //game start ka daryt
    private void Start()
    {
        SpawnPiece();
    }

    //ant lentos ima ir atspawnina kaladele
    public void SpawnPiece()
    {
        int random = Random.Range(0, this.tiles.Length);
        TileData data = this.tiles[random];

        this.activePiece.Initialize(this, this.spawnPosition, data);
        Set(this.activePiece);
    }

    //funkcija kuri paima ir atspawnina ant mapo nurodyta kaladele
    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }
}

using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;

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
    //apsibreziam boardo dydi
    public Vector2Int boardSize = new Vector2Int(11, 20);
    //zodziu kuriuos printinti klase
    public Printer WordLetters = new Printer();
    // Tikrins ar boarde yra sudetu zodziu
    public WordFind finder;
    //apsibreziam boardo ribas kordinatemis zaidimo
    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-this.boardSize.x/2, -this.boardSize.y/2);
            return new RectInt(position, this.boardSize);
        }
    }

    //kai pastartini zaidima priskiria reiksmes viskam
    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();

        for (int i = 0; i < this.tiles.Length; i++)
        {
            this.tiles[i].Initialize();
        }

        this.WordLetters = new Printer();

        finder = new WordFind();
        finder.Create(tilemap); // Sukuriamas zodynas (reik sukurt tik viena kart)
    }

    //game start ka daryt
    private void Start()
    {
        SpawnPiece();
    }

    //ant lentos ima ir atspawnina kaladele
    public void SpawnPiece()
    {
        // pries atspawninant kaladele, tikrinam ar yra sudarytas zodis. Jei taip, reikia ji istrinti
        string word = finder.FindWord();
        if (word != "") // rastas zodis
        {
            // pagal koordinates istrinam zodi ir vykdom kaladeliu perstumimus
            // trinamo zodzio koordinates saugomos "finder.positions" liste (manau tai pravers darant trynima)
            ClearWord();
        }

        int index = WordLetters.GetWordLetter();
        TileData data = this.tiles[index];

        this.activePiece.Initialize(this, this.spawnPosition, data);
        Set(this.activePiece);
    }

    //funkcija skirta istrinti zodi, kuri rado finder objektas
    public void ClearWord()
    {   
        for(int i = 0; i < finder.positions.Count; i++)
        {
            this.tilemap.SetTile(finder.positions[i], null);
        }
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

    //funkcija kuri istrina praitose kordinates kaladele
    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }

    //funkcija keisti kaladeles vieta
    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = this.Bounds;

        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + position;

            //paziurim ar neiseina is ribu zaidimo
            if(!bounds.Contains((Vector2Int)tilePosition))
            {
                return false;
            }

            //paziurim ar laisva kordinate
            if(this.tilemap.HasTile(tilePosition))
            {
                return false;
            }
        }

        return true;
    }
}


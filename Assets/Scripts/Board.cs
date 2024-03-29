using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using static Level;

public class Board : MonoBehaviour
{
    public GameObject gameover;
    [SerializeField] private AudioSource scoreSoundEffect;
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
    // Saugos, ar zaidziama level tipo sesija
    public bool isLevel = false;
    // level kintamasis
    public Level level { get; private set; }
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

        if (isLevel) // patikrinam ar level tipo sesija, jei taip, vaizduojam uzduotys (objectives)
        {
            level = new Level();
            level.Initialize();
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
        
        // pries atspawninant kaladele, tikrinam ar yra sudarytas zodis. Jei taip, reikia ji istrinti
        string word = finder.FindWord();
        while (word != "") // rastas zodis
        {
            // pagal koordinates istrinam zodi ir vykdom kaladeliu perstumimus
            // trinamo zodzio koordinates saugomos "finder.positions" liste (manau tai pravers darant trynima)
            ClearWord();
            LetterGravity();
            int value = 100;
            if (word.Contains('h') || word.Contains('g') || word.Contains('b') || word.Contains('f') || word.Contains('y') || word.Contains('w') || word.Contains('k'))
                value += 50;
            if (word.Contains('v') || word.Contains('x') || word.Contains('z') || word.Contains('j') || word.Contains('q'))
                value += 75;
            ScoreScript.scoreValue += (value * Math.Pow(1.5, word.Length - 3));
            FoundWord.AddWord(word);

            if (isLevel) // patikrinam ar level tipo sesija, jei taip, tikrinam objectives
                level.UpdateObjectives(word.Length);
            word = finder.FindWord(); // Patikrinam is naudo nes po letter gravity galimai susidare naujas zodis
        }

        if(tilemap.HasTile(spawnPosition))
        {
            //SceneManager.LoadScene("MainMenu");
            gameover.SetActive(true);
        }

        int index = WordLetters.GetEndlessLetter();
        TileData data = this.tiles[index];

        this.activePiece.Initialize(this, this.spawnPosition, data);
        Set(this.activePiece);
    }

    //funkcija skirta istrinti zodi, kuri rado finder objektas
    public void ClearWord()
    {
        //scoreSoundEffect.Play();
	    AudioManager.Instance.PlaySFX("Isnykimas");
        for (int i = 0; i < finder.positions.Count; i++)
        {
            this.tilemap.SetTile(finder.positions[i], null);
        }
        if (isLevel) // patikrinam ar level tipo sesija,
        {
            this.activePiece.SpeedUpLVL();
        }
        else
        {
            this.activePiece.SpeedUp();
        }
    }

    //funkcija kuri padaro, kad nukristu raides, po kuriu buvo istrintas zodis
    public void LetterGravity()
    {    
        for(int i = 0; i < finder.positions.Count; i++)
        {
            Vector3Int tileposition = finder.positions[i];
            Vector3Int above = tileposition;
            above.y += 1;
            //checkinam ar virs esamo tile nera bloko
            if(tilemap.HasTile(above))
            {
                var position = tileposition;
                //ziurim visus virsutinius tile ir paslenkam per viena
                while(tilemap.HasTile(above))
                {
                    var tile = tilemap.GetTile(above);
                    tilemap.SetTile(position, tile);
                    tilemap.SetTile(above, null);
                    above.y += 1;
                    position.y += 1;
                }
            }
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


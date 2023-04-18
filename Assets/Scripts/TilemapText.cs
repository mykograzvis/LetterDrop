using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using System.Collections.Generic;

// klase kuri reikalinga raidziu rodymui zaidimo aplinkoje
// kiekviena raide yra saugoma tile.name kintamajame
public class TilemapText : MonoBehaviour
{
    public Tilemap tilemap; // zemelapis
    public TextMeshPro textPrefab; // objektas, kuris rodys teksta
    private GameObject textGO; // objektas, kuris saugos textPrefab objekta
    private List<GameObject> previousTextGOs = new List<GameObject>();

    private void Start()
    {
        // viskas daroma per Update(), cia galima tuscia palikt
    }
    void Update()
    {
        // sunaikinam visas rodytas raides is praeito frame, kadangi grid'as pasikeite po movement
        foreach (GameObject previousTextGO in previousTextGOs)
            Destroy(previousTextGO);
        previousTextGOs.Clear();

        // tikrinam visa zemelapi ir rodom raides, kur reikia
        for (int x = tilemap.cellBounds.xMin; x < tilemap.cellBounds.xMax; x++)
        {
            for (int y = tilemap.cellBounds.yMin; y < tilemap.cellBounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(pos);
                if (tile != null) // patikrinam, ar laukelis ne tuscias
                {
                    //Debug.Log("Tile: " + tile.name + " at: " + pos);
                    textGO = new GameObject("Tile Text"); // kuriam GameObject kuris rodys teksta (raide)
                    textGO.transform.position = tilemap.GetCellCenterWorld(pos);
                    textGO.transform.parent = tilemap.transform;
                    previousTextGOs.Add(textGO);

                    TextMeshPro textMeshPro = textGO.AddComponent<TextMeshPro>();
                    textMeshPro.text = tile.name; // priskiriam text objektui raide
                    textMeshPro.alignment = TextAlignmentOptions.Center;
                    textMeshPro.sortingOrder = 2; // reikia, kad tekstas butu priekyje grid'o, o ne uz jo
                    textMeshPro.fontSize = 7;
                    textMeshPro.color = Color.black;
                }
            }
        }
    }
}
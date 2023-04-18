// Zodziai faile "Words.txt" paimti is: wordlist.aspell.net/12dicts

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Tilemaps;
using System;
using System.Linq;
using System.Reflection;

public class WordFind
{
    private string filePath; // Failo, kuriame yra galimi zodziai, kelias
    private Dictionary<string, bool> dictionary; // Zodynas, kuriame bus ieskomi zodziai
    private Tilemap tilemap; // zemelapis
    public List<Vector3Int> positions; // koordinates kubeliu, kuriu raides sudaro zodi
    // Nuskaito tekstini faila ir sukuria dictionary
    public void Create(Tilemap tilemap)
    {
        this.tilemap = tilemap;
        dictionary = new Dictionary<string, bool>();
        filePath = Application.dataPath + @"\Scripts\Dictionary.txt";
        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
            dictionary[line.ToLower()] = true;
    }
    public string FindWord()
    {
        for (int y = tilemap.cellBounds.yMin; y < tilemap.cellBounds.yMax; y++)
        {
            String line = "";
            int letterCount = 0;
            positions = new List<Vector3Int>();
            for (int x = tilemap.cellBounds.xMin; x < tilemap.cellBounds.xMax; x++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(pos);
                positions.Add(pos);
                if (tile != null)
                {
                    //Debug.Log("Tile: " + tile.name + " at: " + pos);
                    line += tile.name.ToLower();
                    letterCount++;
                }
                else // tuscias langelis
                    line += "-";
            }
            if (letterCount > 2) // tikrinam tik tada, kai yra pakankamai raidziu eiluteje
            {
                string word = WordInLine(line);
                if (word != "")
                {
                    Debug.Log("Found word: " + word);
                    positions = GetWordPositions(line, word, positions);
                    ScoreScript.scoreValue += (100 * Math.Pow(1.5, word.Length - 3));
                    return word;
                }
            }
        }
        return "";
    }
    private string WordInLine(string line)
    {
        var words = dictionary.Where(p => line.ToLower().Contains(p.Key) && p.Key.Length > 2); // randam zodzius, kurie yra linijoje ir kuriu ilgis > 2
        return words.Count() > 0 ? words.OrderByDescending(k => k.Key.Length).First().Key : ""; // atrenkam ilgiausia
    }
    private List<Vector3Int> GetWordPositions(string line, string word, List<Vector3Int> positions)
    {
        int index = 0;
        for (int i = 0; i < line.Length; i++)
        {
            //Debug.Log("Line char: " + line[i]);
            //Debug.Log("Word char: " + word[index]);
            //Debug.Log("Line length: " + line.Length);
            if (line[i] == word[index])
            {
                //Debug.Log("Index: " + index);
                index++;
            }
            else
                index = 0; // reset
            if (index == word.Length) // all letters have been checked
            {
                /*Debug.Log("Adding range from: " + (i - word.Length + 1) + " length: " + word.Length);
                foreach (Vector3Int pos in positions.GetRange(i - word.Length + 1, word.Length))
                    Debug.Log("adding tile at: " + pos);*/
                return positions.GetRange(i - word.Length + 1, word.Length);
            }
        }
        return new List<Vector3Int>();
    }
}

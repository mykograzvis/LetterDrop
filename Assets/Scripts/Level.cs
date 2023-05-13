using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Klase apibrezianti lygi
public class Level : MonoBehaviour
{
    static private int[] wordLengths; // reikiamu sudelioti zodziu ilgiai
    static private int[] wordCounts; // reikiamu sudelioti zodziu kiekiai
    public LevelData levelValues; // reiksmes, kurios yra nurodytos prie lygio
    public TMP_Text levelText; // vaizduos uzduociu teksta
    // klase apirbrezianti lygio tiksla
    public class Objective
    {
        public int WordLength { get; set; } // kokio ilgio zodzius reikia sudeti
        public int WordCount { get; set; } // kiek zodziu reikia sudeti

        public Objective(int wordLength, int wordCount)
        {
            WordLength = wordLength;
            WordCount = wordCount;
        }
        public void UpdateCount(TMP_Text levelText)
        {
            WordCount--;
            levelText.text = levelText.text.Replace("Make " + (WordCount + 1) + " word (-s) with length of " + WordLength + "\n", "Make " + WordCount + " word (-s) with length of " + WordLength + "\n");
        }

    }
    private List<Objective> objectives; // lygio uzduociu listas

    public void SetValues()
    {
        // gaunam values is levelValues kintamojo ir irasom i static kintamuosius, kad reiksmes butu issaugotos keiciant scene
        wordLengths = levelValues.wordLengths;
        wordCounts = levelValues.wordCounts;
    }
    public void Initialize()
    {
        levelText = GameObject.Find("Canvas/Objectives").GetComponent<TMP_Text>();
        objectives = new List<Objective>();
        for (int i = 0; i < wordCounts.Length; i++)
        {
            objectives.Add(new Objective(wordLengths[i], wordCounts[i]));
            levelText.text += "Make " + wordCounts[i] + " word (-s) with length of " + wordLengths[i] + "\n";
        }
    }
    public void UpdateObjectives(int wordLength)
    {
        if (objectives == null)
            return;
        if (objectives.Count > 0)
            for (int i = 0; i < objectives.Count; i++)
                if (objectives[i].WordLength == wordLength)
                {
                    if (objectives[i].WordCount > 0)
                        objectives[i].UpdateCount(levelText);
                    if (objectives[i].WordCount == 0)
                    {
                        levelText.text = levelText.text.Replace("Make " + objectives[i].WordCount + " word (-s) with length of " + objectives[i].WordLength + "\n", "");
                        objectives.RemoveAt(i);
                    }
                    
                }
        if (objectives.Count == 0) // visos uzduotys ivykdytos, game over
        {
            SceneManager.LoadScene("Win");
        }
    }
}


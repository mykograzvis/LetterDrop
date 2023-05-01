using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoundWord : MonoBehaviour
{
    public TMP_Text wordText;
    public static LinkedList<string> wordList = new LinkedList<string>();
    // Start is called before the first frame update
    void Start()
    {
        wordList.Clear();
        wordText.text = "COLLECTED WORDS:\n";
    }
     
    public static void AddWord(string word)
    {
        if(wordList.Count < 5)
        {
            wordList.AddFirst(word);
        }
        else
        {  
            wordList.AddFirst(word);
            wordList.RemoveLast();
        }
    }

    // Update is called once per frame
    void Update()
    {
        wordText.text = "COLLECTED WORDS:\n";
        if(wordList.Count > 0)
        {
            foreach(string word in wordList)
            {
                wordText.text += word + "\n";
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

//klase atsirinkti ka printinti ant lentos
public class Printer
{

    //visu zodziu masyvas
    public List<string> words = new List<string> ();
    //sumetam visus zodzius i char masyva
    public List<char> letters = new List<char>();
    //number of letters
    public int LetterCount;

    //basic konstruktorius priskirt reiksmes
    public Printer()
    {
        //nzn galesim is failo ar kokias eilutes skaityt jei lygiams darysim  ir taip zodzius pasirinksim kolkas fixed masyvas.
        words = new List<string> { "dog", "cat", "bike" };
        LetterCount = -1;
    }

    //jei leter masyvas tuscias vel visu zodziu raidessusideda i masyva letters
    public void Generator()
    {
        foreach (string word in words)
        {
            for(int j =0; j<word.Length; j++)
            {
                LetterCount++;
                letters.Add(word[j]);
            }
            Debug.Log("Count: "  + LetterCount);
        }
    }

    //ima random raide is letters masyvo ir ja istrina is masyvo ir grazina tos raides numeri (pvz A = 0, B = 1, ...)
    public int GetWordLetter()
    {
         if(LetterCount == -1)Generator();
         int number = Random.Range(0, LetterCount);
         char c = letters.ElementAt(number);
         int index = char.ToUpper(c) - 'A';
         letters.RemoveAt(number);
         LetterCount--;
         // wasVowel = "aeiouAEIOU".IndexOf(c) >= 0;

         return index;
    }
}

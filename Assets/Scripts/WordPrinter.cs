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
    //paskutine raide balse
    public bool vowel;

    //basic konstruktorius priskirt reiksmes
    public Printer()
    {
        //nzn galesim is failo ar kokias eilutes skaityt jei lygiams darysim  ir taip zodzius pasirinksim kolkas fixed masyvas.
        words = new List<string> { "dog", "cat", "bike" };
        LetterCount = -1;
        vowel = false;
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

         return index;
    }

    //metodas endless mode kur raides pagal sansa anglu kalboj spawnina
    public int GetEndlessLetter()
    {
        double sum = Random.Range(1, 510);
        int rez = -1;
        List<double> letterNr = new List<double> { 43.31, 10.56, 23.13, 17.25, 56.88, 9.24, 12.59, 15.31, 38.45, 1, 5.61, 27.98, 15.36, 33.92, 36.51, 16.14, 1, 38.64, 29.23, 35.43, 18.51, 5.13, 6.57, 1.57, 9.34, 1.34 };
        List<int> vowels = new List<int> {0, 4, 8, 14, 20, 24};
        while(sum >= 0.01)
        {
            rez++;
            sum = sum-letterNr.ElementAt(rez);
        }
        return rez;
    }
}

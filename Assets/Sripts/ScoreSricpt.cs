using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreSricpt : MonoBehaviour
{
    public static int scoreValue;
    public TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreValue = 0;
        scoreText.text = "Score: " + scoreValue; 
    }

    //Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + scoreValue;
    }
}
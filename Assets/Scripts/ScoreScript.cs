using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public static double scoreValue;
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

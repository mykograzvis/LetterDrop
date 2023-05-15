using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
    public GameObject first;
    public GameObject second;
    public GameObject third;
    public GameObject four;
    public GameObject five;
    public bool isSecond = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackGroundChangerOne()
    {
        if (isSecond == false)
        {
            first.SetActive(true);
            second.SetActive(false);
            third.SetActive(false);
            four.SetActive(false);
            five.SetActive(false);
            isSecond = true;
        }

        else
        {
            first.SetActive(true);
            second.SetActive(false);
            third.SetActive(false);
            four.SetActive(false);
            five.SetActive(false);
            isSecond = false;
        }
    }
    public void BackGroundChangerTwo()
    {
        if (isSecond == false)
        {
            first.SetActive(false);
            second.SetActive(true);
            third.SetActive(false);
            four.SetActive(false);
            five.SetActive(false);
            isSecond = true;
        }

        else
        {
            first.SetActive(false);
            second.SetActive(true);
            third.SetActive(false);
            four.SetActive(false);
            five.SetActive(false);
            isSecond = false;
        }
    }
    public void BackGroundChangerThree()
    {
        if (isSecond == false)
        {
            first.SetActive(false);
            second.SetActive(false);
            third.SetActive(true);
            four.SetActive(false);
            five.SetActive(false);
            isSecond = true;
        }

        else
        {
            first.SetActive(false);
            second.SetActive(false);
            third.SetActive(true);
            four.SetActive(false);
            five.SetActive(false);
            isSecond = false;
        }
    }
    public void BackGroundChangerFour()
    {
        if (isSecond == false)
        {
            first.SetActive(false);
            second.SetActive(false);
            third.SetActive(false);
            four.SetActive(true);
            five.SetActive(false);
            isSecond = true;
        }

        else
        {
            first.SetActive(false);
            second.SetActive(false);
            third.SetActive(false);
            four.SetActive(true);
            five.SetActive(false);
            isSecond = false;
        }
    }
    public void BackGroundChangerFive()
    {
        if (isSecond == false)
        {
            first.SetActive(false);
            second.SetActive(false);
            third.SetActive(false);
            four.SetActive(false);
            five.SetActive(true);
            isSecond = true;
        }

        else
        {
            first.SetActive(false);
            second.SetActive(false);
            third.SetActive(false);
            four.SetActive(false);
            five.SetActive(true);
            isSecond = false;
        }
    }
}
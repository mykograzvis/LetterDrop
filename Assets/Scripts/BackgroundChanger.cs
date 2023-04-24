using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
    public GameObject first;
    public GameObject second;
    public bool isSecond = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackGroundChanger()
    {
	if (isSecond == false)
	{
	    first.SetActive(false);
	    second.SetActive(true);
	    isSecond = true;
	}
	
	else
	{
	    first.SetActive(true);
	    second.SetActive(false);
	    isSecond = false;
	}
    }
}

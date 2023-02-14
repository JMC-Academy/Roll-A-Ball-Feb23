using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    public string winMessage = "You Win";
    int counter = 0;
    public int winAmount = 5;
    
    void Start()
    {
    }
    
    void Update()    
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            counter++;
            if(counter >= winAmount)
            {
                ShowMessage();
            }
        }
    }

    void ShowMessage()
    {
        Debug.Log(winMessage + " " + counter + " times");
    }

}

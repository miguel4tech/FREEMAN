using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonControl : MonoBehaviour
{
    private Button Button;
    private float buttonOpacity = 0.3f;
    private const float cRefResolutionX = 1920.0f;

    // Start is called before the first frame update
    void Start()
    {
        Button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        //set the button Opacity
        Color col = GetComponent<Image>().color;
        col.a = buttonOpacity;
        GetComponent<Image>().color = col;
    }



    //Trying to do the button responsiveness for all mobile screen
    //still in progress
    void OnGUI()
    {
      if(Screen.width >= 700 && Screen.width <= 850)
        {
            GUI.Button(new Rect(4.5f, -81.0f, 600, 40), "Button");
        }

    }
}

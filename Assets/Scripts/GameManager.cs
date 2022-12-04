using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton {set; get;}
    public static bool isGameStarted;

    public GameObject isGameStartedPanel;

    void Awake()
    {
        singleton = this;
        Audiomanager.instance.PlayMusic("Combat-Theme_Africa");
    }



    // Update is called once per frame
    void Update()
    {
        //Disable StartPanel
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isGameStarted)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return;
            }

            isGameStarted = true;
            isGameStartedPanel.SetActive(false);
        }
    }

    public void Quit()
    {
        Debug.Log("Game Exit");
        Application.Quit();
    }
}

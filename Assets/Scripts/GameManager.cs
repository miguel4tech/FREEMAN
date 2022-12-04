using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton {set; get;}
    public static bool isGameStarted;
    public static bool isGamePaused;

    public GameObject isGameStartedPanel;

    void Awake()
    {
        singleton = this;
        Audiomanager.instance.PlayMusic("Combat-Theme_Africa");
    }



    // Update is called once per frame
    void Update()
    {
        //Disable StartPanel at GameStarted
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isGameStarted)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return;
            }

            isGameStarted = true;
            isGameStartedPanel.SetActive(false);
        }

        if(PlayerCombat.gameOver)
        {
            Time.timeScale = 0;
        }
    }

    public void Pause()
    {
        isGamePaused = true;
        Time.timeScale = 0;
    }
    public void Resume()
    {
        isGamePaused = false;
        Time.timeScale = 1;
    }
    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void Quit()
    {
        Debug.Log("Game Exit");
        Application.Quit();
    }
}

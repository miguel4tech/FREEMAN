using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton {set; get;}
    private PlayerCombat playerCombat;
    private Enemy enemyScript;

    public static bool isGameStarted;
    public static bool isGamePaused;

    public bool gameWin;
    public bool gameOver;
    public int enemyTarget, totalEnemyKilled;



    public GameObject gameOverPanel;
    public GameObject GameStartedPanel;

    void Awake()
    {
		if(singleton== null)
		    singleton = this;
		else 
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
        Audiomanager.instance.PlayMusic("Combat-Theme_Africa");
    }

    void Start()
    {
        gameWin = gameOver = isGamePaused = false;
        playerCombat = FindObjectOfType<PlayerCombat>();
        enemyScript = FindObjectOfType<Enemy>();
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
            GameStartedPanel.SetActive(false);
        }

        if(totalEnemyKilled >= enemyTarget && !gameWin)
        {
            gameWin = true;
            playerCombat.LevelComplete();
            int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            if(PlayerPrefs.GetInt("CurrentLevel", 1) < nextLevel)
                PlayerPrefs.SetInt("CurrentLevel", nextLevel);
            SceneManager.LoadScene(nextLevel);
        }

        if(gameOver)
        {
            gameOverPanel.SetActive(true);
            enemyScript.enemyAnim.enabled = false;
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
        print("Game Exit");
        Application.Quit();
    }
}

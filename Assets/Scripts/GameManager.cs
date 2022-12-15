using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.CompilerServices;

delegate void spawnDelegate();
public class GameManager : MonoBehaviour 
{
    public static GameManager singleton {set; get;}
    public static bool isGameStarted;
    public static bool isGamePaused;

    public GameObject gameOverPanel;
    public GameObject GameStartedPanel;
    private PlayerCombat playerCombat;
    private SpawnManager spawnManager;
    private float timer;
    public static bool levelComplete;



    void Awake()
    {
		if(singleton == null)
		    singleton = this;
		else 
		{
			Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
		}
    }

    private void Start()
    {
        ResetLevel();
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

        if (PlayerCombat.gameOver)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }

        spawnDelegate enemiesSpawn = spawnManager.SpawnEnemies;
        //spawn Enemies
        if(spawnManager.spawnComplete == true)
        {
            LevelComplete();
        }
        enemiesSpawn();
        //Check if level is complete

       
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

    void ResetLevel()
    {
        playerCombat =  FindObjectOfType<PlayerCombat>();
        isGameStarted = false;
        spawnManager = FindObjectOfType<SpawnManager>();
    }
    private void onLevelFinishedLoading(Scene scene, LoadSceneMode loadSceneMode)
    {
        ResetLevel();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += onLevelFinishedLoading;
    }

    void LevelComplete()
    {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.Euler(0, 180, 0);
            playerCombat.anim.SetBool("Victory", true);
            //Victory
            levelComplete = true;
            timer += Time.deltaTime;
        if (timer > 5) // 5 seconds
        {
            int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;

            if (nextLevel == 4)
            {
                SceneManager.LoadScene(1); //Returns to MainMenu
                return;
            }

            if (PlayerPrefs.GetInt("CurrentLevel", 1) < nextLevel)
                PlayerPrefs.SetInt("CurrentLevel", nextLevel);

            SceneManager.LoadScene(nextLevel);
        }
    }

}

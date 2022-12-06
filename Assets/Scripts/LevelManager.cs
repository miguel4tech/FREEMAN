using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager singleton;

    //Awake is called even before Start
    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckLoadingScreen();
    }/// <summary>
    /// Checks if it is the loading scene
    /// </summary>
    void CheckLoadingScreen()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            StartCoroutine("LoadNextScene");
    }

    //Go to the next scene(MainMenu) after 4seconds
    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadNewScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        print("Game Exit");
        Application.Quit();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    private void Awake()
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
    }

    /// <summary>
    /// Checks if it is the loading scene
    /// </summary>
    void CheckLoadingScreen()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            StartCoroutine("LoadNextScene");
    }

    //Go to the next scene after 7s
    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadNewScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Debug.Log("Game Exit");
        Application.Quit();
    }
}

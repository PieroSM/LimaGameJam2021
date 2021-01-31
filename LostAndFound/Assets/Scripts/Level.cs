using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float gameOverDelay = 1f;

    public int lastLevelIndex = 1;

    //void Awake()
    //{
    //    int numGameSessions = FindObjectsOfType<Level>().Length;
    //    if (numGameSessions > 1)
    //    {
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        DontDestroyOnLoad(gameObject);
    //    }
    //}

    //public void SetLastLevelIndex(int index)
    //{
    //    lastLevelIndex = index;
    //}

    //public void LoadLastLevel()
    //{
    //    Debug.Log(lastLevelIndex);
    //    SceneManager.LoadScene(lastLevelIndex);
    //}

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadGameOver()
    {
        StartCoroutine(DelayGameOver());
    }

    IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(gameOverDelay);
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
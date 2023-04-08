using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using Game;

public class MainMenu : MonoBehaviour
{
    

    public void PlayGameEasy(string Game)
    {
        newGame.width = 10;
        newGame.height = 10;
        newGame.mineCount = 20;
        SceneManager.LoadScene(Game);
    }

    public void PlayGameMedium(string Game)
    {
        newGame.width = 16;
        newGame.height = 16;
        newGame.mineCount = 32;
        SceneManager.LoadScene(Game);
    }

    public void PlayGameHard(string Game)
    {
        newGame.width = 25;
        newGame.height = 25;
        newGame.mineCount = 100;
        SceneManager.LoadScene(Game);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

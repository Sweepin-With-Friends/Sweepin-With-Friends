using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using Game;

public class MainMenu : Game
{
    public static int width = 10;
    public static int height = 10;
    public static int mineCount = 20;

    public void PlayGameEasy(string Game)
    {
        width = 10;
        height = 10;
        mineCount = 20;
        
       

    }

    public void PlayGameMedium(string Game)
    {
        width = 16;
        height = 16;
        mineCount = 32;
        Start();
        
    }

    public void PlayGameHard(string Game)
    {
        width = 25;
        height = 25;
        mineCount = 100;
        Start();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

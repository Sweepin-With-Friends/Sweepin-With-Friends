using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public void LoadGame(string Game)
    {
        SceneManager.LoadScene(Game);
        
    }
    public void LoadSound(string Sound)
    {
        SceneManager.LoadScene(Sound, LoadSceneMode.Additive);
    }

    public void LoadMenu(string Menu)
    {
        SceneManager.LoadScene(Menu);
    }

}

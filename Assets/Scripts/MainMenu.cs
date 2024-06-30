/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 25/06/2024
 * Description: This script is used for functionality of the Main Menu
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the functionality of the Main Menu.
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Loads the Main Menu scene and restarts the game manager values.
    /// </summary>
    public void LoadMainMenu()
    {
        GameManager.instance.MainMenuRestart();
        SceneManager.LoadScene(0);
    }
    
    /// <summary>
    /// Quits the game and logs a quit message.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}


/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 25/06/2024
 * Description: This script is used whenever a scene change is required
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages scene transitions based on collectible conditions.
/// </summary>
public class ChangeScene : MonoBehaviour
{
    /// <summary>
    /// Indicates if the first collectible has been collected.
    /// </summary>
    public static bool oneCollected = false;

    /// <summary>
    /// Indicates if the second collectible has been collected.
    /// </summary>
    public static bool twoCollected = false;

    /// <summary>
    /// Indicates if the third collectible has been collected.
    /// </summary>
    public static bool threeCollected = false;

    /// <summary>
    /// The index of the scene to change to.
    /// </summary>
    public int sceneToChange;

    /// <summary>
    /// The message displayed when Collectible hasnt been collected.
    /// </summary>
    public GameObject blockOffMessage;

    /// <summary>
    /// Called when aPlayer enters the trigger collider attached to this object.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // if 10 gears have been colleceted teleport the player to Scene 3 
            if (sceneToChange == 3 && oneCollected)
            {
                SceneManager.LoadScene(sceneToChange);
                GameManager.instance.LoadingScene();
                return;
            }

            // if collectible hasnt been collected, display block off message.
            if (!oneCollected)
            {
                blockOffMessage.gameObject.SetActive(true);
            }

            // if Crystal has been colleceted teleport the player to Scene 4
            if (sceneToChange == 4 && twoCollected)
            {
                SceneManager.LoadScene(sceneToChange);
                GameManager.instance.LoadingScene();
                return;
            }

            // if Crystal has been colleceted teleport the player to Scene 5
            if (sceneToChange == 5 && twoCollected)
            {
                SceneManager.LoadScene(sceneToChange);
                GameManager.instance.LoadingScene();
                return;
            }

            // if collectible hasnt been collected, display block off message.
            if (!twoCollected)
            {
                blockOffMessage.gameObject.SetActive(true);
            }

            // if 10 wood have been colleceted teleport the player to Scene 3 
            if (sceneToChange == 6 && threeCollected)
            {
                SceneManager.LoadScene(sceneToChange);
                GameManager.instance.LoadingScene();
                return;
            }

            // if collectible hasnt been collected, display block off message.
            if (!threeCollected)
            {
                blockOffMessage.gameObject.SetActive(true);
            }

            //When player dies show cursor and change scene to game over scene
            if (sceneToChange == 2)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene(sceneToChange);
            }
        }
    }

    /// <summary>
    /// Called when another collider exits the trigger collider attached to this object.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        blockOffMessage.gameObject.SetActive(false);
    }

    /// <summary>
    /// Marks the first scene as done, indicating the first collectible has been collected.
    /// </summary>
    public static void FirstSceneDone()
    {
        oneCollected = true;
    }

    /// <summary>
    /// Marks the second scene as done, indicating the second collectible has been collected.
    /// </summary>
    public static void SecondSceneDone()
    {
        twoCollected = true;
    }

    /// <summary>
    /// Marks the third scene as done, indicating the third collectible has been collected.
    /// </summary>
    public static void ThirdSceneDone()
    {
        threeCollected = true;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public static bool oneCollected = false;
    public static bool twoCollected = false;
    public static bool threeCollected = false;

    public int sceneToChange;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (sceneToChange == 3 && oneCollected)
            {
                SceneManager.LoadScene(sceneToChange);
            }
            else if (sceneToChange == 2)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene(sceneToChange);
            }
        }
    }

    public static void FirstSceneDone()
    {
        oneCollected = true;
    }
}


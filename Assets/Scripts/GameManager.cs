/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 25/06/2024
 * Description: This script is used for managing the game state, UI, scoring, player health, and dialogues 
 * as well as carrying over that data from one scene to another
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Manages the game state, UI, scoring, player health, and dialogues.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    /// <summary>
    /// Current number of Gear collectible.
    /// </summary>
    private int currentScoreOne;

    /// <summary>
    /// Current number of Crystal Collectible
    /// </summary>
    private int currentScoreTwo;

    /// <summary>
    /// Current number of Wood collectible.
    /// </summary>
    private int currentScoreThree;

    /// <summary>
    /// Used to adjust ammo count display.
    /// </summary>
    private int ammoMinus;

    /// <summary>
    /// Time duration for dialogues to appear.
    /// </summary>
    public float dialogueTime = 3f;

    /// <summary>
    /// Text display for Gear collectible score.
    /// </summary>
    public TextMeshProUGUI collectibleOneText;

    /// <summary>
    /// Text display Crystal collectible 
    /// </summary>
    public TextMeshProUGUI collectibleTwoText;

    /// <summary>
    /// Text display for wood collectible score.
    /// </summary>
    public TextMeshProUGUI collectibleThreeText;

    /// <summary>
    /// Text display for ammo count.
    /// </summary>
    public TextMeshProUGUI ammoCount;

    /// <summary>
    /// Flag indicating if the inventory is currently open.
    /// </summary>
    bool inventoryOn;

    /// <summary>
    /// Inventory UI GameObject.
    /// </summary>
    public GameObject inventory;

    /// <summary>
    /// Main UI GameObject.
    /// </summary>
    public GameObject UI;

    /// <summary>
    /// Main menu UI GameObject.
    /// </summary>
    public GameObject mainMenuUI;

    /// <summary>
    /// Game over UI GameObject.
    /// </summary>
    public GameObject gameOverUI;

    /// <summary>
    /// Game end UI GameObject.
    /// </summary>
    public GameObject gameEnd;

    /// <summary>
    /// Loading screen GameObject.
    /// </summary>
    public GameObject loadingScreen;

    //Dialogue text
    /// <summary>
    /// GameObject for starting dialogue.
    /// </summary>
    public GameObject startingText;

    /// <summary>
    /// GameObject for gears message dialogue.
    /// </summary>
    public GameObject gearsMessage;

    /// <summary>
    /// GameObject for first message dialogue.
    /// </summary>
    public GameObject oneMessage;

    /// <summary>
    /// GameObject for second message dialogue.
    /// </summary>
    public GameObject twoMessage;

    /// <summary>
    /// GameObject for third message dialogue.
    /// </summary>
    public GameObject threeMessage;

    /// <summary>
    /// GameObject for crystal message dialogue.
    /// </summary>
    public GameObject crystalMessage;

    /// <summary>
    /// GameObject for after crystal message dialogue.
    /// </summary>
    public GameObject afterCrystalMessage;

    /// <summary>
    /// GameObject for forest message dialogue.
    /// </summary>
    public GameObject forestMessage;

    /// <summary>
    /// GameObject for end game message dialogue.
    /// </summary>
    public GameObject goGomeMessage;

    //Cutscene text
    /// <summary>
    /// GameObject for cutscene text 1.
    /// </summary>
    public GameObject cutsceneText1;

    /// <summary>
    /// GameObject for cutscene text 2.
    /// </summary>
    public GameObject cutsceneText2;

    /// <summary>
    /// GameObject for cutscene text 3.
    /// </summary>
    public GameObject cutsceneText3;

    /// <summary>
    /// GameObject for cutscene text 4.
    /// </summary>
    public GameObject cutsceneText4;

    /// <summary>
    /// GameObject for cutscene text 5.
    /// </summary>
    public GameObject cutsceneText5;

    /// <summary>
    /// GameObject for cutscene background.
    /// </summary>
    public GameObject cutsceneBackground;

    bool gearsMessageShown = false;
    bool crystalMessageShown = false;
    bool startingMessageShown = false;
    bool forestMessageShown = false;

    /// <summary>
    /// Current health of the player.
    /// </summary>
    public float health;

    /// <summary>
    /// Slider UI for displaying health.
    /// </summary>
    public Slider slider;

    /// <summary>
    /// GameObject for health panel at 75%.
    /// </summary>
    public GameObject panel75;

    /// <summary>
    /// GameObject for health panel at 50%.
    /// </summary>
    public GameObject panel50;

    /// <summary>
    /// GameObject for health panel at 25%.
    /// </summary>
    public GameObject panel25;

    /// <summary>
    /// GameObject for health panel at 0%.
    /// </summary>
    public GameObject panel0;

    /// <summary>
    /// Field for Background music
    /// </summary>
    [SerializeField]
    private AudioSource BGMAudio;

    /// <summary>
    /// Field for Player Death audio
    /// </summary>
    [SerializeField]
    private AudioSource deathAudio;

    /// <summary>
    /// Field for Player hurt audio
    /// </summary>
    [SerializeField]
    private AudioSource hurtAudio;

    bool musicStop = false;
    bool cutscenePlayed = false;

    /// <summary>
    /// Initialize GameManager instance and prevent destruction on scene load.
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    private void Start()
    {
        // Initialize variables
        currentScoreOne = 0;
        currentScoreTwo = 0;
        currentScoreThree = 0;
        ammoMinus = 0;
        inventoryOn = false;
        health = 100f;
        UI.gameObject.SetActive(false);
        mainMenuUI.gameObject.SetActive(true);
        gameOverUI.gameObject.SetActive(false);
        gameEnd.gameObject.SetActive(false);
        //StartCoroutine(StartingMessage());
    }

    /// <summary>
    /// Restart the game after main menu or game over.
    /// </summary>
    public void RestartGame()
    {   
        // Hide cursor
        Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

        //Play cutscene if havent played before
        if (!cutscenePlayed)
        {
            StartCoroutine(CutscenePlaying());
            cutscenePlayed = true;
            return;
        }

        //Reset UI
        mainMenuUI.gameObject.SetActive(false);
        panel0.gameObject.SetActive(false);
        panel25.gameObject.SetActive(false);
        panel50.gameObject.SetActive(false);
        panel75.gameObject.SetActive(false);

        //To stop Music from replaying if cutscene played
        if (!musicStop)
        {
            BGMAudio.Play();
        }

        //Reset Values and UI
        health = 100f;
        slider.value = health;
        ammoCount.text =  "30 / 30";
        gearsMessageShown = false;
        inventory.gameObject.SetActive(false);
        Time.timeScale = 1;
        inventoryOn = false;
        currentScoreOne = 0;
        collectibleOneText.text = currentScoreOne.ToString() + "/10";
        currentScoreTwo = 0;
        collectibleTwoText.text = currentScoreTwo.ToString() + "/1";
        currentScoreThree = 0;
        collectibleThreeText.text = currentScoreThree.ToString() + "/10";
        //startingMessageShown = false;
        UI.gameObject.SetActive(true);
        gameOverUI.gameObject.SetActive(false);
       // loadingScreen.gameObject.SetActive(false);
        gearsMessage.gameObject.SetActive(false);
        crystalMessage.gameObject.SetActive(false);
        afterCrystalMessage.gameObject.SetActive(false);
        forestMessage.gameObject.SetActive(false);
        goGomeMessage.gameObject.SetActive(false);
        oneMessage.gameObject.SetActive(false);
        twoMessage.gameObject.SetActive(false);
        SceneManager.LoadScene(1);
        musicStop = false;
        //Display StartingMessage
        StartCoroutine(StartingMessage());
    }

    /// <summary>
    /// Play the cutscene when restarting the game.
    /// </summary>
    IEnumerator CutscenePlaying()
    {
        Debug.Log("Cutscene");
        cutsceneBackground.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        cutsceneText1.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        cutsceneText1.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        cutsceneText2.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        cutsceneText2.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        cutsceneText3.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        cutsceneText3.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        cutsceneText4.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        cutsceneText4.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        cutsceneText5.gameObject.SetActive(true);
        BGMAudio.Play();
        yield return new WaitForSeconds(5f);
        cutsceneText5.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        cutsceneBackground.gameObject.SetActive(false);
        musicStop = true;
        RestartGame();
    }

    /// <summary>
    /// Check if the inventory is currently open.
    /// </summary>
    public bool IsInventoryOpen()
    {
        return inventoryOn;
    }

    /// <summary>
    /// Return to main menu and reset game state.
    /// </summary>
    public void MainMenuRestart()
    {
        //Reset UI and values wheb returning back to the main menu
        mainMenuUI.gameObject.SetActive(true);
        panel0.gameObject.SetActive(false);
        panel25.gameObject.SetActive(false);
        panel50.gameObject.SetActive(false);
        panel75.gameObject.SetActive(false);
        health = 100f;
        slider.value = health;
        ammoCount.text =  "30 / 30";
        gearsMessageShown = false;
        //startingMessageShown = false;
        gearsMessage.gameObject.SetActive(false);
        inventory.gameObject.SetActive(false);
        Time.timeScale = 1;
        inventoryOn = false;
        UI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(false);
        loadingScreen.gameObject.SetActive(false);
        StartCoroutine(StartingMessage());
    }

    /// <summary>
    /// Display starting message when the game begins.
    /// </summary>
    IEnumerator StartingMessage()
    {
        startingText.gameObject.SetActive(true);
        yield return new WaitForSeconds(dialogueTime);
        startingText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Display specific dialogues based on the given dialogue number.
    /// </summary>
    public void Message(int textNumber)
    {
        StartCoroutine(DialogueActiviate(textNumber));
    }

    /// <summary>
    /// Show loading screen when transitioning between scenes.
    /// </summary>
    public void LoadingScene()
    {
        loadingScreen.gameObject.SetActive(true);
    }

    /// <summary>
    /// Activate dialogues based on dialogue number.
    /// </summary>
    IEnumerator DialogueActiviate(int dialogueNumber)
    {
        //Turn off loading scene when scene is loaded
        if (dialogueNumber == 1)
        {
            loadingScreen.gameObject.SetActive(false);
        }

        //Prevent message being displayed if player re enters trigger
        if (dialogueNumber == 2 && !gearsMessageShown)
        {
            gearsMessage.gameObject.SetActive(true);
            yield return new WaitForSeconds(dialogueTime);
            gearsMessage.gameObject.SetActive(false);
            gearsMessageShown = true;
        }

        if (dialogueNumber == 3)
        {
           oneMessage.gameObject.SetActive(true);
           yield return new WaitForSeconds(dialogueTime);
           oneMessage.gameObject.SetActive(false);
        }
        
        if (dialogueNumber == 4)
        {
           twoMessage.gameObject.SetActive(true);
           yield return new WaitForSeconds(dialogueTime);
           twoMessage.gameObject.SetActive(false);
        }

        if (dialogueNumber == 5)
        {
           threeMessage.gameObject.SetActive(true);
           yield return new WaitForSeconds(dialogueTime);
           threeMessage.gameObject.SetActive(false);
        }

        //Prevent message being displayed if player re enters trigger
        if (dialogueNumber == 6 && !crystalMessageShown)
        {
           crystalMessage.gameObject.SetActive(true);
           yield return new WaitForSeconds(dialogueTime);
           crystalMessage.gameObject.SetActive(false);
           crystalMessageShown = true;
        }
        if (dialogueNumber == 7)
        {
           afterCrystalMessage.gameObject.SetActive(true);
           yield return new WaitForSeconds(dialogueTime);
           afterCrystalMessage.gameObject.SetActive(false);
        }

        //Prevent message being displayed if player re enters trigger
        if (dialogueNumber == 8 && !forestMessageShown)
        {
           forestMessage.gameObject.SetActive(true);
           yield return new WaitForSeconds(dialogueTime);
           forestMessage.gameObject.SetActive(false);
           forestMessageShown = true;
        }
        if (dialogueNumber == 9)
        {
           goGomeMessage.gameObject.SetActive(true);
           yield return new WaitForSeconds(dialogueTime);
           goGomeMessage.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Increase score for collectible one and trigger related events.
    /// </summary>
    public void IncreaseScoreOne(int scoreToAdd)
    {
        currentScoreOne += scoreToAdd;
        collectibleOneText.text = currentScoreOne.ToString() + "/10";

        if (currentScoreOne == 10)
        {
            ChangeScene.FirstSceneDone();
            StartCoroutine(DialogueActiviate(3));
        }
    }

    /// <summary>
    /// Increase score for collectible two and trigger related events.
    /// </summary>
    public void IncreaseScoreTwo(int scoreToAdd)
    {
        currentScoreTwo += scoreToAdd;
        collectibleTwoText.text = currentScoreTwo.ToString() + "/1";

        if (currentScoreTwo == 1)
        {
           ChangeScene.SecondSceneDone();
           StartCoroutine(DialogueActiviate(4));
        }
    }

    /// <summary>
    /// Increase score for collectible three and trigger related events.
    /// </summary>
    public void IncreaseScoreThree(int scoreToAdd)
    {
        currentScoreThree += scoreToAdd;
        collectibleThreeText.text = currentScoreThree.ToString() + "/10";

        if (currentScoreThree == 10)
        {
           ChangeScene.ThirdSceneDone();
           StartCoroutine(DialogueActiviate(5));
        }
    }

    /// <summary>
    /// Adjust ammo count display.
    /// </summary>
    public void Ammo(int ammoSet)
    {
        ammoMinus = ammoSet - 1;
        ammoCount.text = ammoMinus.ToString() + " / 30";
    }

    /// <summary>
    /// Increase player health and update UI accordingly.
    /// </summary>
    public void HealthGain(int healthToAdd)
    {
        //If player health is already full, dont increase it
        if (health == 100)
        {
            return;
        }

        //Increase health and disable low health panels
        health += healthToAdd;
        slider.value = health;
        if (health == 100)
        {
            panel75.gameObject.SetActive(false);
        }

        if (health == 75)
        {
            panel75.gameObject.SetActive(true);
            panel50.gameObject.SetActive(false);
        }

        if (health == 50)
        {
            panel25.gameObject.SetActive(false);
            panel50.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Toggle the inventory on and off.
    /// </summary>
    public void OpenInventory()
    {
        if (!inventoryOn)
        {
            // Pause the game and show the inventory
            Time.timeScale = 0;
            inventory.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            inventoryOn = true;
        }
        else
        {
            // Resume the game and hide the inventory
            Time.timeScale = 1;
            inventory.gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            inventoryOn = false;
        }
    }

    /// <summary>
    /// Inflict damage on the player and update UI accordingly.
    /// </summary>
    public void TakeDamageUI(float amount)
    {
        hurtAudio.Play();
        health -= amount;
        slider.value = health;
        if (health == 75)
        {
            panel75.gameObject.SetActive(true);
        }

        if (health == 50)
        {
            panel75.gameObject.SetActive(false);
            panel50.gameObject.SetActive(true);
        }

        if (health == 25)
        {
            panel50.gameObject.SetActive(false);
            panel25.gameObject.SetActive(true);
        }

        if (health <= 0f)
        {
            panel0.gameObject.SetActive(true);
            Die();
        }
    }

    /// <summary>
    /// Handle player death events.
    /// </summary>
    public void Die()
    {
        SceneManager.LoadScene(2);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        BGMAudio.Play();
        deathAudio.Play();
        BGMAudio.Stop();
        gameOverUI.gameObject.SetActive(true);
        UI.gameObject.SetActive(false);
    }

    /// <summary>
    /// End the game and display end game UI.
    /// </summary>
    public void EndGame()
    {
        SceneManager.LoadScene(7);
        UI.gameObject.SetActive(false);
        gameEnd.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}



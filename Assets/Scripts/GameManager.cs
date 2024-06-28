using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int currentScoreOne;
    private int currentScoreTwo;
    private int currentScoreThree;

    private int ammoMinus;

    public TextMeshProUGUI collectibleOneText;
    public TextMeshProUGUI collectibleTwoText;
    public TextMeshProUGUI collectibleThreeText;
    public TextMeshProUGUI ammoCount;

    bool inventoryOn;
    public GameObject inventory;

    public float health;

    public Slider slider;

    public GameObject panel75;
    public GameObject panel50;
    public GameObject panel25;
    public GameObject panel0;

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

    private void Start()
    {
        // Initialize variables
        currentScoreOne = 0;
        currentScoreTwo = 0;
        currentScoreThree = 0;
        ammoMinus = 0;
        inventoryOn = false;
        health = 100f;
    }

    public void IncreaseScoreOne(int scoreToAdd)
    {
        // Increase the score of the player by scoreToAdd
        currentScoreOne += scoreToAdd;
        collectibleOneText.text = currentScoreOne.ToString() + "/10";

        if (currentScoreOne == 10)
        {
            ChangeScene.FirstSceneDone();
        }
    }

    public void IncreaseScoreTwo(int scoreToAdd)
    {
        // Increase the score of the player by scoreToAdd
        currentScoreTwo += scoreToAdd;
        collectibleTwoText.text = currentScoreTwo.ToString() + "/1";

        if (currentScoreTwo == 1)
        {
            //ChangeScene.SecondSceneDone();
        }
    }

    public void IncreaseScoreThree(int scoreToAdd)
    {
        // Increase the score of the player by scoreToAdd
        currentScoreThree += scoreToAdd;
        collectibleThreeText.text = currentScoreThree.ToString() + "/10";

        if (currentScoreThree == 10)
        {
            //ChangeScene.ThirdSceneDone();
        }
    }

    public void Ammo(int ammoSet)
    {
        ammoMinus = ammoSet - 1;
        ammoCount.text = ammoMinus.ToString() + " / 30";
    }

    public void HealthGain(int healthToAdd)
    {
        if (health == 100)
        {
            return;
        }

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

    public void OpenInventory()
    {
        if (!inventoryOn)
        {
            inventory.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            inventoryOn = true;
            return;
        }

        if (inventoryOn)
        {
            inventory.gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            inventoryOn = false;
        }
    }

    public void TakeDamageUI(float amount)
    {
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

    public void Die()
    {
        // Handle player death (e.g., respawn, game over)
        SceneManager.LoadScene(2);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}


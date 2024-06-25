using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;

    public TextMeshProUGUI healthText;

    public Slider slider;

    public GameObject panel75;
    public GameObject panel50;
    public GameObject panel25;
    public GameObject panel0;


    public void TakeDamage(float amount)
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

    void Die()
    {
        // Handle player death (e.g., respawn, game over)
        Debug.Log("Player Died");
    }

    void Start()
    {
        panel75.gameObject.SetActive(false);
        panel50.gameObject.SetActive(false);
        panel25.gameObject.SetActive(false);
        panel0.gameObject.SetActive(false);
    }
}

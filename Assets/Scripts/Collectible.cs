/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 25/06/2024
 * Description: This script is used for the different collectibles 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a collectible item that the player can interact with.
/// </summary>
public class Collectible : Interactable
{
    /// <summary>
    /// The audio clip that plays when the collectible is collected.
    /// </summary>
    [SerializeField]
    private AudioClip collectAudio;

    /// <summary>
    /// To keep track of the number of collectibles
    /// </summary>
    public int myScore = 5;

    /// <summary>
    /// The type of the collectible, used to determine specific interactions.
    /// </summary>
    public int collectibleType;

    /// <summary>
    /// The final collectible object to activate when certain conditions are met.
    /// </summary>
    public GameObject finalCollectible;

    /// <summary>
    /// Handles the collection of the collectible.
    /// </summary>
    public void Collected()
    {
        // Destroy the attached GameObject
        Destroy(gameObject);
    }

    /// <summary>
    /// Overrides the Interact method to handle interactions with the player.
    /// </summary>
    public override void Interact(Player thePlayer)
    {
        float volume = 1.0f; // Set volume to maximum (you can adjust this value as needed)

        // if the collectible is a gear
        if (collectibleType == 1)
        {
            base.Interact(thePlayer);
            GameManager.instance.IncreaseScoreOne(myScore);
            AudioSource.PlayClipAtPoint(collectAudio, transform.position, volume);
            Collected();
        }

        // if the collectible is the crystal
        if (collectibleType == 2)
        {
            base.Interact(thePlayer);
            GameManager.instance.IncreaseScoreTwo(myScore);
            AudioSource.PlayClipAtPoint(collectAudio, transform.position, volume);
            Collected();
        }

        // if the collectible is wood
        if (collectibleType == 3)
        {
            base.Interact(thePlayer);
            GameManager.instance.IncreaseScoreThree(myScore);
            AudioSource.PlayClipAtPoint(collectAudio, transform.position, volume);
            Collected();
        }

        // if the collectible to place down the other collectibles
        if (collectibleType == 4)
        {
            base.Interact(thePlayer);
            finalCollectible.gameObject.SetActive(true);
            AudioSource.PlayClipAtPoint(collectAudio, transform.position, volume);
            Destroy(gameObject);
        }

        // if the collectible is to end the game
        if (collectibleType == 5)
        {
            AudioSource.PlayClipAtPoint(collectAudio, transform.position, volume);
            GameManager.instance.EndGame();
        }
    }
}


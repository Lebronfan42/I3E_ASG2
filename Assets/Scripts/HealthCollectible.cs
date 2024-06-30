/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 25/06/2024
 * Description: This script is used for the health collectible dropped by enemies
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages health pickups and their interactions with the player.
/// </summary>
public class Health : Interactable
{
    /// <summary>
    /// The audio clip to play when the health is collected.
    /// </summary>
    [SerializeField]
    private AudioClip collectAudio;

    /// <summary>
    /// The amount of health gained when collected.
    /// </summary>
    public int healthGain = 25;

    /// <summary>
    /// Destroys the attached GameObject.
    /// </summary>
    public void Collected()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Interacts with the player, increasing their health and playing the collection audio.
    /// </summary>
    public override void Interact(Player thePlayer)
    {
        base.Interact(thePlayer);
        GameManager.instance.HealthGain(healthGain);
        AudioSource.PlayClipAtPoint(collectAudio, transform.position, 1f);
        Collected();
    }
}


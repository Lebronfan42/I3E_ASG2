/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 25/06/2024
 * Description: This script is used to trigger different dialogues
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for triggering dialogues when the player enters a specific area.
/// </summary>
public class DialogeTrigger : MonoBehaviour
{
    /// <summary>
    /// The dialogue number to trigger when the player enters the trigger area.
    /// </summary>
    public int dialogeNumber;

    /// <summary>
    /// Called when the player enters the trigger collider attached to the object where this script is attached.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.Message(dialogeNumber);
        }
    }
}


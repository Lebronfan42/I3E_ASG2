/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 25/06/2024
 * Description: This script is used for managing player inputs
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Manages player inputs and interactions within the game.
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// The current interactable object the player is looking at.
    /// </summary>
    Interactable currentInteractable;

    /// <summary>
    /// The player's camera transform used for Raycast.
    /// </summary>
    [SerializeField]
    Transform playerCamera;

    /// <summary>
    /// The maximum distance at which the player can interact with objects.
    /// </summary>
    [SerializeField]
    float interactionDistance;

    /// <summary>
    /// The text that appears when the player can interact with an object.
    /// </summary>
    [SerializeField]
    TextMeshProUGUI interactionText;

    /// <summary>
    /// Updates the current interactable object based on the player's aim.
    /// </summary>
    public void Update()
    {
        //Uses Raycast to detect object in players view
        RaycastHit hitInfo;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hitInfo, interactionDistance))
        {
            //Show interaction text if interactable object is in player view
            if (hitInfo.transform.TryGetComponent<Interactable>(out currentInteractable))
            {
                interactionText.gameObject.SetActive(true);
            }
            else
            {
                currentInteractable = null;
                interactionText.gameObject.SetActive(false);
            }
        }
        else
        {
            currentInteractable = null;
            interactionText.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Updates the current interactable object.
    /// </summary>
    public void UpdateInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
    }

    /// <summary>
    /// Inflicts damage to the player.
    /// </summary>
    public void TakeDamage(float amount)
    {
        Debug.Log("TakeDamage");
        GameManager.instance.TakeDamageUI(amount);
    }

    /// <summary>
    /// Interacts with the current interactable object.
    /// </summary>
    void OnInteract()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact(this);
        }
    }

    /// <summary>
    /// Toggles the player's inventory.
    /// </summary>
    void OnInventory()
    {
        GameManager.instance.OpenInventory();
    }
}


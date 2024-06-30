/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 25/06/2024
 * Description: This script is used for anything interactible
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //<summary>
    //Stores the current player that can interact with the object
    //</summary>
    protected Player currentPlayer;

    //<summary>
    //Update the player's Interactable
    //</summary>
    public void UpdatePlayerInteractable(Player thePlayer)
    {
        thePlayer.UpdateInteractable(this);
    }

    //<summary>
    // Remove the player's Interactable
    //</summary>
    public void RemovePlayerInteractable(Player thePlayer)
    {
        thePlayer.UpdateInteractable(null);
    }

    //<summary>
    // Execute the object's interaction
    //</summary>
    public virtual void Interact(Player thePlayer)
    {
        Debug.Log(gameObject.name + " was interacted with.");
    }
}

/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 25/06/2024
 * Description: This script is used for spawining in a new collectible after main on is interacted with
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftBox : Interactable
{
    /// <summary>
    /// The collectible GameObject to spawn.
    /// </summary>
    [SerializeField]
    private GameObject collectibleToSpawn;

    /// <summary>
    /// The audio clip to play when the collectible is collected.
    /// </summary>
    [SerializeField]
    private AudioClip collectAudio;

    /// <summary>
    /// Interact with the gift box, spawn the collectible, and play the collection audio.
    /// </summary>
    public override void Interact(Player thePlayer)
    {
        base.Interact(thePlayer);
        SpawnCollectible();
        AudioSource.PlayClipAtPoint(collectAudio, transform.position, 1f);
        Destroy(gameObject);
    }

    /// <summary>
    /// Spawn the collectible at the gift box's position.
    /// </summary>
    void SpawnCollectible()
    {
        Instantiate(collectibleToSpawn, transform.position, collectibleToSpawn.transform.rotation);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : Interactable
{

    [SerializeField]
    private AudioClip collectAudio;

    public int myScore = 5;


    public void Collected()
    {
        // Destroy the attached GameObject
        Destroy(gameObject);
    }


    public override void Interact(Player thePlayer)
    {
        base.Interact(thePlayer);
        GameManager.instance.IncreaseScore(myScore);
        AudioSource.PlayClipAtPoint(collectAudio, transform.position, 1f);
        Collected();
    }




}

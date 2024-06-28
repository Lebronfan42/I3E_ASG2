
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : Interactable
{

    [SerializeField]
    private AudioClip collectAudio;

    public int myScore = 5;

    public int collectibleType;


    public void Collected()
    {
        // Destroy the attached GameObject
        Destroy(gameObject);
    }


    public override void Interact(Player thePlayer)
    {
        if (collectibleType == 1)
        {
            base.Interact(thePlayer);
            GameManager.instance.IncreaseScoreOne(myScore);
            AudioSource.PlayClipAtPoint(collectAudio, transform.position, 1f);
            Collected();
        }
        if (collectibleType == 2)
        {
            base.Interact(thePlayer);
            GameManager.instance.IncreaseScoreTwo(myScore);
            AudioSource.PlayClipAtPoint(collectAudio, transform.position, 1f);
            Collected();
        }
        if (collectibleType == 3)
        {
            base.Interact(thePlayer);
            GameManager.instance.IncreaseScoreThree(myScore);
            AudioSource.PlayClipAtPoint(collectAudio, transform.position, 1f);
            Collected();
        }
        
    }




}

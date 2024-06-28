
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Interactable
{

    [SerializeField]
    private AudioClip collectAudio;

    public int healthGain = 25;


    public void Collected()
    {
        // Destroy the attached GameObject
        Destroy(gameObject);
    }


    public override void Interact(Player thePlayer)
    {
        base.Interact(thePlayer);
        GameManager.instance.HealthGain(healthGain);
        AudioSource.PlayClipAtPoint(collectAudio, transform.position, 1f);
        Collected();
    }




}

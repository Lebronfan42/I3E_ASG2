/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 25/06/2024
 * Description: This script is used for the guns and its functions
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages gun functionality including shooting, reloading, and ammo management.
/// </summary>
public class Gun : MonoBehaviour
{
    /// <summary>
    /// The audio clip to play when the gun is fired.
    /// </summary>
    [SerializeField]
    private AudioClip gunAudio;

    /// <summary>
    /// The second audio clip to play when the gun is fired.
    /// </summary>
    [SerializeField]
    private AudioClip gunAudio2;

    /// <summary>
    /// The audio clip to play when the gun is reloaded.
    /// </summary>
    [SerializeField]
    private AudioClip reloadAudio;
    
    /// <summary>
    /// The damage inflicted by the gun.
    /// </summary>
    public float damage = 10f;

    /// <summary>
    /// The range of the gun.
    /// </summary>
    public float range = 100f;

    /// <summary>
    /// The camera from which the gun shoots.
    /// </summary>
    public Camera fpsCam;

    /// <summary>
    /// The particle system for the left muzzle flash.
    /// </summary>
    public ParticleSystem muzzleFlashLeft;

    /// <summary>
    /// The particle system for the right muzzle flash.
    /// </summary>
    public ParticleSystem muzzleFlashRight;

    /// <summary>
    /// The impact effect to instantiate when the gun hits a target.
    /// </summary>
    public GameObject impactEffect;

    /// <summary>
    /// The maximum amount of ammo the gun can hold.
    /// </summary>
    public int maxAmmo = 10;

    /// <summary>
    /// The current amount of ammo the gun has.
    /// </summary>
    private int currentAmmo = 10;

    /// <summary>
    /// The time it takes to reload the gun.
    /// </summary>
    public float reloadTime = 1f;

    /// <summary>
    /// Indicates if the gun is currently reloading.
    /// </summary>
    private bool isReloading = false;

    /// <summary>
    /// The weapons GameObject.
    /// </summary>
    public GameObject weapons;

    /// <summary>
    /// The animator for the reload animation.
    /// </summary>
    public Animator reloadAnimator;

    /// <summary>
    /// Initializes the current ammo to the maximum ammo value.
    /// </summary>
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    /// <summary>
    /// Coroutine for reloading the gun.
    /// </summary>
    IEnumerator Reload()
    {
        if(!isReloading)
        {
            isReloading = true;

            AudioSource.PlayClipAtPoint(reloadAudio, transform.position, 0.7f);
            reloadAnimator.SetBool("Reloading", true);

            yield return new WaitForSeconds(reloadTime);

            reloadAnimator.SetBool("Reloading", false);

            currentAmmo = maxAmmo;
            GameManager.instance.Ammo(currentAmmo);
            yield return new WaitForSeconds(0.2f);
            isReloading = false;
        }
    }

    /// <summary>
    /// Handles shooting from the left side.
    /// </summary>
    void OnShootLeft()
    {
        //If inventory is open, stop gun input
        if (GameManager.instance.IsInventoryOpen())
        {
            return;
        }
   
        currentAmmo--;

        //If player is reloading, stop input
        if (isReloading)
        {
            return;
        }

        //If Ammo is at 0, stp input
        if (currentAmmo <= 0)
        {
            return;
        }

        //Play audio and muzzle flash
        AudioSource.PlayClipAtPoint(gunAudio, transform.position, 0.2f);
        muzzleFlashLeft.Play();

        //update ammo count
        GameManager.instance.Ammo(currentAmmo);

        //Use Raycast to shoot bullets
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Check if bullet hit enemy
            EnemyAI enemy = hit.transform.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            //Spawn explosion at the bullet contact point
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }

    /// <summary>
    /// Handles shooting from the right side.
    /// </summary>
    void OnShootRight()
    {
        if (GameManager.instance.IsInventoryOpen())
        {
            return;
        }

        currentAmmo--;

        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            return;
        }

        //Play audio and muzzle flash
        AudioSource.PlayClipAtPoint(gunAudio2, transform.position, 0.2f);
        muzzleFlashRight.Play();

        //update ammo count
        GameManager.instance.Ammo(currentAmmo);

        //Use Raycast to shoot bullets
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Check if bullet hit enemy
            EnemyAI enemy = hit.transform.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            //Spawn explosion at the bullet contact point
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }

    /// <summary>
    /// Starts the reloading coroutine.
    /// </summary>
    void OnReload()
    {
        StartCoroutine(Reload());
    }
}


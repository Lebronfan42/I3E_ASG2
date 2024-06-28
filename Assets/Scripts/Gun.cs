using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlashLeft;
    public ParticleSystem muzzleFlashRight;
    public GameObject impactEffect;

    public int maxAmmo = 10;
    private int currentAmmo = 10;
    public float reloadTime =1f;
    private bool isReloading = false;
    public GameObject weapons;

    public Animator reloadAnimator;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    IEnumerator Reload()
    {
        if(!isReloading)
        {
            isReloading = true;

            reloadAnimator.SetBool("Reloading", true);
            //weapons.SetActive(false);

            yield return new WaitForSeconds(reloadTime);

            reloadAnimator.SetBool("Reloading", false);
            //weapons.SetActive(true);

            currentAmmo = maxAmmo;
            GameManager.instance.Ammo(currentAmmo);
            yield return new WaitForSeconds(0.2f);
            isReloading = false;

        }
        
    }

    void OnShootLeft()
    {
   
        currentAmmo --;

        if (isReloading)
        {
            return;
        }


        if (currentAmmo <= 0)
        {
            //StartCoroutine(Reload());
            return;
        }

        muzzleFlashLeft.Play();

        GameManager.instance.Ammo(currentAmmo);

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            

            EnemyAI enemy = hit.transform.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
        
    }



    void OnShootRight()
    {
        currentAmmo --;


        if (isReloading)
        {
            return;
        }


        if (currentAmmo <= 0)
        {
            //StartCoroutine(Reload());
            return;
        }

        muzzleFlashRight.Play();

        GameManager.instance.Ammo(currentAmmo);

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            EnemyAI enemy = hit.transform.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }

    void OnReload()
    {
        StartCoroutine(Reload());
    }

}

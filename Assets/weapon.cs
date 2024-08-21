using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    
    public int maxAmmo = 5;
    private int currentAmmo = 5;
    public int totalShotsFired;

    public int ammo;
    public bool isFiring;
    public Text ammoDisplay;

    // Update is called once per frame
    void Start()
    {
        totalShotsFired = 0;

        currentAmmo = maxAmmo;
    }
    void Update()
    {
        if (currentAmmo <= 0)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();

        }

        ammoDisplay.text = currentAmmo.ToString();
        //if (Input.GetMouseButtonDown(0) && !isFiring && currentAmmo > 0)
        //{
        //    isFiring = true;
        //    currentAmmo--;
        //    isFiring = false;
        //}
    }

    void Shoot()
        
    {
        totalShotsFired++;
        currentAmmo--;
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    

    
}

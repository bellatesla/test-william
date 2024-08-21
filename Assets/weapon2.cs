using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon2 : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    
    public int maxAmmo = 3;
    private int currentAmmo = 3;
    
    // Update is called once per frame
    void Start()
    {
        if (currentAmmo == -1)
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

        
    }

    void Shoot()
        
    {
        currentAmmo--;
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    

    
}

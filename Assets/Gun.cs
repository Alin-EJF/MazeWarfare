using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform cam;
    public GameObject bulletPrefab;

    float timeSinceLastShot;

    private void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }

    private void OnDisable() => gunData.reloading = false;

    public void StartReload()
    {
        if (!gunData.reloading && this.gameObject.activeSelf)
            StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        gunData.reloading = false;
    }

/*    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);*/

    private void Shoot()
    {
        if (gunData.currentAmmo > 0)
        {
            
                if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, gunData.maxDistance*100))
                {
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.TakeDamage(gunData.damage);
                    Debug.Log(damageable);
                }

                var bullet = Instantiate(bulletPrefab, cam.position, cam.rotation);
                bullet.GetComponent<Rigidbody>().velocity = cam.forward * 30;
                gunData.currentAmmo--;
                  
                timeSinceLastShot = 0;
                OnGunShot();
            
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if (gunData.currentAmmo > 0)
            {
                
                Shoot();
            }
        }

        Debug.DrawRay(cam.position, cam.forward * gunData.maxDistance);

        
    }
 
    private void OnGunShot() { }
}

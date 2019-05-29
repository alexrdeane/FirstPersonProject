using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 10, maxReserve = 500, maxClip = 30;
    public float spread = 2f, recoil = 10f, shootRate = 0.2f;
    public Transform shotOrigin;
    public GameObject bulletPrefab;
    public bool canShoot = false;

    private int currentReserve = 0, currentClip = 0;
    private float shootTimer = 0f;
    
    void Start()
    {
        Reload();
    }

    void Update()
    {
        //increase shoot timer
        shootTimer += Time.deltaTime;
        //check if shoot timer reaches shoot rate
        if (shootTimer >= shootRate)
        {
            //can shoot
            canShoot = true;
        }
    }

    public void Reload()
    {
        if(currentReserve > 0)
        {
            if(currentReserve < maxClip)
            {
                int offset = maxClip - currentClip;
                currentReserve -= offset;
            }
            if(currentClip < maxClip)
            {
                int tempMag = currentReserve;
                currentClip = tempMag;
                currentReserve -= tempMag;
            }
        }
    }

    public void Shoot()
    {
        print("Shooting!");
        //reduce clip size
        currentClip--;
        //reset shoot timer
        shootTimer = 0f;
        //reset can shoot
        canShoot = false;
        //get origin + direction of fire
        Camera attachedCamera = Camera.main;
        Transform camTransform = attachedCamera.transform;

        Vector3 lineOrigin = shotOrigin.position;
        Vector3 direction = camTransform.forward;
        //shoot bullet
        GameObject clone = Instantiate(bulletPrefab, camTransform.position, camTransform.rotation);
        Bullet bullet = clone.GetComponent<Bullet>();
        bullet.Fire(lineOrigin, direction);
    }
}

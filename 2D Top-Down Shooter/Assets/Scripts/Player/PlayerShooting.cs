using System;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public static PlayerShooting instance;
    
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform[] firePoints;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (PowerManager.instance.isOnPowerMenu) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        
        
        if (UIManager.instance.bulletValue == 0)
        {
            Instantiate(bulletPrefab, firePoints[0].position, firePoints[0].rotation);
        }
        else
        {
            Instantiate(bulletPrefab, firePoints[1].position, firePoints[1].rotation);
            Instantiate(bulletPrefab, firePoints[2].position, firePoints[2].rotation);
        }
        
    }
}

using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    [SerializeField] private float forceamount = 15f;

    [SerializeField] private float bulletLifeTime = 4f;
    [SerializeField] private float bulletDamageAmount = 1f;
    [SerializeField] private GameObject bulletHitEffectPrefab;
    
    private float timeCounter;
    
    private Rigidbody2D bulletRb;

    public bool isPlayerBullet;
    
    private void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        bulletRb.AddForce(transform.right * forceamount, ForceMode2D.Impulse);

        timeCounter = bulletLifeTime;
    }

    private void Update()
    {
        timeCounter -= Time.deltaTime;

        if (timeCounter <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isPlayerBullet && other.gameObject.TryGetComponent(out Health enemyHealth))
        {
            Instantiate(bulletHitEffectPrefab, other.contacts[0].point, Quaternion.identity);
            enemyHealth.TakeDamage(bulletDamageAmount);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player") && !isPlayerBullet)
        {
            Instantiate(bulletHitEffectPrefab, other.contacts[0].point, Quaternion.identity);
            other.gameObject.GetComponent<Health>().TakeDamage(bulletDamageAmount);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Walls"))
        {
            Destroy(gameObject);
        }
    }
}

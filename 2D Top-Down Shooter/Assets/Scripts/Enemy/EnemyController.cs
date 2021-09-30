using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform enemyBody;
    
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float stopDistance = 4f;

    private float shootTimer;
    private float shootWhileMovingTimer;
    
    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }


    private void Start()
    {
        stopDistance = Random.Range(8f, 17f);
        shootTimer = Random.Range(1.5f, 3f);
        shootWhileMovingTimer = Random.Range(2f, 5f);
    }

    private void Update()
    {
        if (GameManager.instance.isGameOver || GameManager.instance.isLevelCompleted) return;
        
        if (Vector2.Distance(transform.position, playerTransform.position) > stopDistance)
        {
            transform.position =
                Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);

            shootWhileMovingTimer -= Time.deltaTime;

            if (shootWhileMovingTimer < 0f)
            {
                ShootWhileMoving();
            }
        }
        else
        {
            shootTimer -= Time.deltaTime;

            if (shootTimer < 0)
            {
                ShootWhileStop();
            }

        }
        
        Vector2 dir = playerTransform.position - enemyBody.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            
        enemyBody.rotation = Quaternion.Euler(0, 0, angle);
    }

    void ShootWhileStop()
    {
        shootTimer = Random.Range(1.5f, 3f);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
    
    void ShootWhileMoving()
    {
        shootWhileMovingTimer = Random.Range(2f, 6f);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}

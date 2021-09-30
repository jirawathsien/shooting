using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 15f;
    [SerializeField] private float maxSpeed = 30f;
    [SerializeField] private float minSpeed = 20f;
    [SerializeField] private Transform playerBody;
    
    private Vector2 movement;
    private Vector2 mousePosition;
   
    private Rigidbody2D playerRb;
    private Camera mainCam;

    public void AddPlayerSpeed(float newSpeed)
    {
        if (playerSpeed <= maxSpeed)
        {
            playerSpeed = newSpeed;
        }

        if (playerSpeed > maxSpeed)
        {
            playerSpeed = maxSpeed;
        }
    }
    
    public void ReducePlayerSpeed(float newSpeed)
    {
        if (playerSpeed >= minSpeed)
        {
            playerSpeed = newSpeed;
        }
     
        if (playerSpeed < minSpeed)
        {
            playerSpeed = minSpeed;
        }
    }

    void SetSpeed(float newSpeed)
    {
        playerSpeed = newSpeed;
    }
    
    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
    }

    private void Start()
    {
        playerRb.AddForce(playerSpeed * Vector2.right, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (!GameManager.instance.isGameStarted) return;
        if (PowerManager.instance.isOnPowerMenu || GameManager.instance.isGameOver || GameManager.instance.isLevelCompleted) return;

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        
        Vector2 dir = mousePosition - (Vector2) playerBody.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

        playerBody.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + movement * (playerSpeed * Time.fixedDeltaTime));
    }

    public void ResetSpeed()
    {
        SetSpeed(minSpeed);
        PlayerPowerSystem.instance.GetPlayerSlider().value = 0f;
    }
}

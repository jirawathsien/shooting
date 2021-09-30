using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static Health instance;
    
    [SerializeField] private float maxHealth;
    [SerializeField] private Slider healthSlider;
    
    private float health;

    public bool isBoss;
    public bool isPlayer;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        UpdateUI();

        if (!isBoss && health <= 0)
        {
            if (isPlayer)
            {
                GameManager.instance.isGameOver = true;
                UIManager.instance.EnableGameOverPanel();
            }
            else
            {
                PlayerPowerSystem.instance.IncreasePlayerPower(0.25f);
            }
            
            Destroy(gameObject);
        }
        else if (health <= 0 && isBoss)
        {
            GameManager.instance.isLevelCompleted = true;
         //   UIManager.instance.EnableLevelCompletedPanel();
            Destroy(gameObject);
        }
    }
    
    public void UpdateUI()
    {
        healthSlider.value = health / maxHealth;
    }

    public float GetPlayerHealth()
    {
        return maxHealth;
    }


}

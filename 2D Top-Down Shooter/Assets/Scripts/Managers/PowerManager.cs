using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerManager : MonoBehaviour
{
    public static PowerManager instance;
    
    public GameObject setPowerPanel;
    public Image speedFillAmount;
    public Image healthFillAmount;
    
    public bool isOnPowerMenu;

    private PlayerController playerController;

    private static int powerSpeedAmount = 20;
    private static float healthAmount;

    public Slider healthSlider;
    
    public Action onDoneButtonPress;
    
    private void Awake()
    {
        instance = this;
        playerController = FindObjectOfType<PlayerController>();
    }

    public void OnUserPowerButtonPressed()
    {
        PlayerPowerSystem.instance.userPowerButton.SetActive(false);
        isOnPowerMenu = true;
        Time.timeScale = 0;
        setPowerPanel.SetActive(true);
    }

    public void OnDoneButtonPressed()
    {
        isOnPowerMenu = false;
        Time.timeScale = 1;
        setPowerPanel.SetActive(false);
       
        onDoneButtonPress?.Invoke();
       
    }
    
    public void OnIncreaseButtonPress()
    {
        if (powerSpeedAmount < 30 && PlayerPowerSystem.instance.currentPowerAmount > 0)
        {
            powerSpeedAmount++;
            speedFillAmount.fillAmount += 0.1f;
            PlayerPowerSystem.instance.GetPlayerSlider().value -= 0.1f;
            PlayerPowerSystem.instance.ReducePowerAmount(1f);
            playerController.AddPlayerSpeed(powerSpeedAmount);
        }
    }
   
    public void OnDecreaseButtonPress()
    {
        if (powerSpeedAmount > 20)
        {
            powerSpeedAmount--;
            speedFillAmount.fillAmount -= 0.1f;
            PlayerPowerSystem.instance.GetPlayerSlider().value += 0.1f;
            PlayerPowerSystem.instance.AddPointAmount(1f);
            playerController.ReducePlayerSpeed(powerSpeedAmount);
        }
    }

    public void OnIncreaseHealthButtonPress()
    {
        if (Health.instance.GetPlayerHealth() < 15 && PlayerPowerSystem.instance.currentPowerAmount > 0)
        {
            healthAmount = Health.instance.GetPlayerHealth() + 1f;
            healthFillAmount.fillAmount += 0.1f;
            PlayerPowerSystem.instance.GetPlayerSlider().value -= 0.1f;
            PlayerPowerSystem.instance.ReducePowerAmount(1f);
            healthSlider.value += 1f;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            OnIncreaseHealthButtonPress();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            OnIncreaseButtonPress();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UIManager.instance.bulletValue = 1;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UIManager.instance.bulletValue = 0;
        }
    }
}

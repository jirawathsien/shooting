using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerSystem : MonoBehaviour
{
   public static PlayerPowerSystem instance;
   
   [SerializeField] private float maxPowerAmount = 10f;
   [SerializeField] private Slider playerPowerSlider;
   
   public GameObject userPowerButton;
   
   public float currentPowerAmount;

   public void AddPointAmount(float newPowerAmount)
   {
      currentPowerAmount += newPowerAmount;
      
      if (currentPowerAmount >= maxPowerAmount)
      {
         currentPowerAmount = maxPowerAmount;
      }
   }

   public void ReducePowerAmount(float newPowerAmount)
   {
      currentPowerAmount -= newPowerAmount;

      if (currentPowerAmount <= 0)
      {
         currentPowerAmount = 0;
      }
   }
   
   public Slider GetPlayerSlider()
   {
      return playerPowerSlider;
   }
   
   private void Awake()
   {
      instance = this;
      userPowerButton.SetActive(false);
   }

   public void IncreasePlayerPower(float newPowerAmount)
   {
      if (currentPowerAmount <= maxPowerAmount)
      {
         currentPowerAmount += newPowerAmount;
         playerPowerSlider.value = currentPowerAmount / maxPowerAmount;
      }
      
      if (currentPowerAmount > 9f)
      {
         currentPowerAmount = maxPowerAmount;
      }
   }


   
}

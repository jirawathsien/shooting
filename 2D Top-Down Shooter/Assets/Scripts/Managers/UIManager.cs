using System;
using DG.Tweening;
using TMPro;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public RectTransform playButton;
    public Ease playButtonEaseType;
    
    public TMP_Dropdown dropDown;
    
    public GameObject gameOverPanel;
    public GameObject levelCompletedPane;
    public GameObject mainMenuPanel;
    public GameObject inGameUIPanel;

  
    public TextMeshProUGUI waveNumberText;
    public TextMeshProUGUI enemyNumberText;
    public TextMeshProUGUI timerText;

    public TMP_InputField inputField;

    public int bulletValue;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        dropDown.onValueChanged.AddListener(delegate { DropDownSelected(dropDown); });
        
        gameOverPanel.SetActive(false);
        levelCompletedPane.SetActive(false);

        playButton.transform.DOScale(new Vector3(1.15f, 1.15f, 1f), 0.75f).SetLoops(-1, LoopType.Yoyo)
            .SetEase(playButtonEaseType);
    }

    
    public void DropDownSelected(TMP_Dropdown dropdown)
    {
        bulletValue = dropdown.value;

        if (PlayerPowerSystem.instance.currentPowerAmount > 5f)
        {
            dropDown.enabled = true;
        }
        else
        {
            dropDown.enabled = false;
        }
        
        if (bulletValue == 1)
        {
            PlayerPowerSystem.instance.GetPlayerSlider().value -= 5f;
            PlayerPowerSystem.instance.ReducePowerAmount(5f);
        }
    }

    
    public void EnableGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void EnableLevelCompletedPanel()
    {
        levelCompletedPane.SetActive(true);
    }

    public void SetWaveNumber(String newWaveNumber)
    {
        waveNumberText.text = newWaveNumber;
    }
    
    public void SetEnemyNumberText(String newEnemyCount)
    {
        enemyNumberText.text = newEnemyCount;
    }

    public void SetTimerText(String newText)
    {
        timerText.text = newText;
    }
    
}
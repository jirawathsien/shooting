using DG.Tweening;
using TMPro;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public static EnemyTrigger instance;
    
    public string enemyType;

    public GameObject enemySpawner;

    public GameObject prevDoor;
    public GameObject nextDoor;

    public Transform startPoint;
    private Transform player;

    public TextMeshProUGUI speedText;
    
    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        GameManager.instance.OnGameStart += OnGameStart;
        PowerManager.instance.onDoneButtonPress += EnableEnemyTypeText;
    }

    void EnableEnemyTypeText()
    {
        speedText.gameObject.SetActive(true);
        speedText.DOFade(0.4f, 0.5f).From(1f).SetLoops(5, LoopType.Yoyo).OnComplete(() => speedText.gameObject.SetActive(false));
    }

    private void OnGameStart()
    {
        PlayerPowerSystem.instance.IncreasePlayerPower(5f);
       
    }

    private static bool isTriggered; 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            PowerManager.instance.OnUserPowerButtonPressed();
           
            nextDoor.SetActive(false);
            prevDoor.SetActive(true);
            enemySpawner.SetActive(true);
            speedText.text = $"Next room: {enemyType} wave";
            
            OnGameStart();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTriggered = false;
            nextDoor.SetActive(true);
            prevDoor.SetActive(true);
            
            player.DOMove(startPoint.position, 0.25f);
        }
        
    }
}

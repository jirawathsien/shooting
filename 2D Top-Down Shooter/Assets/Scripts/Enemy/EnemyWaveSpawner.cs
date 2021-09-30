using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave {
        public EnemyController[] enemies;
        public int count;
        public float timeBetweenSpawns;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves;
    public GameObject boss;
    public Transform bossSpawnPoint;
   // public GameObject healthBar;

    private Wave currentWave;
    private int currentWaveIndex;
    private Transform player;
    private bool spawningFinished;

    public GameObject nextDoor;

    public TextMeshProUGUI moveToNextRoomText;
    private float timer;
    private void OnEnable()
    {
        player = GameObject.FindWithTag("Player").transform;
        UIManager.instance.SetWaveNumber("Current wave number: " + (currentWaveIndex  + 1));
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    private void Update()
    {
        if (GameManager.instance.isGameOver || GameManager.instance.isLevelCompleted) return;
        timer += Time.deltaTime;
        UIManager.instance.SetTimerText(timer.ToString("0") + " s");
        
        if (spawningFinished && GameObject.FindGameObjectsWithTag("Enemy").Length == 0) 
        {
            spawningFinished = false;
            if (currentWaveIndex + 1 < waves.Length)
            {
                currentWaveIndex++;
                UIManager.instance.SetWaveNumber("Current wave number: " + (currentWaveIndex + 1));
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
            else
            {
                moveToNextRoomText.text = $"Amazing! Wave finished in {timer.ToString("0")} seconds \n Move to Next Room";
                moveToNextRoomText.gameObject.SetActive(true);
                moveToNextRoomText.DOFade(0.3f, 0.75f).From(1f).SetLoops(3, LoopType.Yoyo)
                    .OnComplete(()=>moveToNextRoomText.gameObject.SetActive(false));
                nextDoor.SetActive(false);
                gameObject.SetActive(false);
            }

        }
    }

    IEnumerator StartNextWave(int waveIndex) {
        UIManager.instance.SetEnemyNumberText($"Total enemy in wave  {waveIndex + 1} : " + (waves[waveIndex].count));
      
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(waveIndex));
    }

    IEnumerator SpawnWave (int waveIndex) {
        currentWave = waves[waveIndex];

        for (int i = 0; i < currentWave.count; i++)
        {

            if (player == null)
            {
                yield break;
            }
            
            var randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
            var randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomSpawnPoint.position, transform.rotation);

            if (i == currentWave.count - 1)
            {
                spawningFinished = true;
            }
            else
            {
                spawningFinished = false;
            }

            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }
    }
}

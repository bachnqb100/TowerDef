using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;

    public float timeBetweenWaves = 6f;
    private float countdown = 3f;

    public TextMeshProUGUI waveCountdownText;

    private int waveNumber = 0;

    private void Update()
    {
        if (countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        if (waveNumber >= 1)
            waveCountdownText.text = string.Format("{0:00.00}", countdown);
        else waveCountdownText.text = "goooo";
    }

    IEnumerator SpawnWave()
    {
        waveNumber++;
        PlayerStats.Rounds++;

        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.2f);
        }

    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private int currentWave = 1;

    public GameObject enemyPrefab;
    public Transform enemySpawnTransform;

    [SerializeField]private float timeGapBetweenEnemies = .2f;
    [SerializeField]private float timeGapBetweenWaves = 5f;
    [SerializeField]private float currenTimeGapForWaves = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currenTimeGapForWaves = timeGapBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        currenTimeGapForWaves -= Time.deltaTime;

        if(currenTimeGapForWaves <= 0)
        {
            StartCoroutine(SpawnWave());
            currenTimeGapForWaves = timeGapBetweenWaves;
            currentWave++;
        }
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < currentWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSecondsRealtime(timeGapBetweenEnemies);
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, enemySpawnTransform.position, Quaternion.identity);
    }
}

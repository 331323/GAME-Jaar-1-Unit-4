using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9.0f;
    public int enemyCount;
    public int waveNumber = 1;
    public GameObject PowerupPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        Instantiate(PowerupPrefab, GenerateSpawnPosition(), PowerupPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy1>().Length;
        
        if (enemyCount == 0)
        {
            Instantiate(PowerupPrefab, GenerateSpawnPosition(), PowerupPrefab.transform.rotation);
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
    }

    private Vector3  GenerateSpawnPosition()
    {
          float spawnPosX = Random.Range(-spawnRange, spawnRange);
          float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        
          Vector3 randomePos = new Vector3(spawnPosX, 0, spawnPosZ);
          return randomePos;
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i <enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation); 
        }
    }
}

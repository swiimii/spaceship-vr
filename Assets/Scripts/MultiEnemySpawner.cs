using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiEnemySpawner : MonoBehaviour
{
    public GameObject enemySpawnPrefab;
    // Start is called before the first frame update
    public float spawnInterval = 5f, timeElapsed = 0f, minimumRadius = 5f, maximumRadius = 8f;

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("IsPlaying") != 0)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > spawnInterval)
            {
                SpawnEnemy();
                timeElapsed = 0f;
            }
        }
    }

    public void SpawnEnemy()
    {
        var rotation = new Vector3(Random.value * 360, Random.value * 360, Random.value * 360);
        var directionOfSpawn = new Vector3(Random.value -.5f, Random.value-.5f, Random.value-.5f).normalized;
        var distance = Random.value * (maximumRadius - minimumRadius) + minimumRadius;

        var enemyInstance = Instantiate(enemySpawnPrefab);
        enemyInstance.transform.position = transform.position + directionOfSpawn * distance;
        enemyInstance.transform.rotation = Quaternion.Euler(rotation);

    }
}

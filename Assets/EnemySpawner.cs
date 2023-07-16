using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The prefab of the enemy to spawn
    public int minEnemies = 4; // The minimum number of enemies to spawn
    public int maxEnemies = 6; // The maximum number of enemies to spawn
    public float spawnIntervalMin = 8f; // The minimum interval between spawns
    public float spawnIntervalMax = 12f; // The maximum interval between spawns
    public float spawnRadius = 50f; // The radius around the spawner where the enemies will appear
    public int maxEnemiesOnScene = 30; // The maximum number of enemies in the scene

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax));

            int numEnemies = Mathf.Min(maxEnemiesOnScene - GameObject.FindGameObjectsWithTag("Enemy").Length, Random.Range(minEnemies, maxEnemies + 1));

            if(numEnemies > 0) {
                for (int i = 0; i < numEnemies; i++)
                {
                    Vector2 randomCirclePoint = Random.insideUnitCircle * spawnRadius;
                    Vector3 spawnPosition = new Vector3(transform.position.x + randomCirclePoint.x, transform.position.y, transform.position.z + randomCirclePoint.y);

                    spawnPosition.y = transform.position.y; // Ensure the enemy stays on the same plane

                    // Instantiate the enemy
                    GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                    // Make one of the enemies larger
                    if (i == 0)
                    {
                        enemy.transform.localScale *= 2;
                    }

                    // Set the player reference for the enemy AI
                    enemy.GetComponent<EnemyAI>().player = GameObject.FindGameObjectWithTag("Player").transform;

                    // Tag the enemy
                    enemy.tag = "Enemy";
                }
            }
        }
    }
}
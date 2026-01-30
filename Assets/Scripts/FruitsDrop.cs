using System.Collections;
using UnityEngine;

public class FruitsDrop : MonoBehaviour
{
    [SerializeField] private Transform[] fruits;
    [SerializeField] private Transform[] barrels;

    int barrelIndex;
    int fruitIndex;

    float spawnTimer = 0f;
    float spawnInterval = 5f;       // Starting delay
    float minSpawnInterval = 0.5f;  // Minimum delay between drops
    float accelerationRate = 0.0005f; // How quickly the spawn interval decreases over time

    void Update()
    {
        // Reduce the spawn interval smoothly over time
        spawnInterval -= accelerationRate * Time.deltaTime;
        spawnInterval = Mathf.Max(spawnInterval, minSpawnInterval); // Clamp so it doesn't go too fast

        // Countdown to next spawn
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            // Pick random barrel and fruit
            barrelIndex = Random.Range(0, barrels.Length);
            fruitIndex = Random.Range(0, fruits.Length);

            // Spawn fruit
            Transform spawnFruit = Instantiate(fruits[fruitIndex], barrels[barrelIndex]);
            spawnFruit.position = barrels[barrelIndex].position - new Vector3(0, 1, 0);

            // Reset timer for next drop
            spawnTimer = spawnInterval;
        }
    }
}

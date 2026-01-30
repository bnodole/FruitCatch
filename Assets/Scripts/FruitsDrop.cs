using System.Collections;
using UnityEngine;

public class FruitsDrop : MonoBehaviour
{
    [SerializeField] private Transform[] fruits;
    [SerializeField] private Transform[] barrels;

    int barrelIndex;
    int fruitIndex;
    float spawnTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTime = 5;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;
        if(spawnTime <= 0)
        {
            barrelIndex = Random.Range(0, barrels.Length);
            fruitIndex = Random.Range(0, fruits.Length);
            Transform spawnFruit = Instantiate(fruits[fruitIndex], barrels[barrelIndex]);
            spawnFruit.position = barrels[barrelIndex].position - new Vector3(0, 1, 0);
            spawnTime = 5;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public float size = 0.0f;
    public List<Transform> spawnPositions = new List<Transform>();
    public int minNoOfEnemiesToSpawn = 1;
    public int maxNoOfEnemeiesToSpawn = 3;
    public GameObject enemyPrefab = null;
    public EnemyContainer enemyContainer = null;
    
    private void Initialize()
    {
        if (spawnPositions.Count <= 0 || enemyPrefab == null)
            return;

        int noOfEnemyToSpawn = Random.Range(minNoOfEnemiesToSpawn, maxNoOfEnemeiesToSpawn +1);
        var positions = spawnPositions;
        for (int indx = 0; indx < noOfEnemyToSpawn; indx++)
        {
            int spawnPosIndx = Random.Range(0, positions.Count);
            Instantiate(enemyPrefab, positions[spawnPosIndx].position, Quaternion.Euler(0f, 90f, 0f), enemyContainer.transform);
            positions.RemoveAt(spawnPosIndx);
            enemyContainer.count++;
        }
    }

    private void Start()
    {
        enemyContainer = EnemyContainer.instance;
        Initialize();
    }
}

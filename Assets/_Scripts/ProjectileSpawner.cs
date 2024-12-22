using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] projectilePrefabs;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private float projectielSpeed;
    [SerializeField] private float spawnDuration;
    [SerializeField] private float increaseSpeed = 1f;
    float increaseTime = 0;
    private float timeUntilSpawn;
    

    // Update is called once per frame
    void Update()
    {
        SpawnLoop();
        //IncreaseProjectileSpeed();
    }
    void SpawnLoop()
    {
        timeUntilSpawn += Time.deltaTime;
        if (timeUntilSpawn >=  spawnDuration)
        {
            Spawn();
            timeUntilSpawn = 0f;
        }
    }
    void Spawn()
    {
        GameObject spawnP = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject spawnedProjectile = Instantiate(projectilePrefabs[Random.Range(0, projectilePrefabs.Length)], spawnP.transform.position, Quaternion.identity);
    }

}

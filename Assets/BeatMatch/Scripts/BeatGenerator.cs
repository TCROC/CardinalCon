using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatGenerator : MonoBehaviour
{

    public float xOffset;

    public float minSpawnTime;
    public float maxSpawnTime;

    public GameObject beatCubePrefab;

    public Transform[] spawnPoints;

    private float currentSpawnTimeToWait;
    private float _currentSpawnTime;

    private void Update()
    {
        if (Time.time - _currentSpawnTime > currentSpawnTimeToWait)
            Spawn();
    }

    public void Spawn()
    {
        float offset = Random.Range(-xOffset, xOffset);
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject beatCubeClone = Instantiate(beatCubePrefab, spawnPoint.position, Quaternion.identity);
            Vector3 tempPos = beatCubeClone.transform.position;
            tempPos.x += offset;
            beatCubeClone.transform.position = tempPos;
            Destroy(beatCubeClone, 15);
        }

        currentSpawnTimeToWait = Random.Range(minSpawnTime, maxSpawnTime);
        _currentSpawnTime = Time.time;
    }
}

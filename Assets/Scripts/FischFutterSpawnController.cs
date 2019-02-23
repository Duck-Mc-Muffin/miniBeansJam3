using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FischFutterSpawnController : MonoBehaviour
{
    public Vector3 SpawnArea;
    public GameObject FischfutterPrefab;
    public float SpawnDelay;
    public int MaxFutterSpawns;

    private float nextSpawn = 0;

    // Update is called once per frame
    private void Update()
    {
        if (nextSpawn <= 0)
        {
            GameObject fischfutter = GameObject.Instantiate(FischfutterPrefab);

            Vector3 randomPos = new Vector3(Random.Range(-SpawnArea.x, SpawnArea.x), SpawnArea.y + 5, Random.Range(-SpawnArea.z, SpawnArea.z));

            fischfutter.transform.position = randomPos;

            nextSpawn += SpawnDelay;
        }

        if (GameObject.FindGameObjectsWithTag("Fischfutter").Length < MaxFutterSpawns)
        {
            nextSpawn -= Time.deltaTime;
        }
    }
}

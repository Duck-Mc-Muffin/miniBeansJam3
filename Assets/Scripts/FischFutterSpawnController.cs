﻿using System.Collections;
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
            fischfutter.transform.parent = this.gameObject.transform;

            Vector3 randomPos = transform.position + new Vector3(Random.Range(-SpawnArea.x, SpawnArea.x), SpawnArea.y, Random.Range(-SpawnArea.z, SpawnArea.z));

            fischfutter.transform.position = randomPos;

            nextSpawn += SpawnDelay;
        }

        if (GameObject.FindGameObjectsWithTag("Food").Length < MaxFutterSpawns)
        {
            nextSpawn -= Time.deltaTime;
        }
    }
}

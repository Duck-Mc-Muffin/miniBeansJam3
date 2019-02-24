using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaweedSpawner : MonoBehaviour
{
    public Vector2 SpawnArea;
    public int Count;
    public GameObject SeaWeedPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Count; i++)
        {
            float posX = Random.Range(-SpawnArea.x, SpawnArea.x);
            float posY = Random.Range(-SpawnArea.y, SpawnArea.y);
            GameObject seaWeed = GameObject.Instantiate(SeaWeedPrefab);
            seaWeed.transform.position = new Vector3(posX, 0, posY);
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

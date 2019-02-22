using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public Vector3 SpawnArea;
    public GameObject FischfutterPrefab;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject fischfutter = GameObject.Instantiate(FischfutterPrefab);
    }
}
